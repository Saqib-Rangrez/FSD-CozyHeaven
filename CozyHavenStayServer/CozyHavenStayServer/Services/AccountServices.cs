using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Mappers;
using CozyHavenStayServer.Models;
using CozyHavenStayServer.Models.DTO;
using CozyHavenStayServer.Repositories;

namespace CozyHavenStayServer.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly ILogger<AccountServices> _logger;
        private readonly IAuthServices _authServices;
        private readonly IUserServices _userServices;
        private readonly IHotelOwnerServices _hotelOwnerServices;
        private readonly IAdminServices _adminServices;

        public AccountServices(ILogger<AccountServices> logger, IAuthServices authServices, IUserServices userServices, IHotelOwnerServices hotelOwnerServices, IAdminServices adminServices)
        {
            _logger = logger;
            _authServices = authServices;
            _userServices = userServices;
            _hotelOwnerServices = hotelOwnerServices;
            _adminServices = adminServices;
        }
        public string LoginAsync(dynamic user)
        {
            var jwt = _authServices.GenerateToken(user);
            
            return jwt;
        }

        public async Task<Admin> RegisterAdminAsync(RegisterAdminDTO registrationData)
        {
            string hashedPassword = _authServices.HashPassword(registrationData.Password);
            registrationData.Password = hashedPassword;

            RegisterToAdmin registerToUser = new RegisterToAdmin(registrationData);
            Admin userToRegister = registerToUser.GetUser();

            var createdUser = await _adminServices.CreateAdminAsync(userToRegister);
            createdUser.Password = null;

            return createdUser;
        }

        public async Task<User> RegisterUserAsync(RegisterUserDTO registrationData)
        {
            string hashedPassword = _authServices.HashPassword(registrationData.Password);
            registrationData.Password = hashedPassword;

            RegisterToUser registerToUser = new RegisterToUser(registrationData);
            User userToRegister = registerToUser.GetUser();

            var createdUser = await _userServices.CreateUserAsync(userToRegister);
            createdUser.Password = null;

            return createdUser;
        }

        public async Task<HotelOwner> RegisterOwnerAsync(RegisterUserDTO registrationData)
        {
            string hashedPassword = _authServices.HashPassword(registrationData.Password);
            registrationData.Password = hashedPassword;

            RegisterToOwner registerToOwner = new RegisterToOwner(registrationData);
            HotelOwner ownerToRegister = registerToOwner.GetUser();

            var createdUser = await _hotelOwnerServices.CreateHotelOwnerAsync(ownerToRegister);
            createdUser.Password = null;

            return createdUser;
        }

    }
}
