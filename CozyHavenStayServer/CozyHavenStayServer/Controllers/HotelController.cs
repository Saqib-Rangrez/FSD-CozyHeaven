﻿using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using CozyHavenStayServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CozyHavenStayServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly ILogger<HotelController> _logger;
        private readonly IHotelServices _hotelServices;

        public HotelController(ILogger<HotelController> logger, IHotelServices hotelServices)
        {
            _logger = logger;
            _hotelServices = hotelServices;
        }

        // Get all hotels
        [HttpGet]
        [Route("GetAllHotels")]
        public async Task<ActionResult<List<Hotel>>> GetAllHotelsAsync()
        {
            try
            {
                var hotels = await _hotelServices.GetAllHotelsAsync();
                if (hotels == null || hotels.Count <=0)
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
                var hotel = await _hotelServices.GetHotelByIdAsync(id);
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
                var hotel = await _hotelServices.GetHotelByNameAsync(name);
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
                var createdHotel = await _hotelServices.CreateHotelAsync(hotel);
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
                    message = "Hotel Created Successfully",
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
                var success = await _hotelServices.UpdateHotelAsync(hotel);
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
                var success = await _hotelServices.DeleteHotelAsync(id);
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
