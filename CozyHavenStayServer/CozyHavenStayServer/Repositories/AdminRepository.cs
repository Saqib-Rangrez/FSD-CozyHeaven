using CozyHavenStayServer.Context;
using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using Microsoft.EntityFrameworkCore;
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

        public Task<Admin> CreateAsync(Admin dbRecord)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Admin dbRecord)
        {
            throw new NotImplementedException();
        }

        public Task<List<Admin>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Admin> GetAsync(Expression<Func<Admin, bool>> filter, bool useNoTracking = false)
        {
            throw new NotImplementedException();
        }

        public Task<Admin> UpdateAsync(Admin dbRecord)
        {
            throw new NotImplementedException();
        }
    }
}
