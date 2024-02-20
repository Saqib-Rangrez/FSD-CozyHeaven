using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using CozyHavenStayServer.Services;
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

        public UserController(ILogger<UserController> logger, IUserServices userServices)
        {
            _logger = logger;
            _userServices = userServices;
        }


        //GetAllUsers
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
        [Route("GetUserByEmail/{email}")]
        public async Task<ActionResult<User>> GetUserByNameAsync(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = "False",
                        message = "Invalid User Email"
                    });
                }

                var user = await _userServices.GetUserByEmailAsync(email);

                if (user == null)
                {
                    _logger.LogError("User not found with given name");
                    return NotFound(new
                    {
                        success = "False",
                        message = $"The 'user' with Email: {email} not found"
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
        [Route("DeleteUser/{id}")]
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

                if(deleteStatus)
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
                    error = "An error occurred while deleting user."
                });
            }
        }


        //UploadDisplay Picture
        [HttpPost]
        [Route("UploadDisplayPicture/{id}")]
        public async Task<ActionResult> UploadDisplayPicture(int id,[FromForm] IFormFile file)
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
                var user = await _userServices.GetUserByIdAsync(id);  
                if (user == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "User not found"
                    });
                }

                if (file == null || file.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }

                var result = await _userServices.UploadDisplayPicture(id, file); 
                user.ProfileImage = result.SecureUrl.ToString();

                if (result != null)
                {
                    return Ok(new
                    {
                        success = "True",
                        message = "Image Uploaded Successfully",
                        data = user,
                        imgRes = result
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
                    error = "An error occurred while deleting user."
                });
            }
        }

        //GetAllReviews
        [HttpGet]
        [Route("GetAllReviews")]
        public async Task<ActionResult<List<Review>>> GetAllReviewsAsync()
        {
            try
            {
                var reviews = await _userServices.GetAllReviewsAsync();

                if (reviews == null || reviews.Count <= 0)
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
                    data = reviews
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = "False",
                    error = "An error occurred while fetching reviews."
                });
            }
        }

        //GetReviewByReviewId
        [HttpGet]
        [Route("GetReviewByReviewId/{id}", Name = "GetReviewByReviewId")]
        public async Task<ActionResult<Review>> GetReviewByReviewIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = "False",
                        message = "Invalid Review Id"
                    });
                }
                var review = await _userServices.GetReviwByReviewIdAsync(id);

                if (review == null)
                {
                    _logger.LogError("User not found with given Id");
                    return NotFound(new
                    {
                        success = "False",
                        message = $"The 'Review' with Id: {id} not found"
                    });
                }

                return Ok(new
                {
                    success = "True",
                    data = review
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = "False",
                    error = "An error occurred while fetching review."
                });
            }
        }


        //GetReviewByUserId
        [HttpGet]
        [Route("GetReviewByUserId/{id}", Name = "GetReviewByUserId")]
        public async Task<ActionResult<Review>> GetReviewByUserIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = "False",
                        message = "Invalid user Id"
                    });
                }
                var review = await _userServices.GetReviewByUserIdAsync(id);

                if (review == null)
                {
                    _logger.LogError("User not found with given Id");
                    return NotFound(new
                    {
                        success = "False",
                        message = $"The 'Review' with Id: {id} not found"
                    });
                }

                return Ok(new
                {
                    success = "True",
                    data = review
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = "False",
                    error = "An error occurred while fetching review."
                });
            }
        }

        //GetReviewByUserId
        [HttpGet]
        [Route("GetReviewByHotelId/{id}", Name = "GetReviewByHotelId")]
        public async Task<ActionResult<Review>> GetReviewByHotelIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = "False",
                        message = "Invalid Hotel Id"
                    });
                }
                var review = await _userServices.GetReviewByHotelIdAsync(id);

                if (review == null)
                {
                    _logger.LogError("User not found with given Id");
                    return NotFound(new
                    {
                        success = "False",
                        message = $"The 'Review' with Id: {id} not found"
                    });
                }

                return Ok(new
                {
                    success = "True",
                    data = review
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = "False",
                    error = "An error occurred while fetching review."
                });
            }
        }


        //CreateReview
        [HttpPost]
        [Route("AddReview")]
        public async Task<ActionResult<Review>> AddReviewAsync([FromBody] Review model)
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

                var createdReview = await _userServices.AddReviewAsync(model);

                //return CreatedAtRoute("GetStudentById", new { id = createdUser.UserId }, User);

                return Ok(new
                {
                    success = "True",
                    data = createdReview
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = "False",
                    error = "An error occurred while creating review."
                });
            }
        }


        //Updatereview
        [HttpPut]
        [Route("UpdateReview")]
        public async Task<ActionResult> UpdateReviewAsync([FromBody] Review model)
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

                var user = await _userServices.UpdateReviewAsync(model);

                return Ok(new
                {
                    success = "True",
                    message = "Review updated successfully"
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = "False",
                    error = "An error occurred while updating Review."
                });
            }
        }


        //DeleteReview
        [HttpDelete]
        [Route("DeleteReview/{id}")]
        public async Task<ActionResult<bool>> DeleteReviewAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = "False",
                        message = "Invalid Review Id"
                    });
                }

                var deleteStatus = await _userServices.DeleteReviewAsync(id);

                if (deleteStatus)
                {
                    return Ok(new
                    {
                        success = "True",
                        message = "Review deleted successfully"
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        success = "False",
                        message = "Review Not found"
                    });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = "False",
                    error = "An error occurred while deleting Review."
                });
            }
        }

    }
}
