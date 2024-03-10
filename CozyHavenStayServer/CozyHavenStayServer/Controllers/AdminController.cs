using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using CozyHavenStayServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CozyHavenStayServer.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IAdminServices _adminServices;

        public AdminController(ILogger<AdminController> logger, IAdminServices adminServices)
        {
            _logger = logger;
            _adminServices = adminServices;
        }

        //GetAll
        [HttpGet]
        [Route("GetAllAdmins")]
        public async Task<ActionResult<List<Admin>>> GetAllAdminsAsync()
        {
            try
            {
                var admins = await _adminServices.GetAllAdminsAsync();

                if (admins == null || admins.Count <= 0)
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
                    data = admins
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching Admins."
                });
            }
        }

        //GetAdminById
        [HttpGet]
        [Route("GetAdminById/{id}", Name = "GetAdminById")]
        public async Task<ActionResult<Admin>> GetAdminByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Admin Id"
                    });
                }
                var admin = await _adminServices.GetAdminByIdAsync(id);


                if (admin == null)
                {
                    _logger.LogError("Admin not found with given Id");
                    return NotFound(new
                    {
                        success = false,
                        message = $"The 'admin' with Id: {id} not found"
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = admin
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching admin."
                });
            }
        }

        //GetAdminByName
        [HttpGet]
        [Route("GetAdminByEmail/{email}")]
        public async Task<ActionResult<Admin>> GetAdminByEmailAsync(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Admin Email"
                    });
                }

                var admin = await _adminServices.GetAdminByEmailAsync(email);

                if (admin == null)
                {
                    _logger.LogError("Admin not found with given email");
                    return NotFound(new
                    {
                        success = false,
                        message = $"The 'admin' with Email: {email} not found"
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = admin
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while fetching admin."
                });
            }
        }

        //CreateAdmin
        [HttpPost]
        [Route("CreateAdmin")]
        public async Task<ActionResult<Admin>> CreateAdminAsync([FromBody] Admin model)
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

                var createdAdmin = await _adminServices.CreateAdminAsync(model);
                
                if(createdAdmin == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Failed to create admin"
                    });
                }
                return Ok(new
                {
                    success = true,
                    data = createdAdmin
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while creating admin."
                });
            }
        }


        //UpdateAdmin
        [HttpPut]
        [Route("UpdateAdmin")]
        public async Task<ActionResult<Admin>> UpdateAdminAsync([FromBody] Admin model)
        {
            try
            {
                if (model == null || model.AdminId <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Data"
                    });
                }

                var admin = await _adminServices.UpdateAdminAsync(model);

                if(admin) {
                   return Ok(new
                    {
                        success = true,
                        message = "User updated successfully",
                        user = model
                    });
                }

                return BadRequest(new
                {
                    success = false,
                    message = "Invalid Data"
                });
                

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = false,
                    error = "An error occurred while updating admin."
                });
            }
        }


        //DeleteAdmin
        [HttpDelete]
        [Route("DeleteAdmin/{id}")]
        public async Task<ActionResult<bool>> DeleteAdminAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = false,
                        message = "Invalid Admin Id"
                    });
                }

                var deleteStatus = await _adminServices.DeleteAdminAsync(id);

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
                    error = "An error occurred while deleting admin."
                });
            }
        }

    }
}
