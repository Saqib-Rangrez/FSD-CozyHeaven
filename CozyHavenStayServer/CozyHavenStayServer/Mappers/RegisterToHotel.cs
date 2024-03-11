using CozyHavenStayServer.Models.DTO;
using CozyHavenStayServer.Models;

namespace CozyHavenStayServer.Mappers
{
    public class RegisterToHotel
    {
        Hotel hotel;

        public RegisterToHotel(HotelDTO registerHotel)
        {
            hotel = new Hotel();
            hotel.HotelId = registerHotel.HotelId;
            hotel.Name = registerHotel.Name;
            hotel.OwnerId = registerHotel.OwnerId;
            hotel.Amenities = registerHotel.Amenities;
            hotel.Location = registerHotel.Location;
            hotel.Description = registerHotel.Description;
        }

        public Hotel GetHotel()
        {
            return hotel;
        }
    }
}
