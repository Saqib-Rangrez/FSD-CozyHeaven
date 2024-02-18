using CozyHavenStayServer.Context;
using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CozyHavenStayServer.Repositories
{
    public class HotelImageRepository : IRepository<HotelImage>
    {
        private readonly CozyHeavenStayContext _context;
        public HotelImageRepository(CozyHeavenStayContext context)
        {
            _context = context;
        }

        public Task<HotelImage> CreateAsync(HotelImage dbRecord)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(HotelImage dbRecord)
        {
            throw new NotImplementedException();
        }

        public Task<List<HotelImage>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<HotelImage> GetAsync(Expression<Func<HotelImage, bool>> filter, bool useNoTracking = false)
        {
            throw new NotImplementedException();
        }

        public Task<HotelImage> UpdateAsync(HotelImage dbRecord)
        {
            throw new NotImplementedException();
        }
    }
}
