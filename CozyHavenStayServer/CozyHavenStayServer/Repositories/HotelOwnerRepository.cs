using CozyHavenStayServer.Context;
using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace CozyHavenStayServer.Repositories
{
    public class HotelOwnerRepository : IRepository<HotelOwner>
    {
        private readonly CozyHeavenStayContext _context;
        public HotelOwnerRepository(CozyHeavenStayContext context)
        {
            _context = context;
        }

        public async Task<HotelOwner> CreateAsync(HotelOwner dbRecord)
        {
            _context.Add(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }

        public async Task<bool> DeleteAsync(HotelOwner dbRecord)
        {
            _context.Remove(dbRecord);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<HotelOwner>> GetAllAsync()
        {
            return await _context.HotelOwners.ToListAsync();
        }

        public async Task<HotelOwner> GetAsync(Expression<Func<HotelOwner, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _context.HotelOwners.AsNoTracking().Where(filter).FirstOrDefaultAsync();
            else
                return await _context.HotelOwners.Where(filter).FirstOrDefaultAsync();
        }

        public async Task<HotelOwner> UpdateAsync(HotelOwner dbRecord)
        {
            _context.HotelOwners.Update(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }
    }
}
