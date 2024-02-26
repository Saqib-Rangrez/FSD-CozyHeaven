using CozyHavenStayServer.Models.DTO;
using CozyHavenStayServer.Models;

namespace CozyHavenStayServer.Mappers
{
    public class RegisterToHotelImage
    {
        HotelImage hotelImage;

        public RegisterToHotelImage(HotelImageDTO hotelImageDTO)
        {
            hotelImage = new HotelImage();
            hotelImage.ImageUrl = hotelImageDTO.ImageUrl;
            hotelImage.HotelId = hotelImageDTO.HotelId;
        }

        public HotelImage GetHotelImage()
        {
            return hotelImage;
        }
    }
}
