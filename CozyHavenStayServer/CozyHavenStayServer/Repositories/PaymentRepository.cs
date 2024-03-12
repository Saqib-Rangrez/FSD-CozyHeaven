using CozyHavenStayServer.Context;
using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace CozyHavenStayServer.Repositories
{
    public class PaymentRepository : IRepository<Payment>
    {
        private readonly CozyHeavenStayContext _context;
        public PaymentRepository(CozyHeavenStayContext context)
        {
            _context = context;
        }
        public async Task<Payment> CreateAsync(Payment dbRecord)
        {
            _context.Add(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }

        public async Task<bool> DeleteAsync(Payment dbRecord)
        {
            _context.Remove(dbRecord);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Payment>> GetAllAsync()
        {
            return await _context.Payments
            .Include(p => p.Refund)
            .Include(p => p.Booking)
            .AsSplitQuery()
            .ToListAsync();
        }

        public async Task<Payment> GetAsync(Expression<Func<Payment, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _context.Payments.AsNoTracking().Where(filter)
                .Include(p => p.Refund)
                .Include(p => p.Booking)
                .AsSplitQuery()
                .FirstOrDefaultAsync();
            else
                return await _context.Payments.Where(filter)
                .Include(p => p.Refund)
                .Include(p => p.Booking)
                .AsSplitQuery()
                .FirstOrDefaultAsync();

        }

        public async Task<Payment> UpdateAsync(Payment dbRecord)
        {
            _context.Payments.Update(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }
    }
}
