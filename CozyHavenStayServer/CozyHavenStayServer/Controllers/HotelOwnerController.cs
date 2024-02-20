using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using CozyHavenStayServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CozyHavenStayServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelOwnerController : ControllerBase
    {
        private readonly IHotelOwnerServices _hotelOwnerServices;
        private readonly ILogger<HotelOwnerController> _logger;

        public HotelOwnerController(ILogger<HotelOwnerController> logger, IHotelOwnerServices hotelOwnerServices)
        {
            _logger = logger;
            _hotelOwnerServices = hotelOwnerServices;
        }

        //GetAll
        [HttpGet]
        [Route("GetAllHotelOwners")]
        public async Task<ActionResult<List<HotelOwner>>> GetAllHotelOwnersAsync()
        {
            try
            {
                var hotelOwners = await _hotelOwnerServices.GetAllHotelOwnersAsync();

                if (hotelOwners == null || hotelOwners.Count <= 0)
                {
                    return NotFound(new
                    {
                        success = "False",
                        message = "No data found"
                    });
                }

                return Ok(new
                {
                    success = "True",
                    data = hotelOwners
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = "False",
                    error = "An error occurred while fetching hotelOwners."
                });
            }
        }


        //GetHotelOwnerById
        [HttpGet]
        [Route("GetHotelOwnerById/{id}", Name = "GetHotelOwnerById")]
        public async Task<ActionResult<HotelOwner>> GetHotelOwnerByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = "False",
                        message = "Invalid HotelOwner Id"
                    });
                }
                var hotelOwner = await _hotelOwnerServices.GetHotelOwnerByIdAsync(id);


                if (hotelOwner == null)
                {
                    _logger.LogError("HotelOwner not found with given Id");
                    return NotFound(new
                    {
                        success = "False",
                        message = $"The 'hotelOwner' with Id: {id} not found"
                    });
                }

                return Ok(new
                {
                    success = "True",
                    data = hotelOwner
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = "False",
                    error = "An error occurred while fetching hotelOwner."
                });
            }
        }


        //GetHotelOwnerByName
        [HttpGet]
        [Route("GetHotelOwnerByEmail/{email}")]
        public async Task<ActionResult<HotelOwner>> GetHotelOwnerByEmailAsync(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = "False",
                        message = "Invalid HotelOwner Email"
                    });
                }

                var hotelOwner = await _hotelOwnerServices.GetHotelOwnerByEmailAsync(email);

                if (hotelOwner == null)
                {
                    _logger.LogError("HotelOwner not found with given email");
                    return NotFound(new
                    {
                        success = "False",
                        message = $"The 'hotelOwner' with Email: {email} not found"
                    });
                }

                return Ok(new
                {
                    success = "True",
                    data = hotelOwner
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = "False",
                    error = "An error occurred while fetching hotelOwner."
                });
            }
        }

        //CreateHotelOwner
        [HttpPost]
        [Route("CreateHotelOwner")]
        public async Task<ActionResult<HotelOwner>> CreateHotelOwnerAsync([FromBody] HotelOwner model)
        {
            try
            {
                if (model == null)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = "False",
                        message = "Null Object"
                    });
                }

                var createdHotelOwner = await _hotelOwnerServices.CreateHotelOwnerAsync(model);

                return Ok(new
                {
                    success = "True",
                    data = createdHotelOwner
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = "False",
                    error = "An error occurred while creating hotelOwner."
                });
            }
        }


        //UpdateHotelOwner
        [HttpPut]
        [Route("UpdateHotelOwner")]
        public async Task<ActionResult> UpdateHotelOwnerAsync([FromBody] HotelOwner model)
        {
            try
            {
                if (model == null || model.OwnerId <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = "False",
                        message = "Invalid Data"
                    });
                }

                var hotelOwner = await _hotelOwnerServices.UpdateHotelOwnerAsync(model);

                return Ok(new
                {
                    success = "True",
                    message = "HotelOwner updated successfully"
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = "False",
                    error = "An error occurred while updating hotelOwner."
                });
            }
        }


        //DeleteHotelOwner
        [HttpDelete]
        [Route("/DeleteHotelOwner/{id}")]
        public async Task<ActionResult<bool>> DeleteHotelOwnerAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = "False",
                        message = "Invalid HotelOwner Id"
                    });
                }

                var deleteStatus = await _hotelOwnerServices.DeleteHotelOwnerAsync(id);

                if (deleteStatus)
                {
                    return Ok(new
                    {
                        success = "True",
                        message = "User deleted successfully"
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        success = "False",
                        message = "User Not found"
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = "False",
                    error = "An error occurred while deleting hotelOwner."
                });
            }
        }


        // Get booking by ID
        [HttpGet]
        [Route("GetBookingById/{id}")]
        public async Task<ActionResult<Booking>> GetBookingByIdAsync(int id)
        {
            try
            {
                var booking = await _hotelOwnerServices.GetBookingByIdAsync(id);
                if (booking == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = $"Booking with ID {id} not found"
                    });
                }
                return Ok(new
                {
                    success = true,
                    data = booking
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while retrieving booking by ID"
                });
            }
        }

        // Create booking
        [HttpPost]
        [Route("CreateBooking")]
        public async Task<ActionResult<Booking>> CreateBookingAsync([FromBody] Booking booking)
        {
            try
            {
                var createdBooking = await _hotelOwnerServices.CreateBookingAsync(booking);
                if (createdBooking == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        error = "Failed to create booking"
                    });
                }
                return Ok(new
                {
                    success = true,
                    data = createdBooking
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while creating booking"
                });
            }
        }

        // Update booking
        [HttpPut]
        [Route("UpdateBooking")]
        public async Task<ActionResult<bool>> UpdateBookingAsync([FromBody] Booking booking)
        {
            try
            {
                var success = await _hotelOwnerServices.UpdateBookingAsync(booking);
                if (!success)
                {
                    return BadRequest(new
                    {
                        success = false,
                        error = "Failed to update booking"
                    });
                }
                return Ok(new
                {
                    success = true,
                    message = "Booking updated successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while updating booking"
                });
            }
        }

        // Delete booking
        [HttpDelete]
        [Route("DeleteBooking/{id}")]
        public async Task<ActionResult<bool>> DeleteBookingAsync(int id)
        {
            try
            {
                var success = await _hotelOwnerServices.DeleteBookingAsync(id);
                if (!success)
                {
                    return BadRequest(new
                    {
                        success = false,
                        error = "Failed to delete booking"
                    });
                }
                return Ok(new
                {
                    success = true,
                    message = "Booking deleted successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while deleting booking"
                });
            }
        }


        [HttpGet]
        [Route("GetAllBookings")]
        public async Task<ActionResult<List<Booking>>> GetAllBookingsAsync()
        {
            try
            {
                var bookings = await _hotelOwnerServices.GetAllBookingsAsync();
                if (bookings == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new
                    {
                        success = false,
                        error = "An error occurred while retrieving all bookings"
                    });
                }
                return Ok(new
                {
                    success = true,
                    data = bookings
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while retrieving all bookings"
                });
            }
        }
        // Get all rooms
        [HttpGet]
        [Route("GetAllRooms")]
        public async Task<ActionResult<List<Room>>> GetAllRoomsAsync()
        {
            try
            {
                var rooms = await _hotelOwnerServices.GetAllRoomsAsync();
                if (rooms == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new
                    {
                        success = false,
                        error = "An error occurred while retrieving all rooms"
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
                var room = await _hotelOwnerServices.GetRoomByIdAsync(id);
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
                var createdRoom = await _hotelOwnerServices.CreateRoomAsync(room);
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
                var success = await _hotelOwnerServices.UpdateRoomAsync(room);
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
                var success = await _hotelOwnerServices.DeleteRoomAsync(id);
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


        // Get all hotels
        [HttpGet]
        [Route("GetAllHotels")]
        public async Task<ActionResult<List<Hotel>>> GetAllHotelsAsync()
        {
            try
            {
                var hotels = await _hotelOwnerServices.GetAllHotelsAsync();
                if (hotels == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new
                    {
                        success = false,
                        error = "An error occurred while retrieving all hotels"
                    });
                }
                return Ok(new
                {
                    success = true,
                    data = hotels
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while retrieving all hotels"
                });
            }
        }

        // Get hotel by ID
        [HttpGet]
        [Route("GetHotelById/{id}")]
        public async Task<ActionResult<Hotel>> GetHotelByIdAsync(int id)
        {
            try
            {
                var hotel = await _hotelOwnerServices.GetHotelByIdAsync(id);
                if (hotel == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = $"Hotel with ID {id} not found"
                    });
                }
                return Ok(new
                {
                    success = true,
                    data = hotel
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while retrieving hotel by ID"
                });
            }
        }

        // Get hotel by name
        [HttpGet]
        [Route("GetHotelByName/{name}")]
        public async Task<ActionResult<Hotel>> GetHotelByNameAsync(string name)
        {
            try
            {
                var hotel = await _hotelOwnerServices.GetHotelByNameAsync(name);
                if (hotel == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = $"Hotel with name {name} not found"
                    });
                }
                return Ok(new
                {
                    success = true,
                    data = hotel
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while retrieving hotel by name"
                });
            }
        }

        // Create hotel
        [HttpPost]
        [Route("CreateHotel")]
        public async Task<ActionResult<Hotel>> CreateHotelAsync([FromBody] Hotel hotel)
        {
            try
            {
                var createdHotel = await _hotelOwnerServices.CreateHotelAsync(hotel);
                if (createdHotel == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        error = "Failed to create hotel"
                    });
                }
                return Ok(new
                {
                    success = true,
                    data = createdHotel
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while creating hotel"
                });
            }
        }

        // Update hotel
        [HttpPut]
        [Route("UpdateHotel")]
        public async Task<ActionResult<bool>> UpdateHotelAsync([FromBody] Hotel hotel)
        {
            try
            {
                var success = await _hotelOwnerServices.UpdateHotelAsync(hotel);
                if (!success)
                {
                    return BadRequest(new
                    {
                        success = false,
                        error = "Failed to update hotel"
                    });
                }
                return Ok(new
                {
                    success = true,
                    message = "Hotel updated successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while updating hotel"
                });
            }
        }

        // Delete hotel
        [HttpDelete]
        [Route("DeleteHotel/{id}")]
        public async Task<ActionResult<bool>> DeleteHotelAsync(int id)
        {
            try
            {
                var success = await _hotelOwnerServices.DeleteHotelAsync(id);
                if (!success)
                {
                    return BadRequest(new
                    {
                        success = false,
                        error = "Failed to delete hotel"
                    });
                }
                return Ok(new
                {
                    success = true,
                    message = "Hotel deleted successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while deleting hotel"
                });
            }
        }

    }
}
