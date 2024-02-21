using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using CozyHavenStayServer.Repositories;

namespace CozyHavenStayServer.Services
{
    public class RoomServices : IRoomServices
    {
        private readonly ILogger<RoomServices> _logger;
        private readonly IRepository<Room> _roomRepository;

        public RoomServices(ILogger<RoomServices> logger, IRepository<Room> roomRepository)
        {
            _logger = logger;
            _roomRepository = roomRepository;
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
                if(createdRoom == null)
                {
                    _logger.LogError("Failed to add Hotel");
                    return null;
                }
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
