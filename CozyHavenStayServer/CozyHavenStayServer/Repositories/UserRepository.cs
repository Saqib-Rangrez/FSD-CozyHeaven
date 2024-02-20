using CozyHavenStayServer.Context;
using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CozyHavenStayServer.Repositories
{
    public class UserRepository : IRepository<User> 
    {
        private readonly CozyHeavenStayContext _context;
        public UserRepository(CozyHeavenStayContext context) {
            _context = context;
        }

        public async Task<User> CreateAsync(User dbRecord)
        {
            _context.Users.Add(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }

        public async Task<bool> DeleteAsync(User dbRecord)
        {
            _context.Users.Remove(dbRecord);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetAsync(Expression<Func<User, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _context.Users.AsNoTracking().Where(filter).FirstOrDefaultAsync();
            else
                return await _context.Users.Where(filter).FirstOrDefaultAsync();
        }

        public async Task<User> GetAsyncByName(Expression<Func<User, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _context.Users.AsNoTracking().Where(filter).FirstOrDefaultAsync();
            else
                return await _context.Users.Where(filter).FirstOrDefaultAsync();
        }

        public async Task<User> UpdateAsync(User dbRecord)
        {
            _context.Users.Update(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }
    }
}
