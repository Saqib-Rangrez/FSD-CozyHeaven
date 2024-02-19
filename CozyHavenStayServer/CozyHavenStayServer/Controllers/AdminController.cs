using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using CozyHavenStayServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CozyHavenStayServer.Controllers
{
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
                        success = "False",
                        message = "No data found"
                    });
                }

                return Ok(new
                {
                    success = "True",
                    data = admins
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = "False",
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
                        success = "False",
                        message = "Invalid Admin Id"
                    });
                }
                var admin = await _adminServices.GetAdminByIdAsync(id);


                if (admin == null)
                {
                    _logger.LogError("Admin not found with given Id");
                    return NotFound(new
                    {
                        success = "False",
                        message = $"The 'admin' with Id: {id} not found"
                    });
                }

                return Ok(new
                {
                    success = "True",
                    data = admin
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = "False",
                    error = "An error occurred while fetching admin."
                });
            }
        }

        //GetAdminByName
        [HttpGet]
        [Route("GetAdminByName/{name}")]
        public async Task<ActionResult<Admin>> GetAdminByNameAsync(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = "False",
                        message = "Invalid Admin Name"
                    });
                }

                var admin = await _adminServices.GetAdminByNameAsync(name);

                if (admin == null)
                {
                    _logger.LogError("Admin not found with given name");
                    return NotFound(new
                    {
                        success = "False",
                        message = $"The 'admin' with Name: {name} not found"
                    });
                }

                return Ok(new
                {
                    success = "True",
                    data = admin
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = "False",
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
                        success = "False",
                        message = "Null Object"
                    });
                }

                var createdAdmin = await _adminServices.CreateAdminAsync(model);
                
                return Ok(new
                {
                    success = "True",
                    data = createdAdmin
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = "False",
                    error = "An error occurred while creating admin."
                });
            }
        }


        //UpdateAdmin
        [HttpPut]
        [Route("UpdateAdmin")]
        public async Task<ActionResult> UpdateAdminAsync([FromBody] Admin model)
        {
            try
            {
                if (model == null || model.AdminId <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = "False",
                        message = "Invalid Data"
                    });
                }

                var admin = await _adminServices.UpdateAdminAsync(model);

                return Ok(new
                {
                    success = "True",
                    message = "Admin updated successfully"
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = "False",
                    error = "An error occurred while updating admin."
                });
            }
        }


        //DeleteAdmin
        [HttpDelete]
        [Route("/DeleteAdmin/{id}")]
        public async Task<ActionResult<bool>> DeleteAdminAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Bad Request");
                    return BadRequest(new
                    {
                        success = "False",
                        message = "Invalid Admin Id"
                    });
                }

                var deleteStatus = await _adminServices.DeleteAdminAsync(id);

                return Ok(new
                {
                    success = "True",
                    message = "Admin deleted successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    success = "False",
                    error = "An error occurred while deleting admin."
                });
            }
        }

    }
}
