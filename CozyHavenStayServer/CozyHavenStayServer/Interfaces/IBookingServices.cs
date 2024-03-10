using CozyHavenStayServer.Models;

namespace CozyHavenStayServer.Interfaces
{
    public interface IBookingServices
    {
        public Task<List<Booking>> GetAllBookingsAsync();
        public Task<Booking> GetBookingByIdAsync(int id);
        public Task<List<Booking>> GetBookingByUserIdAsync(int id);
        public Task<Booking> CreateBookingAsync(Booking booking);
        public Task<bool> UpdateBookingAsync(Booking booking);
        public Task<bool> DeleteBookingAsync(int id);
    }
}
