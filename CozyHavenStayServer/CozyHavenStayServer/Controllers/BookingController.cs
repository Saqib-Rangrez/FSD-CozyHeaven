using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using CozyHavenStayServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CozyHavenStayServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly ILogger<BookingController> _logger;
        private readonly IBookingServices _bookingServices;

        public BookingController(ILogger<BookingController> logger, IBookingServices bookingServices)
        {
            _logger = logger;
            _bookingServices = bookingServices;
        }


        [Authorize(Roles = "Admin, User, Owner")]
        [HttpGet]
        [Route("GetAllBookings")]
        public async Task<ActionResult<List<Booking>>> GetAllBookingsAsync()
        {
            try
            {
                var bookings = await _bookingServices.GetAllBookingsAsync();
                if (bookings == null || bookings.Count <= 0)
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

        // Get booking by ID
        [Authorize(Roles = "Admin, User, Owner")]
        [HttpGet]
        [Route("GetBookingById/{id}")]
        public async Task<ActionResult<Booking>> GetBookingByIdAsync(int id)
        {
            try
            {
                var booking = await _bookingServices.GetBookingByIdAsync(id);
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

        //Get all bookings of user
        [Authorize(Roles = "Admin, User, Owner")]
        [HttpGet]
        [Route("GetBookingByUserId/{id}")]
        public async Task<ActionResult<List<Booking>>> GetBookingByUserId(int id)
        {
            try
            {
                var booking = await _bookingServices.GetBookingByUserIdAsync(id);
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
        [Authorize(Roles = "User")]
        [HttpPost]
        [Route("CreateBooking")]
        public async Task<ActionResult<Booking>> CreateBookingAsync([FromBody] Booking booking)
        {
            try
            {
                var createdBooking = await _bookingServices.CreateBookingAsync(booking);
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
        [Authorize(Roles = "Admin, User, Owner")]
        [HttpPut]
        [Route("UpdateBooking")]
        public async Task<ActionResult<Booking>> UpdateBookingAsync([FromBody] Booking booking)
        {
            try
            {
                var success = await _bookingServices.UpdateBookingAsync(booking);
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
                    message = "Booking updated successfully",
                    data  = booking
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
        [Authorize(Roles = "Admin, Owner")]
        [HttpDelete]
        [Route("DeleteBooking/{id}")]
        public async Task<ActionResult<bool>> DeleteBookingAsync(int id)
        {
            try
            {
                var success = await _bookingServices.DeleteBookingAsync(id);
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


    }
}
