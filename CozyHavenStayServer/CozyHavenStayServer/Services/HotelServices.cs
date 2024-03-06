using CloudinaryDotNet.Actions;
using CozyHavenStayServer.Controllers;
using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using CozyHavenStayServer.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace CozyHavenStayServer.Services
{
    public class HotelServices : IHotelServices
    {
        private readonly ILogger<HotelServices> _logger;
        private readonly IRepository<Hotel> _hotelRepository;
        private readonly IRepository<HotelImage> _hotelImageRepository; 


        public HotelServices(ILogger<HotelServices> logger, IRepository<Hotel> hotelRepository, IRepository<HotelImage> hotelImageRepository)
        {
            _logger = logger;
            _hotelRepository = hotelRepository;
            _hotelImageRepository = hotelImageRepository;
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
            var hotel = await _hotelRepository.GetAsync(hotel => hotel.Name.Equals(name , StringComparison.OrdinalIgnoreCase), false);

            if (hotel == null)
            {
                _logger.LogError("Hotel not found with given name");
                return null;
            }
            return hotel;
        }


        public async Task<Hotel> CreateHotelAsync(Hotel hotel)
        {
            try
            {
                var createdHotel = await _hotelRepository.CreateAsync(hotel);
                if(createdHotel == null)
                {
                    _logger.LogError("Failed to add Hotel");
                    return null;
                }
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

        public async Task<List<Hotel>> SearchHotelsAsync(SearchHotelDTO searchHotelDTO)
        {
            var allHotels = await _hotelRepository.GetAllAsync();
            var filteredHotels = allHotels;
            if (!string.IsNullOrEmpty(searchHotelDTO.Location))
            {
                filteredHotels = allHotels.Where(h => h.Location.Contains(searchHotelDTO.Location, StringComparison.OrdinalIgnoreCase)).ToList();
            }
          
            if (filteredHotels == null || filteredHotels.Count <= 0)
            {
                return null;
            }

            if(searchHotelDTO.CheckInDate != null && searchHotelDTO.CheckOutDate != null)
            {
                filteredHotels = filteredHotels.Where(h => h.Rooms.Any(r =>
                !r.Bookings.Any(b =>
                    (searchHotelDTO.CheckInDate >= b.CheckInDate && searchHotelDTO.CheckInDate < b.CheckOutDate) ||
                    (searchHotelDTO.CheckOutDate > b.CheckInDate && searchHotelDTO.CheckOutDate <= b.CheckOutDate)
                ))).ToList();
            }
            
            if (filteredHotels == null || filteredHotels.Count <= 0)
            {
                return null;
            }

            if (searchHotelDTO.NumberOfRooms != null)
            {
                filteredHotels = filteredHotels.Where(h => h.Rooms.Count >= searchHotelDTO.NumberOfRooms).ToList();
            }
            if (filteredHotels == null || filteredHotels.Count <= 0)
            {
                return null;
            }

            if(searchHotelDTO.NumberOfChildren != null && searchHotelDTO.NumberOfAdults != null)
            {
                int? totalPersons = searchHotelDTO.NumberOfAdults + searchHotelDTO.NumberOfChildren;
                filteredHotels = filteredHotels.Where(h => h.Rooms.Any(r => r.MaxOccupancy >= totalPersons)).ToList();
            }

            if (filteredHotels == null || filteredHotels.Count <= 0)
            {
                return null;
            }

            return filteredHotels;
        }

        public async Task<HotelImage> AddHotelImageAsync(HotelImage hotelImage)
        {
            try
            {
                var createdImage = await _hotelImageRepository.CreateAsync(hotelImage);
                if (createdImage == null)
                {
                    _logger.LogError("Failed to add Image");
                    return null;
                }
                return createdImage;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }


    }
}
