using CozyHavenStayServer.Models.DTO;
using CozyHavenStayServer.Models;

namespace CozyHavenStayServer.Mappers
{
    public class RegisterToRoomImage
    {
        RoomImage roomImage;

        public RegisterToRoomImage(RoomImageDTO hotelImageDTO)
        {
            roomImage = new RoomImage();
            roomImage.ImageUrl = hotelImageDTO.ImageUrl;
            roomImage.RoomId = hotelImageDTO.RoomId;
        }

        public RoomImage GetRoomImage()
        {
            return roomImage;
        }
    }
}
