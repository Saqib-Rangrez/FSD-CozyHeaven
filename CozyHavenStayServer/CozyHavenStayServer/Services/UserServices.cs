﻿using CozyHavenStayServer.Controllers;
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
        private readonly IRepository<Hotel> _hotelRepository;
        private readonly IRepository<Room> _roomRepository;
        private readonly IRepository<Booking> _bookingRepository;

        public UserServices(IRepository<User> userRepository, ILogger<UserController> logger, IRepository<Hotel> hotelRepository, IRepository<Booking> bookingRepository, IRepository<Room> roomRepository)
        {
            _userRepository = userRepository;
            _logger = logger;
            _hotelRepository = hotelRepository;
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
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


        public async Task<List<Room>> GetAllRoomsAsync()
        {
            try
            {
                var rooms = await _roomRepository.GetAllAsync();
                return rooms;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
        public async Task<Room> GetRoomByIdAsync(int id)
        {
            try
            {
                var room = await _roomRepository.GetAsync(r => r.RoomId == id, false);

                if (room == null)
                {
                    _logger.LogError("Room not found with the given ID");
                    return null;
                }
                return room;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<Room> CreateRoomAsync(Room room)
        {
            try
            {
                var createdRoom = await _roomRepository.CreateAsync(room);
                return createdRoom;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateRoomAsync(Room room)
        {
            try
            {
                var existingRoom = await _roomRepository.GetAsync(r => r.RoomId == room.RoomId, true);

                if (existingRoom == null)
                {
                    _logger.LogError("Room not found with the given ID");
                    return false;
                }

                await _roomRepository.UpdateAsync(room);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteRoomAsync(int id)
        {
            try
            {
                var room = await _roomRepository.GetAsync(r => r.RoomId == id, false);

                if (room == null)
                {
                    _logger.LogError("Room not found with the given ID");
                    return false;
                }

                await _roomRepository.DeleteAsync(room);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<List<Hotel>> GetAllHotelsAsync()
        {
            try
            {
                var rooms = await _hotelRepository.GetAllAsync();
                return rooms;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<Hotel> GetHotelByIdAsync(int id)
        {
            try
            {
                var room = await _hotelRepository.GetAsync(r => r.HotelId == id, false);

                if (room == null)
                {
                    _logger.LogError("Hotel not found with the given ID");
                    return null;
                }
                return room;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<Hotel> GetHotelByNameAsync(string name)
        {
            var user = await _hotelRepository.GetAsync(hotel => hotel.Name.Contains(name), false);

            if (user == null)
            {
                _logger.LogError("Hotel not found with given name");
                return null;
            }
            return user;
        }
    }
}
