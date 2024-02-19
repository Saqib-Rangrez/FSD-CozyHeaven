using CozyHavenStayServer.Controllers;
using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using CozyHavenStayServer.Repositories;

namespace CozyHavenStayServer.Services
{
    public class HotelOwnerServices : IHotelOwnerServices
    {
        private readonly IRepository<HotelOwner> _hotelOwnerRepository;
        private readonly ILogger<HotelOwnerController> _logger;
        private readonly IHotelServices _hotelRepository;

        public HotelOwnerServices(IRepository<HotelOwner> hotelOwnerRepository, ILogger<HotelOwnerController> logger, IHotelServices hotelRepository)
        {
            _hotelOwnerRepository = hotelOwnerRepository;
            _logger = logger;
            _hotelRepository = hotelRepository;
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

                var availableRooms = await _hotelRepository.SearchHotelRoomsAsync(location, checkInDate, checkOutDate, numberOfRooms);
                return availableRooms;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }
    }
}
