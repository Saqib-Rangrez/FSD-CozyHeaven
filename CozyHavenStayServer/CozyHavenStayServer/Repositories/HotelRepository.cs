using CozyHavenStayServer.Context;
using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace CozyHavenStayServer.Repositories
{
    public class HotelRepository : IRepository<Hotel>
    {
        private readonly CozyHeavenStayContext _context;
        public HotelRepository(CozyHeavenStayContext context)
        {
            _context = context;
        }

        public async Task<Hotel> CreateAsync(Hotel dbRecord)
        {
            _context.Add(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }

        public async Task<bool> DeleteAsync(Hotel dbRecord)
        {
            _context.Remove(dbRecord);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Hotel>> GetAllAsync()
        {
            return await _context.Hotels.ToListAsync();
        }

        public async Task<Hotel> GetAsync(Expression<Func<Hotel, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _context.Hotels.AsNoTracking().Where(filter).FirstOrDefaultAsync();
            else
                return await _context.Hotels.Where(filter).FirstOrDefaultAsync();

        }

        public async Task<Hotel> UpdateAsync(Hotel dbRecord)
        {
            _context.Hotels.Update(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }

        public async Task<List<Hotel>> SearchHotelsAsync(string location, string amenities)
        {

            
            var hotels = await _context.Hotels
                .Where(h => h.Location.Contains(location) && h.Amenities.Contains(amenities))
                .ToListAsync();

            return hotels;
        }

        public async Task<List<Room>> SearchHotelRoomsAsync(string location, DateTime checkInDate, DateTime checkOutDate, int numberOfRooms)
        {
            // Implement logic to search for available hotel rooms based on location, dates, and number of rooms
            var availableRooms = await _context.Rooms
                .Include(r => r.Hotel)
                .Where(r => r.Hotel.Location.Contains(location))
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
