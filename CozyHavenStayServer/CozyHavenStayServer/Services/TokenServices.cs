using CozyHavenStayServer.Interfaces;
using CozyHavenStayServer.Models.DTO;

namespace CozyHavenStayServer.Services
{
    public class TokenServices : ITokenServices
    {
        public Task<string> GenerateToken(LoginUserDTO user)
        {
            throw new NotImplementedException();
        }
    }
}
