using CozyHavenStayServer.Context;
using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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

        public async Task<Review> CreateAsync(Review dbRecord)
        {
            _context.Reviews.Add(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }

        public async Task<bool> DeleteAsync(Review dbRecord)
        {
            _context.Reviews.Remove(dbRecord);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Review>> GetAllAsync()
        {
            return await _context.Reviews
            .Include(r => r.User)
                .Include(r => r.Hotel)
                    .ThenInclude(h => h.HotelImages)
                .Include(r => r.Hotel)
                    .ThenInclude(h => h.Rooms)
                        .ThenInclude(r => r.RoomImages)
            .ToListAsync();
        }

        public async Task<Review> GetAsync(Expression<Func<Review, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _context.Reviews.AsNoTracking().Where(filter)
                .Include(r => r.User)
                .Include(r => r.Hotel)
                    .ThenInclude(h => h.HotelImages)
                .Include(r => r.Hotel)
                    .ThenInclude(h => h.Rooms)
                        .ThenInclude(r => r.RoomImages)
                .FirstOrDefaultAsync();
            else
                return await _context.Reviews.Where(filter)
                .Include(r => r.User)
                .Include(r => r.Hotel)
                    .ThenInclude(h => h.HotelImages)
                .Include(r => r.Hotel)
                    .ThenInclude(h => h.Rooms)
                        .ThenInclude(r => r.RoomImages)
                .FirstOrDefaultAsync();
        }

        
        public async Task<Review> UpdateAsync(Review dbRecord)
        {
            _context.Reviews.Update(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }
    }
}
