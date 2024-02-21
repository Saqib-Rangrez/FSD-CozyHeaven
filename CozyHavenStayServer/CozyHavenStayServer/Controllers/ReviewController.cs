using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CozyHavenStayServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly ILogger<UserController> _logger;

        public ReviewController(ILogger<UserController> logger, IUserServices userServices)
        {
            _logger = logger;
            _userServices = userServices;
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
                        success = false,
                        message = "No data found"
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = reviews
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
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
                        success = false,
                        message = "Invalid Review Id"
                    });
                }
                var review = await _userServices.GetReviwByReviewIdAsync(id);

                if (review == null)
                {
                    _logger.LogError("User not found with given Id");
                    return NotFound(new
                    {
                        success = false,
                        message = $"The 'Review' with Id: {id} not found"
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = review
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
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
                        success = false,
                        message = "Invalid user Id"
                    });
                }
                var review = await _userServices.GetReviewByUserIdAsync(id);

                if (review == null)
                {
                    _logger.LogError("User not found with given Id");
                    return NotFound(new
                    {
                        success = false,
                        message = $"The 'Review' with Id: {id} not found"
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = review
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
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
                        success = false,
                        message = "Invalid Hotel Id"
                    });
                }
                var review = await _userServices.GetReviewByHotelIdAsync(id);

                if (review == null)
                {
                    _logger.LogError("User not found with given Id");
                    return NotFound(new
                    {
                        success = false,
                        message = $"The 'Review' with Id: {id} not found"
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = review
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
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
                        success = false,
                        message = "Null Object"
                    });
                }

                var createdReview = await _userServices.AddReviewAsync(model);

                //return CreatedAtRoute("GetStudentById", new { id = createdUser.UserId }, User);
                if(createdReview != null)
                {
                    return Ok(new
                    {
                        success = true,
                        data = createdReview
                    });
                }
                else
                {
                    return BadRequest( new
                    {
                        success = false,
                        message = "Failed to create review"
                    });
                }                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
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
                        success = false,
                        message = "Invalid Data"
                    });
                }

                var updateStatus = await _userServices.UpdateReviewAsync(model);

                if (updateStatus)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Review updated successfully"
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Review not found"
                    });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
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
                        success = false,
                        message = "Invalid Review Id"
                    });
                }

                var deleteStatus = await _userServices.DeleteReviewAsync(id);

                if (deleteStatus)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Review deleted successfully"
                    });
                }
                else
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Review Not found"
                    });
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while deleting Review."
                });
            }
        }

    }
}
