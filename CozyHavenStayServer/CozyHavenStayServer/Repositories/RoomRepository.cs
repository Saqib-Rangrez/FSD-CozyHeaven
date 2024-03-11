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
            // _context.Add(dbRecord);
            // await _context.SaveChangesAsync();
            // return dbRecord;

            var existingRecord = await _context.Rooms.FindAsync(dbRecord.RoomId);

            if (existingRecord != null)
            {
                _context.Entry(existingRecord).CurrentValues.SetValues(dbRecord);
            }
            else
            {
                _context.Add(dbRecord); 
            }
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
            return await _context.Rooms
            .Include(r => r.Bookings).ThenInclude(b => b.User)
            .Include(r => r.RoomImages)
            .Include(r => r.Hotel).ThenInclude(h => h.HotelImages)
            .Include(r => r.Hotel).ThenInclude(h => h.Reviews)
            .ToListAsync();
        }

        public async Task<Room> GetAsync(Expression<Func<Room, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _context.Rooms.AsNoTracking().Where(filter)
                .Include(r => r.Bookings).ThenInclude(b => b.User)
                .Include(r => r.RoomImages)
                .Include(r => r.Hotel).ThenInclude(h => h.HotelImages)
                .Include(r => r.Hotel).ThenInclude(h => h.Reviews)
                .FirstOrDefaultAsync();
            else
                return await _context.Rooms.Where(filter)
                .Include(r => r.Bookings).ThenInclude(b => b.User)
                .Include(r => r.RoomImages)
                .Include(r => r.Hotel).ThenInclude(h => h.HotelImages)
                .Include(r => r.Hotel).ThenInclude(h => h.Reviews)
                .FirstOrDefaultAsync();
        }

        public async Task<Room> UpdateAsync(Room dbRecord)
        {
            _context.Rooms.Update(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }        
    }
}
