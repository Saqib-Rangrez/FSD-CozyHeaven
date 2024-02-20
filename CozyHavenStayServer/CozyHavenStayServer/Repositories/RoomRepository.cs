using CozyHavenStayServer.Context;
using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace CozyHavenStayServer.Repositories
{
    public class RoomRepository : IRepository<Room>
    {
        private readonly CozyHeavenStayContext _context;
        public RoomRepository(CozyHeavenStayContext context)
        {
            _context = context;
        }

        public async Task<Room> CreateAsync(Room dbRecord)
        {
            _context.Add(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }

        public async Task<bool> DeleteAsync(Room dbRecord)
        {
            _context.Remove(dbRecord);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Room>> GetAllAsync()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task<Room> GetAsync(Expression<Func<Room, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _context.Rooms.AsNoTracking().Where(filter).FirstOrDefaultAsync();
            else
                return await _context.Rooms.Where(filter).FirstOrDefaultAsync();
        }

        public async Task<Room> UpdateAsync(Room dbRecord)
        {
            _context.Rooms.Update(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }


        public async Task<List<Room>> SearchHotelRoomsAsync(string location, DateTime checkInDate, DateTime checkOutDate, int numberOfRooms)
        {
            // Implement logic to search for available hotel rooms based on location, dates, and number of rooms
            var availableRooms = await _context.Rooms
                .Include(r => r.Hotel)
                .Where(predicate: r => r.Hotel.Location.Contains(location))
                .ToListAsync();

            // Filter available rooms based on availability for the specified dates
            var bookedRoomIds = await _context.Bookings
                .Where(b => (checkInDate >= b.CheckInDate && checkInDate < b.CheckOutDate) ||
                            (checkOutDate > b.CheckInDate && checkOutDate <= b.CheckOutDate))
                .Select(b => b.RoomId)
                .ToListAsync();

            availableRooms = availableRooms.Where(r => !bookedRoomIds.Contains(r.RoomId)).ToList();

            // Filter available rooms based on the number of rooms required
            availableRooms = availableRooms.Take(numberOfRooms).ToList();

            return availableRooms;

        }
    }
}
