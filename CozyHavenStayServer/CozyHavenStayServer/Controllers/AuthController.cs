using CloudinaryDotNet;
using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Mappers;
using CozyHavenStayServer.Models;
using CozyHavenStayServer.Models.DTO;
using CozyHavenStayServer.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace CozyHavenStayServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthServices _authServices;
        private readonly IUserServices _userServices;
        private readonly IAdminServices _adminServices;
        private readonly IHotelOwnerServices _hotelOwnerServices;
        private readonly IAccountServices _accountServices;
        private readonly IEmailService _emailService;


        public AuthController(ILogger<AuthController> logger, IAuthServices authServices, IUserServices userServices, IAccountServices accountServices, IAdminServices adminServices, IHotelOwnerServices hotelOwnerServices, IEmailService emailService)
        {
            _logger = logger;
            _authServices = authServices;
            _userServices = userServices;
            _accountServices = accountServices;
            _adminServices = adminServices;
            _hotelOwnerServices = hotelOwnerServices;
            _emailService = emailService;
        }


        //User Registration
        [HttpPost]
        [Route("User/Register")]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterUserDTO registrationData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (registrationData.Password != registrationData.ConfirmPassword)
            {
                ModelState.AddModelError("Error", "Passwords do not match!");
                return BadRequest(ModelState);
            }

            string email = registrationData.Email;

            if(await _userServices.GetUserByEmailAsync(email) != null || await _adminServices.GetAdminByEmailAsync(email) != null
                || await _hotelOwnerServices.GetHotelOwnerByEmailAsync(email) != null
                )
            {
                ModelState.AddModelError("Error", "A user with the email already exists!");
                return BadRequest(ModelState);
            }

            var createdUser = await _accountServices.RegisterUserAsync(registrationData);

            if (createdUser != null)
            {
                return Ok(new
                {
                    success = true,
                    message = "User registered successfully.",
                    data = createdUser
                });
            }
            else
            {
                ModelState.AddModelError("Error", "An error occuerd while creating the admin!");
                return StatusCode(500, new
                {
                    success = false,
                    ModelState
                });
            }            
        }

        //Hotel Owner Registration
        [HttpPost]
        [Route("HotelOwner/Register")]
        public async Task<ActionResult> RegisterHotelOwner([FromBody] RegisterUserDTO registrationData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (registrationData.Password != registrationData.ConfirmPassword)
            {
                ModelState.AddModelError("Error", "Passwords do not match!");
                return BadRequest(ModelState);
            }

            string email = registrationData.Email;

            if (await _userServices.GetUserByEmailAsync(email) != null || await _adminServices.GetAdminByEmailAsync(email) != null
                || await _hotelOwnerServices.GetHotelOwnerByEmailAsync(email) != null
                )
            {
                ModelState.AddModelError("Error", "An owner with the email already exists!");
                return BadRequest(ModelState);
            }

            var createdUser = await _accountServices.RegisterOwnerAsync(registrationData);

            if (createdUser != null)
            {
                return Ok(new
                {
                    success = true,
                    message = "Owner registered successfully.",
                    data = createdUser
                });
            }
            else
            {
                ModelState.AddModelError("Error", "An error occuerd while creating the admin!");
                return StatusCode(500, new
                {
                    success = false,
                    ModelState
                });
            }
        }

        //Hotel Owner Registration
        [HttpPost]
        [Route("Admin/Register")]
        public async Task<ActionResult> RegisterAdmin([FromBody] RegisterAdminDTO registrationData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (registrationData.Password != registrationData.ConfirmPassword)
            {
                ModelState.AddModelError("Error", "Passwords do not match!");
                return BadRequest(ModelState);
            }

            string email = registrationData.Email;

            if (await _userServices.GetUserByEmailAsync(email) != null || await _adminServices.GetAdminByEmailAsync(email) != null
                || await _hotelOwnerServices.GetHotelOwnerByEmailAsync(email) != null
                )
            {
                ModelState.AddModelError("Error", "An Admin with the email already exists!");
                return BadRequest(ModelState);
            }

            var createdUser = await _accountServices.RegisterAdminAsync(registrationData);

            if (createdUser != null)
            {
                return Ok(new
                {
                    success = true,
                    message = "Admin registered successfully.",
                    data = createdUser
                });
            }
            else
            {
                ModelState.AddModelError("Error", "An error occuerd while creating the admin!");
                return StatusCode(500, new
                {
                    success = false,
                    ModelState
                });
            }
        }

        //Hotel Owner Login
        [HttpPost]
        [Route("Admin/Login")]
        public async Task<ActionResult> AdminLogin([FromBody] LoginUserDTO loginCredentials)
        {
            if (loginCredentials == null) return BadRequest(ModelState); ;

            var user = await _adminServices.GetAdminByEmailAsync(loginCredentials.Email);

            if (user == null) return BadRequest("Invalid Username or Password!");

            var match = _authServices.VerifyPassword(loginCredentials.Password, user.Password);

            if (!match) return BadRequest("Invalid Username or Password!");

            var token = _accountServices.LoginAsync(user);
            user.Token = token;
            user.Password = null;


            try
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTimeOffset.UtcNow.AddDays(1)
                };

                Response.Cookies.Append("token", token, cookieOptions);

                return Ok(new
                {
                    success = true,
                    token,
                    user,
                    message = "Logged in successfully"
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        //Hotel Owner Login
        [HttpPost]
        [Route("HotelOwner/Login")]
        public async Task<ActionResult> HotelOwnerLogin([FromBody] LoginUserDTO loginCredentials)
        {
            if (loginCredentials == null) return BadRequest(ModelState); ;

            var user = await _hotelOwnerServices.GetHotelOwnerByEmailAsync(loginCredentials.Email);

            if (user == null) return BadRequest("Invalid Username or Password!");

            var match = _authServices.VerifyPassword(loginCredentials.Password, user.Password);

            if (!match) return BadRequest("Invalid Username or Password!");

            var token = _accountServices.LoginAsync(user);
            user.Token = token;
            user.Password = null;


            try
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTimeOffset.UtcNow.AddDays(1)
                };

                Response.Cookies.Append("token", token, cookieOptions);

                return Ok(new
                {
                    success = true,
                    token,
                    user,
                    message = "Logged in successfully"
                });

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        //User  Login
        [HttpPost]
        [Route("User/Login")]
        public async Task<ActionResult> UserLogin([FromBody] LoginUserDTO loginCredentials)
        {
            if (loginCredentials == null) return BadRequest(ModelState);;

            var user = await _userServices.GetUserByEmailAsync(loginCredentials.Email);

            if (user == null) return BadRequest("Invalid Username or Password!");

            var match = _authServices.VerifyPassword(loginCredentials.Password, user.Password);

            if (!match) return BadRequest("Invalid Username or Password!");

            var token = _accountServices.LoginAsync(user);
            user.Token = token;
            user.Password = null;


            try
            {
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.None,
                    Expires = DateTimeOffset.UtcNow.AddDays(1) 
                };

                Response.Cookies.Append("token", token, cookieOptions);

                return Ok(new
                {
                    success = true,
                    token,
                    user,
                    message = "Logged in successfully"
                });

            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal server error"); 
            }

        }

        [HttpPost]
        [Route("Logout")]
        public IActionResult Logout([FromServices] ITokenBlacklistService tokenBlacklistService)
        {
            string token = HttpContext.Request?.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new { message = "Token is missing in the request header" });
            }
            DateTime expiryTime = DateTime.UtcNow.AddMinutes(30);

            tokenBlacklistService.AddTokenToBlacklist(token, expiryTime);

            return Ok(new { message = "Logout successful" });
        }

        //forget-password mail
        [HttpPost]
        [Route("ForgetPassword")]
        public async Task<IActionResult> ForgetPasswordAsync([FromBody] ForgetPasswordDTO model)
        {
            dynamic user;
            if(model == null)
            {
                return BadRequest();
            }

            if (model.Role == "User")
            {
                user = await _userServices.GetUserByEmailAsync(model.Email);
            }
            else if (model.Role == "Admin")
            {
                user = await _adminServices.GetAdminByEmailAsync(model.Email);
            }
            else
            {
                user = await _hotelOwnerServices.GetHotelOwnerByEmailAsync(model.Email);
            }

            if (user == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = $"User with email : {model.Email} does not exist"
                });
            }

            var data = await _accountServices.ForgotPassAsync(user);

            var origin = Request.Headers["Origin"];
            string message;
            if (!string.IsNullOrEmpty(origin))
            {
                var resetUrl = $"{origin}/account/reset-password?token={data.Token}";
                message = $@"<p>Please click the below link to reset your password, the link will be valid for 6 hours:</p>
                            <p><a href=""{resetUrl}"">{resetUrl}</a></p>";
            }
            else
            {
                var resetUrl = $"http://localhost:4200/account/reset-password?token{data.Token}";
                message = $@"<p>Please click the below link to reset your password, the link will be valid for 6 hours:</p>
                            <p><a href=""{resetUrl}"">{resetUrl}</a></p>";
            }

            var toEmail = "ronrohan557@gmail.com"; //model.Email;
            var subject = "Reset Password";
            var body = message;

            var isEmailSent = _emailService.SendEmailAsync(toEmail, subject, body);
            if (isEmailSent)
            {
                return Ok(new
                {
                    success = true,
                    message = "Email sent successfully",
                    user = data
                });
            }
            else
            {
                return BadRequest("Failed to send email");
            }
        }


        //reset-password 
        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordDTO model)
        {
            
            if (model == null)
            {
                return BadRequest();
            }
            if (model.Password != model.ConfirmPassword)
            {
                return BadRequest(new { success = false, message = "Password mismatch" });
            }

            dynamic user = await _accountServices.ResetPassAsync(model);

            if(user == null)
            {
                return BadRequest(new { success = false, message = "Token is expired. Please regenerate your token" });
            }

            string message = $@"<h1>Password Reset Successfull</h1><p>Your password has been changed successfully.</p>";
 


            var toEmail = model.Email;
            var subject = "Reset Password";
            var body = message;

            var isEmailSent = _emailService.SendEmailAsync(toEmail, subject, body);
            if (isEmailSent)
            {
                return Ok(new
                {
                    success = true,
                    message = "Password changed successfully.",
                    data = user
                });
            }
            else
            {
                return BadRequest("Failed to reset password");
            }
        }
    }
}
