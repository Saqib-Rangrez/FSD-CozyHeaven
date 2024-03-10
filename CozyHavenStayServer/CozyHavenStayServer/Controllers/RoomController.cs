using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Mappers;
using CozyHavenStayServer.Models;
using CozyHavenStayServer.Models.DTO;
using CozyHavenStayServer.Services;
using Microsoft.AspNetCore.Authorization;
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
        private readonly ICloudinaryService _cloudinaryService;

        public RoomController(IRoomServices roomServices, ILogger<RoomController> logger, ICloudinaryService cloudinaryService)
        {
            _roomServices = roomServices;
            _logger = logger;
            _cloudinaryService = cloudinaryService;
        }


        // Get all rooms
        [Authorize(Roles = "Admin, User, Owner")]
        [HttpGet]
        [Route("GetAllRooms")]
        public async Task<ActionResult<List<Room>>> GetAllRoomsAsync()
        {
            try
            {
                var rooms = await _roomServices.GetAllRoomsAsync();
                if (rooms == null || rooms.Count <= 0)
                {
                    return NotFound( new
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
        [Authorize(Roles = "Admin, User, Owner")]
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
        /*        [HttpPost]
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
                }*/
        [Authorize(Roles = "Owner")]
        [HttpPost]
        [Route("CreateRoom")]
        public async Task<ActionResult<Room>> CreateRoomAsync([FromForm] RoomDTO model)
        {
            try
            {
                if (model == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Error in details"
                    });
                }


                if (model.Files == null || model.Files.Count <= 0)
                {
                    return BadRequest("No file provided.");
                }

                RegisterToRoom registerToRoom = new RegisterToRoom(model);

                var roomToAdd = registerToRoom.GetRoom();
                var createdRoom = await _roomServices.CreateRoomAsync(roomToAdd);

                if (createdRoom != null)
                {
                    foreach (var file in model.Files)
                    {
                        var uploadResult = await _cloudinaryService.UploadImageAsync(file);

                        if (uploadResult != null)
                        {
                            RoomImageDTO roomImageDTO = new RoomImageDTO()
                            {
                                RoomId = createdRoom.RoomId,
                                ImageUrl = uploadResult.SecureUrl.ToString(),
                            };
      
                            RegisterToRoomImage registerToRoomImage = new RegisterToRoomImage(roomImageDTO);

                            var imageToAdd = registerToRoomImage.GetRoomImage();
                            var addedImage = await _roomServices.AddRoomImageAsync(imageToAdd);

                            if (addedImage == null)
                            {
                                return StatusCode(500, new
                                {
                                    success = false,
                                    message = "Error while adding image",
                                    data = uploadResult.SecureUrl
                                });
                            }
                        }
                    }
                }
                return Ok(new
                {
                    success = true,
                    message = "Room Added Successfully.",
                    data = createdRoom
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error, while adding room"
                });
            }
        }

        // Update room
        [Authorize(Roles = "Owner")]
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
                        error = "Failed to update room, Room with given room id not found"
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
        [Authorize(Roles = "Owner")]
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
