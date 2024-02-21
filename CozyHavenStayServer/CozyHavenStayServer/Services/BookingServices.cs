using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using CozyHavenStayServer.Repositories;

namespace CozyHavenStayServer.Services
{
    public class BookingServices : IBookingServices
    {
        private readonly ILogger<BookingServices> _logger;
        private readonly IRepository<Booking> _bookingRepository;

        public BookingServices(ILogger<BookingServices> logger, IRepository<Booking> bookingRepository)
        {
            _logger = logger;
            _bookingRepository = bookingRepository;
        }

        public async Task<List<Booking>> GetAllBookingsAsync()
        {
            try
            {
                var bookings = await _bookingRepository.GetAllAsync();
                return bookings;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<Booking> GetBookingByIdAsync(int id)
        {
            try
            {
                var booking = await _bookingRepository.GetAsync(booking => booking.BookingId == id, false);

                if (booking == null)
                {
                    _logger.LogError("Booking not found with given Id");
                    return null;
                }
                return booking;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            try
            {
                var createdBooking = await _bookingRepository.CreateAsync(booking);
                if(createdBooking == null)
                {
                    _logger.LogError("Failed to add Booking");
                    return null;
                }
                return createdBooking;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateBookingAsync(Booking booking)
        {
            try
            {
                var bookingUser = await _bookingRepository.GetAsync(item => item.BookingId == booking.BookingId, true);

                if (bookingUser == null)
                {
                    _logger.LogError("Booking not found with given Id");
                    return false;
                }

                await _bookingRepository.UpdateAsync(booking);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteBookingAsync(int id)
        {
            try
            {
                var booking = await _bookingRepository.GetAsync(booking => booking.BookingId == id, false);

                if (booking == null)
                {
                    _logger.LogError("Booking not found with given Id");
                    return false;
                }

                await _bookingRepository.DeleteAsync(booking);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
