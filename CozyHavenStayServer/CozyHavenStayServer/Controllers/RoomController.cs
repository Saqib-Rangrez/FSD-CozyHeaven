using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using CozyHavenStayServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CozyHavenStayServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomServices _roomServices;
        private readonly ILogger<RoomController> _logger;

        public RoomController(IRoomServices roomServices, ILogger<RoomController> logger)
        {
            _roomServices = roomServices;
            _logger = logger;
        }


        // Get all rooms
        [HttpGet]
        [Route("GetAllRooms")]
        public async Task<ActionResult<List<Room>>> GetAllRoomsAsync()
        {
            try
            {
                var rooms = await _roomServices.GetAllRoomsAsync();
                if (rooms == null || rooms.Count <= 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new
                    {
                        success = false,
                        error = "No data found"
                    });
                }
                return Ok(new
                {
                    success = true,
                    data = rooms
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while retrieving all rooms"
                });
            }
        }

        // Get room by ID
        [HttpGet]
        [Route("GetRoomById/{id}")]
        public async Task<ActionResult<Room>> GetRoomByIdAsync(int id)
        {
            try
            {
                var room = await _roomServices.GetRoomByIdAsync(id);
                if (room == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = $"Room with ID {id} not found"
                    });
                }
                return Ok(new
                {
                    success = true,
                    data = room
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while retrieving room by ID"
                });
            }
        }

        // Create room
        [HttpPost]
        [Route("CreateRoom")]
        public async Task<ActionResult<Room>> CreateRoomAsync([FromBody] Room room)
        {
            try
            {
                var createdRoom = await _roomServices.CreateRoomAsync(room);
                if (createdRoom == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        error = "Failed to create room"
                    });
                }
                return Ok(new
                {
                    success = true,
                    message = "Room Added Successfully",
                    data = createdRoom
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while creating room"
                });
            }
        }

        // Update room
        [HttpPut]
        [Route("UpdateRoom")]
        public async Task<ActionResult<bool>> UpdateRoomAsync([FromBody] Room room)
        {
            try
            {
                var success = await _roomServices.UpdateRoomAsync(room);
                if (!success)
                {
                    return BadRequest(new
                    {
                        success = false,
                        error = "Failed to update room"
                    });
                }
                return Ok(new
                {
                    success = true,
                    message = "Room updated successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while updating room"
                });
            }
        }

        // Delete room
        [HttpDelete]
        [Route("DeleteRoom/{id}")]
        public async Task<ActionResult<bool>> DeleteRoomAsync(int id)
        {
            try
            {
                var success = await _roomServices.DeleteRoomAsync(id);
                if (!success)
                {
                    return BadRequest(new
                    {
                        success = false,
                        error = "Failed to delete room"
                    });
                }
                return Ok(new
                {
                    success = true,
                    message = "Room deleted successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while deleting room"
                });
            }
        }

    }
}
