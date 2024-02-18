using CozyHavenStayServer.Context;
using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using Microsoft.EntityFrameworkCore;
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

        public Task<Hotel> CreateAsync(Hotel dbRecord)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Hotel dbRecord)
        {
            throw new NotImplementedException();
        }

        public Task<List<Hotel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Hotel> GetAsync(Expression<Func<Hotel, bool>> filter, bool useNoTracking = false)
        {
            throw new NotImplementedException();
        }

        public Task<Hotel> UpdateAsync(Hotel dbRecord)
        {
            throw new NotImplementedException();
        }
    }
}
