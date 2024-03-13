using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Mappers;
using CozyHavenStayServer.Models;
using CozyHavenStayServer.Models.DTO;
using CozyHavenStayServer.Repositories;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;

namespace CozyHavenStayServer.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly ILogger<AccountServices> _logger;
        private readonly IAuthServices _authServices;
        private readonly IUserServices _userServices;
        private readonly IHotelOwnerServices _hotelOwnerServices;
        private readonly IAdminServices _adminServices;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Admin> _adminRepository;
        private readonly IRepository<HotelOwner> _hotelOwnerRepository;
        

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

        public async Task<dynamic> ForgotPassAsync(dynamic model)
        {
            model.Token = await  generateResetToken(model.Role);
            model.ResetPasswordExpires = DateTime.UtcNow.AddHours(6);

            if (model.Role == "User")
            {
                await _userServices.UpdateUserAsync(model, false);
            }
            else if (model.Role == "Admin")
            {
                await _adminServices.UpdateAdminAsync(model,false);
            }
            else
            {
                await _hotelOwnerServices.UpdateHotelOwnerAsync(model,false);
            }
            return model;
        }

        public async Task<dynamic> ResetPassAsync(dynamic model)
        {
            dynamic user = null;
            var users = await _userServices.GetAllUsersAsync();
            user = users.Where(u => u.Token == model.Token).FirstOrDefault();
            if(user == null)
            {
                var admins = await _adminServices.GetAllAdminsAsync();
                user = admins.Where(u => u.Token == model.Token).FirstOrDefault();
                if(user == null)
                {
                    var owners = await _hotelOwnerServices.GetAllHotelOwnersAsync();
                    user = owners.Where(u => u.Token == model.Token).FirstOrDefault();
                }
            }

            if(user == null)
            {
                return null; 
            }

            if (user.ResetPasswordExpires < DateTime.UtcNow)
            {
                return null; 
            }

            string hashedPassword = _authServices.HashPassword(model.Password);
            user.Password = hashedPassword;
            

            if (user.Role == "User")
            {
                user = await _userServices.UpdateUserAsync(user, false);
            }
            else if (user.Role == "Admin")
            {
                user = await _adminServices.UpdateAdminAsync(user, false);
            }
            else
            {
                user = await _hotelOwnerServices.UpdateHotelOwnerAsync(user, false);
            }

            return user;

        }


        private async Task<string> generateResetToken(string role)
        {
            var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
            bool tokenIsUnique = false;
            if (role == "User")
            {
                var users = await _userServices.GetAllUsersAsync();
                tokenIsUnique = !users.Any(u => u.Token == token);
            }else if(role == "Admin")
            {
                var users = await _adminServices.GetAllAdminsAsync();
                tokenIsUnique = !users.Any(u => u.Token == token);
            }
            else
            {
                var users = await _hotelOwnerServices.GetAllHotelOwnersAsync();
                tokenIsUnique = !users.Any(u => u.Token == token);
            }
            if (!tokenIsUnique)
                return await generateResetToken(role);

            return token;
        }

    }
}
