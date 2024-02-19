using CozyHavenStayServer.Context;
using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace CozyHavenStayServer.Repositories
{
    public class AdminRepository : IRepository<Admin>
    {
        private readonly CozyHeavenStayContext _context;
        public AdminRepository(CozyHeavenStayContext context)
        {
            _context = context;
        }

        public async Task<Admin> CreateAsync(Admin dbRecord)
        {
            _context.Add(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }

        public async Task<bool> DeleteAsync(Admin dbRecord)
        {
            _context.Remove(dbRecord);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Admin>> GetAllAsync()
        {
            return await _context.Admins.ToListAsync();
        }

        public async Task<Admin> GetAsync(Expression<Func<Admin, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _context.Admins.AsNoTracking().Where(filter).FirstOrDefaultAsync();
            else
                return await _context.Admins.Where(filter).FirstOrDefaultAsync();
        }

        public async Task<Admin> UpdateAsync(Admin dbRecord)
        {
            _context.Admins.Update(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }
    }
}
