using CozyHavenStayServer.Models.DTO;

namespace CozyHavenStayServer.Interfaces
{
    public interface IAccountServices
    {
        Task<bool> RegisterAsync(RegisterUserDTO user);
        Task<string> LoginAsync(LoginUserDTO user);
    }
}
