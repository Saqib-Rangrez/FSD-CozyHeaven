using CozyHavenStayServer.Context;
using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace CozyHavenStayServer.Repositories
{
    public class RoomImageRepository : IRepository<RoomImage>
    {
        private readonly CozyHeavenStayContext _context;
        public RoomImageRepository(CozyHeavenStayContext context)
        {
            _context = context;
        }

        public async Task<RoomImage> CreateAsync(RoomImage dbRecord)
        {
            _context.RoomImages.Add(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }

        public async Task<bool> DeleteAsync(RoomImage dbRecord)
        {
            _context.RoomImages.Remove(dbRecord);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<RoomImage>> GetAllAsync()
        {
            return await _context.RoomImages.ToListAsync();
        }

        public async Task<RoomImage> GetAsync(Expression<Func<RoomImage, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _context.RoomImages.AsNoTracking().Where(filter).FirstOrDefaultAsync();
            else
                return await _context.RoomImages.Where(filter).FirstOrDefaultAsync();
        }

        public async Task<RoomImage> UpdateAsync(RoomImage dbRecord)
        {
            _context.RoomImages.Update(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }
    }
}
