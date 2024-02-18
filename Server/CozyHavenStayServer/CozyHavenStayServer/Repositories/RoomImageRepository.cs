using CozyHavenStayServer.Context;
using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CozyHavenStayServer.Repositories
{
    public class RoomImageRepository : IRepository<RoomImage>
    {
        private readonly CozyHeavenStayContext _context;
        public RoomImageRepository(CozyHeavenStayContext context)
        {
            _context = context;
        }

        public Task<RoomImage> CreateAsync(RoomImage dbRecord)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(RoomImage dbRecord)
        {
            throw new NotImplementedException();
        }

        public Task<List<RoomImage>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<RoomImage> GetAsync(Expression<Func<RoomImage, bool>> filter, bool useNoTracking = false)
        {
            throw new NotImplementedException();
        }

        public Task<RoomImage> UpdateAsync(RoomImage dbRecord)
        {
            throw new NotImplementedException();
        }
    }
}
