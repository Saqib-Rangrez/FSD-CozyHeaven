﻿using CozyHavenStayServer.Controllers;
using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using CozyHavenStayServer.Repositories;

namespace CozyHavenStayServer.Services
{
    public class HotelOwnerServices : IHotelOwnerServices
    {
        private readonly IRepository<HotelOwner> _hotelOwnerRepository;
        private readonly ILogger<HotelOwnerController> _logger;
        private readonly HotelRepository _hotelRepository;
        private readonly RoomRepository _roomRepository;
        private readonly IRepository<Booking> _bookingRepository;

        public HotelOwnerServices(IRepository<HotelOwner> hotelOwnerRepository, ILogger<HotelOwnerController> logger, HotelRepository hotelRepository, IRepository<Booking> bookingRepository, RoomRepository roomRepository)
        {
            _hotelOwnerRepository = hotelOwnerRepository;
            _logger = logger;
            _hotelRepository = hotelRepository;
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
        }
        public async Task<HotelOwner> CreateHotelOwnerAsync(HotelOwner hotelOwner)
        {
            try
            {
                var createdHotelOwner = await _hotelOwnerRepository.CreateAsync(hotelOwner);
                return createdHotelOwner;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> DeleteHotelOwnerAsync(int id)
        {
            try
            {
                var hotelOwner = await _hotelOwnerRepository.GetAsync(hotelOwner => hotelOwner.OwnerId == id, false);

                if (hotelOwner == null)
                {
                    _logger.LogError("HotelOwner not found with given Id");
                    return false;
                }

                await _hotelOwnerRepository.DeleteAsync(hotelOwner);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<List<HotelOwner>> GetAllHotelOwnersAsync()
        {
            try
            {
                var hotelOwners = await _hotelOwnerRepository.GetAllAsync();
                return hotelOwners;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<HotelOwner> GetHotelOwnerByIdAsync(int id)
        {
            try
            {
                var hotelOwner = await _hotelOwnerRepository.GetAsync(hotelOwner => hotelOwner.OwnerId == id, false);

                if (hotelOwner == null)
                {
                    _logger.LogError("HotelOwner not found with given Id");
                    return null;
                }
                return hotelOwner;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<HotelOwner> GetHotelOwnerByNameAsync(string name)
        {
            var hotelOwner = await _hotelOwnerRepository.GetAsync(hotelOwner => hotelOwner.FirstName.Contains(name) || hotelOwner.LastName.Contains(name), false);

            if (hotelOwner == null)
            {
                _logger.LogError("HotelOwner not found with given name");
                return null;
            }
            return hotelOwner;
        }

        public async Task<bool> UpdateHotelOwnerAsync(HotelOwner hotelOwner)
        {
            try
            {
                var hotelOwnerUser = await _hotelOwnerRepository.GetAsync(item => item.OwnerId == hotelOwner.OwnerId, true);

                if (hotelOwner == null)
                {
                    _logger.LogError("HotelOwner not found with given Id");
                    return false;
                }

                await _hotelOwnerRepository.UpdateAsync(hotelOwner);
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

                var availableRooms = await _roomRepository.SearchHotelRoomsAsync(location, checkInDate, checkOutDate, numberOfRooms);
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



    }
}
