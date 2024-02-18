using CozyHavenStayServer.Context;
using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CozyHavenStayServer.Repositories
{
    public class ReviewRepository : IRepository<Review>
    {
        private readonly CozyHeavenStayContext _context;
        public ReviewRepository(CozyHeavenStayContext context)
        {
            _context = context;
        }

        public Task<Review> CreateAsync(Review dbRecord)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Review dbRecord)
        {
            throw new NotImplementedException();
        }

        public Task<List<Review>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Review> GetAsync(Expression<Func<Review, bool>> filter, bool useNoTracking = false)
        {
            throw new NotImplementedException();
        }

        public Task<Review> UpdateAsync(Review dbRecord)
        {
            throw new NotImplementedException();
        }
    }
}
