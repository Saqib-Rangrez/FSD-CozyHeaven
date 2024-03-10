using CozyHavenStayServer.Context;
using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace CozyHavenStayServer.Repositories
{
    public class RefundRepository : IRepository<Refund>
    {
        private readonly CozyHeavenStayContext _context;
        public RefundRepository(CozyHeavenStayContext context)
        {
            _context = context;
        }

        public async Task<Refund> CreateAsync(Refund dbRecord)
        {
            _context.Add(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }

        public async Task<bool> DeleteAsync(Refund dbRecord)
        {
            _context.Remove(dbRecord);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Refund>> GetAllAsync()
        {
            return await _context.Refunds  
            .Include(re => re.Payment)      
            .ToListAsync();
        }

        public async Task<Refund> GetAsync(Expression<Func<Refund, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _context.Refunds.AsNoTracking().Where(filter)
                .Include(re => re.Payment)
                .FirstOrDefaultAsync();
            else
                return await _context.Refunds.Where(filter)
                .Include(re => re.Payment)
                .FirstOrDefaultAsync();

        }

        public async Task<Refund> UpdateAsync(Refund dbRecord)
        {
            _context.Refunds.Update(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }
    }
}
