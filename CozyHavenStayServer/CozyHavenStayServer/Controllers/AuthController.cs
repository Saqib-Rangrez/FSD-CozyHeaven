using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using CozyHavenStayServer.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CozyHavenStayServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IAuthServices _authServices;
        private readonly IUserServices _userServices;
        private readonly IAccountServices _accountServices;

        public AuthController(ILogger logger, IAuthServices authServices, IUserServices userServices, IAccountServices accountServices)
        {
            _logger = logger;
            _authServices = authServices;
            _userServices = userServices;
            _accountServices = accountServices;
        }



        //User Registration
        [HttpPost]
        [Route("User/Register")]
        public async Task<ActionResult<User>> RegisterUser([FromBody] RegisterUserDTO model)
        {

        }

        ////Hotel Owner Registration
        //[HttpPost]
        //[Route("HotelOwner/Register")]
        //public async Task<ActionResult<User>> RegisterHotelOwner([FromBody] User model)
        //{

        //}

        ////Hotel Owner Login
        //[HttpPost]
        //[Route("HotelOwner/Login")]
        //public async Task<ActionResult<User>> RegisterHotelOwner([FromBody] User model)
        //{

        //}

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

            var token = _authServices.GenerateToken(user);
            user.Token = token;


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
    }
}
