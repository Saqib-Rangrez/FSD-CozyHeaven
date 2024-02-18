using CozyHavenStayServer.Context;
using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CozyHavenStayServer.Repositories
{
    public class RoomRepository : IRepository<Room>
    {
        private readonly CozyHeavenStayContext _context;
        public RoomRepository(CozyHeavenStayContext context)
        {
            _context = context;
        }

        public Task<Room> CreateAsync(Room dbRecord)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Room dbRecord)
        {
            throw new NotImplementedException();
        }

        public Task<List<Room>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Room> GetAsync(Expression<Func<Room, bool>> filter, bool useNoTracking = false)
        {
            throw new NotImplementedException();
        }

        public Task<Room> UpdateAsync(Room dbRecord)
        {
            throw new NotImplementedException();
        }
    }
}
