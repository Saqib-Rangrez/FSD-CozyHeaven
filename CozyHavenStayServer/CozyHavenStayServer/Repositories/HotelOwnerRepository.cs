using CozyHavenStayServer.Context;
using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using Microsoft.EntityFrameworkCore;
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

        public Task<HotelOwner> CreateAsync(HotelOwner dbRecord)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(HotelOwner dbRecord)
        {
            throw new NotImplementedException();
        }

        public Task<List<HotelOwner>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<HotelOwner> GetAsync(Expression<Func<HotelOwner, bool>> filter, bool useNoTracking = false)
        {
            throw new NotImplementedException();
        }

        public Task<HotelOwner> UpdateAsync(HotelOwner dbRecord)
        {
            throw new NotImplementedException();
        }
    }
}
