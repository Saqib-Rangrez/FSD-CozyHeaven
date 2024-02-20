using CozyHavenStayServer.Controllers;
using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;

namespace CozyHavenStayServer.Services
{
    public class HotelServices : IHotelServices
    {
        private readonly ILogger<HotelServices> _logger;
        private readonly IRepository<Hotel> _hotelRepository;

        public HotelServices(ILogger<HotelServices> logger, IRepository<Hotel> hotelRepository)
        {
            _logger = logger;
            _hotelRepository = hotelRepository;
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


        public async Task<Hotel> CreateHotelAsync(Hotel hotel)
        {
            try
            {
                var createdHotel = await _hotelRepository.CreateAsync(hotel);
                return createdHotel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateHotelAsync(Hotel hotel)
        {
            try
            {
                var hotelUser = await _hotelRepository.GetAsync(item => item.HotelId == hotel.HotelId, true);

                if (hotelUser == null)
                {
                    _logger.LogError("Hotel not found with given Id");
                    return false;
                }

                await _hotelRepository.UpdateAsync(hotel);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteHotelAsync(int id)
        {
            try
            {
                var hotel = await _hotelRepository.GetAsync(r => r.HotelId == id, false);

                if (hotel == null)
                {
                    _logger.LogError("hotel not found with the given ID");
                    return false;
                }

                await _hotelRepository.DeleteAsync(hotel);
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
