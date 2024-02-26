using CozyHavenStayServer.Models.DTO;
using CozyHavenStayServer.Models;

namespace CozyHavenStayServer.Mappers
{
    public class RegisterToRoom
    {
        Room room;

        public RegisterToRoom(RoomDTO registerRoom)
        {
            room = new Room();
            room.HotelId = registerRoom.HotelId;
            room.BaseFare = registerRoom.BaseFare;
            room.RoomSize = registerRoom.RoomSize;
            room.Acstatus = registerRoom.Acstatus;
            room.BedType = registerRoom.BedType;
            room.RoomType = registerRoom.RoomType;
            room.MaxOccupancy = registerRoom.MaxOccupancy;
        }

        public Room GetRoom()
        {
            return room;
        }
    }
}
