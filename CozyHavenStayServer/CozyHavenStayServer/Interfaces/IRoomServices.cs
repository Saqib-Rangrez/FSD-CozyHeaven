using CozyHavenStayServer.Models;

namespace CozyHavenStayServer.Interfaces
{
    public interface IRoomServices
    {
        public Task<List<Room>> GetAllRoomsAsync();
        public Task<Room> GetRoomByIdAsync(int id);
        public Task<Room> CreateRoomAsync(Room room);
        public Task<bool> UpdateRoomAsync(Room room);
        public Task<bool> DeleteRoomAsync(int id);
    }
}
