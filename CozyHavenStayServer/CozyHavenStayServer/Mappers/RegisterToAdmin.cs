using CozyHavenStayServer.Models.DTO;
using CozyHavenStayServer.Models;

namespace CozyHavenStayServer.Mappers
{
    public class RegisterToAdmin
    {
        Admin admin;

        public RegisterToAdmin(RegisterAdminDTO registerUser)
        {
            admin = new Admin();
            admin.FirstName = registerUser.FirstName;
            admin.LastName = registerUser.LastName;
            admin.Email = registerUser.Email;
            admin.Role = "Admin";
            admin.Password = registerUser.Password;
            admin.ProfileImage = $"https://api.dicebear.com/5.x/initials/svg?seed={registerUser.FirstName}{registerUser.LastName}";
        }

        public Admin GetUser()
        {
            return admin;
        }
    }
}
