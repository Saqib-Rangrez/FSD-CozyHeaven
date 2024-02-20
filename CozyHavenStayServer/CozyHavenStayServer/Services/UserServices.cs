using CozyHavenStayServer.Controllers;
using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using CozyHavenStayServer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CozyHavenStayServer.Services
{
    public class UserServices : IUserServices
    {
        private readonly IRepository<User> _userRepository;
        private readonly ILogger<UserController> _logger;
        private readonly HotelRepository _hotelRepository;
        private readonly IRepository<Booking> _bookingRepository;

        public UserServices(IRepository<User> userRepository, ILogger<UserController> logger, HotelRepository hotelRepository, IRepository<Booking> bookingRepository)
        {
            _userRepository = userRepository;
            _logger = logger;
            _hotelRepository = hotelRepository;
            _bookingRepository = bookingRepository;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();
                return users;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await _userRepository.GetAsync(user => user.UserId == id, false);

                if (user == null)
                {
                    _logger.LogError("User not found with given Id");
                    return null;
                }
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<User> GetUserByNameAsync(string name)
        {
            var user = await _userRepository.GetAsync(user => user.FirstName.Contains(name) || user.LastName.Contains(name), false);

            if (user == null)
            {
                _logger.LogError("User not found with given name");
                return null;
            }
            return user;
        }

        public async Task<User> CreateUserAsync(User model)
        {
            try
            {
                var createdUser = await _userRepository.CreateAsync(model);
                return createdUser;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }            
        }               

        public async Task<bool> UpdateUserAsync(User model)
        {
            try
            {
                var user = await _userRepository.GetAsync(item => item.UserId == model.UserId, true);

                if (user == null)
                {
                    _logger.LogError("User not found with given Id");
                    return false;
                }

                await _userRepository.UpdateAsync(model);
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }            
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                var user = await _userRepository.GetAsync(user => user.UserId == id, false);

                if (user == null)
                {
                    _logger.LogError("User not found with given Id");
                    return false;
                }

                await _userRepository.DeleteAsync(user);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<List<Hotel>> SearchHotelsAsync(string location, string amenities)
        {
            try
            {
                var hotels = await _hotelRepository.SearchHotelsAsync(location, amenities);
                if (hotels == null)
                {
                    _logger.LogError("Hotel not found");
                    return null;
                }

                return hotels;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<List<Room>> SearchHotelRoomsAsync(string location, DateTime checkInDate, DateTime checkOutDate, int numberOfRooms)
        {
            try
            {
                // Validate input parameters
                if (string.IsNullOrWhiteSpace(location) || checkInDate >= checkOutDate || numberOfRooms <= 0)
                    throw new ArgumentException("Invalid input parameters");

                var availableRooms = await _hotelRepository.SearchHotelRoomsAsync(location, checkInDate, checkOutDate, numberOfRooms);
                return availableRooms;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
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
