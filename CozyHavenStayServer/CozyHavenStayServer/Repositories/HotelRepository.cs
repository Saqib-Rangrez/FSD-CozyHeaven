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

        
    }
}
