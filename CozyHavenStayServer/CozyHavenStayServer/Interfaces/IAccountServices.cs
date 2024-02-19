using CozyHavenStayServer.Models;
using CozyHavenStayServer.Models.DTO;

namespace CozyHavenStayServer.Interfaces
{
    public interface IAccountServices
    {
        Task<bool> RegisterAsync(RegisterUserDTO user);
        string LoginAsync(User user);
    }
}
