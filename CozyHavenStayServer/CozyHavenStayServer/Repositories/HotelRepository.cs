using CozyHavenStayServer.Context;
using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using Microsoft.EntityFrameworkCore;
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

        public Task<Hotel> CreateAsync(Hotel dbRecord)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Hotel dbRecord)
        {
            throw new NotImplementedException();
        }

        public Task<List<Hotel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Hotel> GetAsync(Expression<Func<Hotel, bool>> filter, bool useNoTracking = false)
        {
            throw new NotImplementedException();
        }

        public Task<Hotel> UpdateAsync(Hotel dbRecord)
        {
            throw new NotImplementedException();
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
