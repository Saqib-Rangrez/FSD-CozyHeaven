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
        [Authorize(Roles = "Admin, Owner")]
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
                        success = false,
                        message = "No data found"
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = hotelOwners
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching hotelOwners."
                });
            }
        }


        //GetHotelOwnerById
        [Authorize(Roles = "Admin, Owner")]
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
                        success = false,
                        message = "Invalid HotelOwner Id"
                    });
                }
                var hotelOwner = await _hotelOwnerServices.GetHotelOwnerByIdAsync(id);


                if (hotelOwner == null)
                {
                    _logger.LogError("HotelOwner not found with given Id");
                    return NotFound(new
                    {
                        success = false,
                        message = $"The 'hotelOwner' with Id: {id} not found"
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = hotelOwner
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching hotelOwner."
                });
            }
        }


        //GetHotelOwnerByName
        [Authorize(Roles = "Admin, Owner")]
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
                        success = false,
                        message = "Invalid HotelOwner Email"
                    });
                }

                var hotelOwner = await _hotelOwnerServices.GetHotelOwnerByEmailAsync(email);

                if (hotelOwner == null)
                {
                    _logger.LogError("HotelOwner not found with given email");
                    return NotFound(new
                    {
                        success = false,
                        message = $"The 'hotelOwner' with Email: {email} not found"
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = hotelOwner
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching hotelOwner."
                });
            }
        }

        //CreateHotelOwner
        [Authorize(Roles = "Admin, Owner")]
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
                        success = false,
                        message = "Null Object"
                    });
                }

                var createdHotelOwner = await _hotelOwnerServices.CreateHotelOwnerAsync(model);
                if(createdHotelOwner == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Failed to create hotel owner"
                    });
                }
                return Ok(new
                {
                    success = true,
                    data = createdHotelOwner
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while creating hotelOwner."
                });
            }
        }


        //UpdateHotelOwner
        [Authorize(Roles = "Admin, Owner")]
        [HttpPut]
        [Route("UpdateHotelOwner")]
        public async Task<ActionResult<HotelOwner>> UpdateHotelOwnerAsync([FromBody] HotelOwner model)
        {
            try
            {
                if (model == null || model.OwnerId <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Data"
                    });
                }

                var updateStatus = await _hotelOwnerServices.UpdateHotelOwnerAsync(model);

                if (updateStatus)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "HotelOwner updated successfully",
                        user = model
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "HotelOwner not found"
                    });
                }               

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while updating hotelOwner."
                });
            }
        }


        //DeleteHotelOwner
        [Authorize(Roles = "Admin, Owner")]
        [HttpDelete]
        [Route("DeleteHotelOwner/{id}")]
        public async Task<ActionResult<bool>> DeleteHotelOwnerAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid HotelOwner Id"
                    });
                }

                var deleteStatus = await _hotelOwnerServices.DeleteHotelOwnerAsync(id);

                if (deleteStatus)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "User deleted successfully"
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "User Not found"
                    });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while deleting hotelOwner."
                });
            }
        }

    }
}
