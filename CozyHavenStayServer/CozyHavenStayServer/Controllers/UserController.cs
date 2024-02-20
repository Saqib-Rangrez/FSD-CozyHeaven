using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CozyHavenStayServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly ILogger<UserController> _logger;
        private readonly IHotelServices _hotelServices;


        public UserController(ILogger<UserController> logger, IUserServices userServices, IHotelServices hotelServices)
        {
            _logger = logger;
            _userServices = userServices;
            _hotelServices = hotelServices;
        }


        //GetAll
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<ActionResult<List<User>>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userServices.GetAllUsersAsync();

                if(users == null || users.Count <= 0)
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
                    data = users 
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = "False",
                    error = "An error occurred while fetching users."
                });
            }
        }


        //GetUserById
        [HttpGet]
        [Route("GetUserById/{id}", Name = "GetUserById")]
        public async Task<ActionResult<User>> GetUserByIdAsync(int id)
        {
            try
            {
                if(id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = "False",
                        message = "Invalid User Id"
                    });
                }
                var user = await _userServices.GetUserByIdAsync(id);


                if (user == null)
                {
                    _logger.LogError("User not found with given Id");
                    return NotFound(new
                    {
                        success = "False",
                        message = $"The 'user' with Id: {id} not found"
                    });
                }

                return Ok(new
                {
                    success ="True",
                    data = user
                });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = "False",
                    error = "An error occurred while fetching user."
                });
            }
        }


        //GetUserByName
        [HttpGet]
        [Route("GetUserByName/{name}")]
        public async Task<ActionResult<User>> GetUserByNameAsync(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = "False",
                        message = "Invalid User Name"
                    });
                }

                var user = await _userServices.GetUserByNameAsync(name);

                if (user == null)
                {
                    _logger.LogError("User not found with given name");
                    return NotFound(new
                    {
                        success = "False",
                        message = $"The 'user' with Name: {name} not found"
                    });
                }

                return Ok(new
                {
                    success = "True",
                    data = user
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = "False",
                    error = "An error occurred while fetching user."
                });
            }
        }

        //CreateUser
        [HttpPost]
        [Route("CreateUser")]
        public async Task<ActionResult<User>> CreateUserAsync([FromBody] User model)
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

                var createdUser = await _userServices.CreateUserAsync(model);

                //return CreatedAtRoute("GetStudentById", new { id = createdUser.UserId }, User);

                return Ok(new
                {
                    success = "True",
                    data = createdUser
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = "False",
                    error = "An error occurred while creating user."
                });
            }
        }


        //UpdateUser
        [HttpPut]
        [Route("UpdateUser")]
        public async Task<ActionResult> UpdateUserAsync([FromBody] User model)
        {
            try
            {
                if (model == null || model.UserId <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = "False",
                        message = "Invalid Data"
                    });
                }

                var user = await _userServices.UpdateUserAsync(model);                

                return Ok(new
                {
                    success = "True",
                    message = "User updated successfully"
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = "False",
                    error = "An error occurred while updating user."
                });
            }
        }


        //DeleteUser
        [HttpDelete]
        [Route("/DeleteUser/{id}")]
        public async Task<ActionResult<bool>> DeleteUserAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = "False",
                        message = "Invalid User Id"
                    });
                }

                var deleteStatus = await _userServices.DeleteUserAsync(id);

                return Ok(new
                {
                    success = "True",
                    message = "User deleted successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = "False",
                    error = "An error occurred while deleting user."
                });
            }
        }

        [HttpGet]
        [Route("SearchHotels")]
        public async Task<ActionResult<List<Hotel>>> SearchHotelsAsync(string location, string amenities)
        {
            try
            {
                var hotels = await _hotelServices.SearchHotelsAsync(location, amenities);

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
                var hotelRooms = await _hotelServices.SearchHotelRoomsAsync(location, checkInDate, checkOutDate, numberOfRooms);

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
