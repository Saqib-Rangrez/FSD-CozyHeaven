using CozyHavenStayServer.Models;
using CozyHavenStayServer.Models.DTO;

namespace CozyHavenStayServer.Interfaces
{
    public interface IAuthServices
    {
        public string GenerateToken(dynamic user);
        public string HashPassword(string password);
        public bool VerifyPassword(string password, string hashedPassword);

    }
}
