﻿using CozyHavenStayServer.Context;
using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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

        public async Task<Booking> CreateAsync(Booking dbRecord)
        {
            _context.Add(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }

        public async Task<bool> DeleteAsync(Booking dbRecord)
        {
            _context.Remove(dbRecord);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Booking>> GetAllAsync()
        {
            return await _context.Bookings.ToListAsync();
        }

        public async Task<Booking> GetAsync(Expression<Func<Booking, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _context.Bookings.AsNoTracking().Where(filter).FirstOrDefaultAsync();
            else
                return await _context.Bookings.Where(filter).FirstOrDefaultAsync();
        }

        public async Task<Booking> UpdateAsync(Booking dbRecord)
        {
            _context.Bookings.Update(dbRecord);
            await _context.SaveChangesAsync();
            return dbRecord;
        }


    }
}
