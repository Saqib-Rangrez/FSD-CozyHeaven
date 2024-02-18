using CozyHavenStayServer.Context;
using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CozyHavenStayServer.Repositories
{
    public class BookingRepository : IRepository<Booking>
    {
        private readonly CozyHeavenStayContext _context;
        public BookingRepository(CozyHeavenStayContext context)
        {
            _context = context;
        }

        public Task<Booking> CreateAsync(Booking dbRecord)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Booking dbRecord)
        {
            throw new NotImplementedException();
        }

        public Task<List<Booking>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Booking> GetAsync(Expression<Func<Booking, bool>> filter, bool useNoTracking = false)
        {
            throw new NotImplementedException();
        }

        public Task<Booking> UpdateAsync(Booking dbRecord)
        {
            throw new NotImplementedException();
        }
    }
}
