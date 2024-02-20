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


        
    }
}
