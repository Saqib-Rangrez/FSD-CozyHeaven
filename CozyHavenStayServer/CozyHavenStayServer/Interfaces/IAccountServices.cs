using CozyHavenStayServer.Models;
using CozyHavenStayServer.Models.DTO;

namespace CozyHavenStayServer.Interfaces
{
    public interface IAccountServices
    {
        Task<User> RegisterUserAsync(RegisterUserDTO model);
        Task<HotelOwner> RegisterOwnerAsync(RegisterUserDTO model);
        Task<Admin> RegisterAdminAsync(RegisterAdminDTO model);
        string LoginAsync(dynamic model);
        public Task<dynamic> ForgotPassAsync(dynamic model);
        public Task<dynamic> ResetPassAsync(dynamic model);

    }
}
