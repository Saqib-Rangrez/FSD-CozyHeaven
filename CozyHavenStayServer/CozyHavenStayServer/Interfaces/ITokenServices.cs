using CozyHavenStayServer.Models.DTO;

namespace CozyHavenStayServer.Interfaces
{
    public interface ITokenServices
    {
        public Task<string> GenerateToken(LoginUserDTO user);
    }
}
