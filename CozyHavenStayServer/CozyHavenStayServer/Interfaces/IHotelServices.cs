using CozyHavenStayServer.Models;

namespace CozyHavenStayServer.Interfaces
{
    public interface IHotelServices
    {
        Task<List<Hotel>> SearchHotelsAsync(string location, string amenities);
        Task<List<Room>> SearchHotelRoomsAsync(string location, DateTime checkInDate, DateTime checkOutDate, int numberOfRooms);
    }
}
