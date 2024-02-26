using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Mappers;
using CozyHavenStayServer.Models;
using CozyHavenStayServer.Models.DTO;
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
        private readonly ICloudinaryService _cloudinaryService;

        public HotelController(ILogger<HotelController> logger, IHotelServices hotelServices, ICloudinaryService cloudinaryService)
        {
            _logger = logger;
            _hotelServices = hotelServices;
            _cloudinaryService = cloudinaryService;
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
                    return NotFound( new
                    {
                        success = false,
                        error = "No data found"
                    });
                }
                return Ok(new
                {
                    success = true,
                    data = hotels,
                    size = hotels.Count
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

        // Get all hotels
        [HttpPost]
        [Route("SearchHotels")]
        public async Task<ActionResult<List<Hotel>>> SearchHotelsAsync([FromBody] SearchHotelDTO searchHotelDTO)
        {
            try
            {
                var hotels = await _hotelServices.SearchHotelsAsync(searchHotelDTO);
                if (hotels == null || hotels.Count <= 0)
                {
                    return NotFound(new
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
                    data = hotel,
                    size = hotel.Rooms.Count
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
        /*[HttpPost]
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
*/
        [HttpPost]
        [Route("CreateHotel")]
        public async Task<ActionResult<Hotel>> CreateHotelAsync([FromForm] HotelDTO model)
        {
            try
            {
                if(model == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Error in details"
                    });
                }
                var hotelValid = await _hotelServices.GetHotelByNameAsync(model.Name);
                if ( hotelValid != null )
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Hotel with given name already exist",
                        data = hotelValid
                    });
                }

                if (model.Files == null || model.Files.Count <= 0)
                {
                    return BadRequest("No file provided.");
                }

                RegisterToHotel registerToHotel = new RegisterToHotel(model);
                var hotelToAdd = registerToHotel.GetHotel();
                var createdHotel = await _hotelServices.CreateHotelAsync(hotelToAdd);

                if (createdHotel != null)
                {
                    foreach (var file in model.Files)
                    {
                        var uploadResult = await _cloudinaryService.UploadImageAsync(file);
                        
                        if (uploadResult != null)
                        {
                            HotelImageDTO hotelImageDTO = new HotelImageDTO()
                            {
                                HotelId = createdHotel.HotelId,
                                ImageUrl = uploadResult.SecureUrl.ToString(),
                            };
                            RegisterToHotelImage registerToHotelImage = new RegisterToHotelImage(hotelImageDTO);
                            var imageToAdd = registerToHotelImage.GetHotelImage();
                            var addedImage = await _hotelServices.AddHotelImageAsync(imageToAdd);

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
                    message = "Hotel Added Successfully.",
                    data = createdHotel
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, new
                {
                    success = false,
                    message = "Error, while adding hotel"
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
