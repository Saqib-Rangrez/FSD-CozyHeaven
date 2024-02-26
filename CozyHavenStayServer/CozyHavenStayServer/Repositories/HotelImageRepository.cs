using CozyHavenStayServer.Context;
using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace CozyHavenStayServer.Repositories
{
    public class HotelImageRepository : IRepository<HotelImage>
    {
        private readonly CozyHeavenStayContext _context;
        public HotelImageRepository(CozyHeavenStayContext context)
        {
            _context = context;
        }

        public async Task<HotelImage> CreateAsync(HotelImage dbRecord)
        {
            _context.HotelImages.Add(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }

        public async Task<bool> DeleteAsync(HotelImage dbRecord)
        {
            _context.HotelImages.Remove(dbRecord);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<HotelImage>> GetAllAsync()
        {
            return await _context.HotelImages.ToListAsync();
        }

        public async Task<HotelImage> GetAsync(Expression<Func<HotelImage, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _context.HotelImages.AsNoTracking().Where(filter).FirstOrDefaultAsync();
            else
                return await _context.HotelImages.Where(filter).FirstOrDefaultAsync();
        }

        public async Task<HotelImage> UpdateAsync(HotelImage dbRecord)
        {
            _context.HotelImages.Update(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }
    }
}
