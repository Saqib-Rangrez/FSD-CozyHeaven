using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models;
using CozyHavenStayServer.Models.DTO;
using CozyHavenStayServer.Repositories;

namespace CozyHavenStayServer.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly ILogger _logger;
        private readonly IAuthServices _authServices;
        private readonly IUserServices _userServices;

        public AccountServices(ILogger logger, IAuthServices authServices, IUserServices userServices)
        {
            _logger = logger;
            _authServices = authServices;
            _userServices = userServices;
        }
        public string LoginAsync(User user)
        {
            var jwt = _authServices.GenerateToken(user);
            
            return jwt;
        }

        public Task<bool> RegisterAsync(RegisterUserDTO user)
        {
            throw new NotImplementedException();
        }
    }
}
