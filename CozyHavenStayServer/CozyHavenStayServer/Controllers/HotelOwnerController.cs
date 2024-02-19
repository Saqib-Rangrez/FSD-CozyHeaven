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
        [Route("GetHotelOwnerByName/{name}")]
        public async Task<ActionResult<HotelOwner>> GetHotelOwnerByNameAsync(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = "False",
                        message = "Invalid HotelOwner Name"
                    });
                }

                var hotelOwner = await _hotelOwnerServices.GetHotelOwnerByNameAsync(name);

                if (hotelOwner == null)
                {
                    _logger.LogError("HotelOwner not found with given name");
                    return NotFound(new
                    {
                        success = "False",
                        message = $"The 'hotelOwner' with Name: {name} not found"
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

                return Ok(new
                {
                    success = "True",
                    message = "HotelOwner deleted successfully"
                });
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

        [HttpGet]
        [Route("SearchHotels")]
        public async Task<ActionResult<List<Hotel>>> SearchHotelsAsync(string location, string amenities)
        {
            try
            {
                var hotels = await _hotelOwnerServices.SearchHotelsAsync(location, amenities);

                return Ok(new
                {
                    success = "True",
                    data = hotels
                });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest(new
                {
                    success = "False",
                    error = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = "False",
                    error = "An error occurred while searching hotels."
                });
            }
        }

        // Search hotel rooms
        [HttpGet]
        [Route("SearchHotelRooms")]
        public async Task<ActionResult<List<Room>>> SearchHotelRoomsAsync(string location, DateTime checkInDate, DateTime checkOutDate, int numberOfRooms)
        {
            try
            {
                var hotelRooms = await _hotelOwnerServices.SearchHotelRoomsAsync(location, checkInDate, checkOutDate, numberOfRooms);

                return Ok(new
                {
                    success = "True",
                    data = hotelRooms
                });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest(new
                {
                    success = "False",
                    error = ex.Message
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = "False",
                    error = "An error occurred while searching hotel rooms."
                });
            }
        }
    }
}
