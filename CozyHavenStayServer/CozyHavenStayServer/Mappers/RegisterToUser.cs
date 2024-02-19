using CozyHavenStayServer.Models;
using CozyHavenStayServer.Models.DTO;

namespace CozyHavenStayServer.Mappers
{
    public class RegisterToUser
    {
        User user;

        public RegisterToUser(RegisterUserDTO registerUser)
        {
            user = new User();
            user.FirstName = registerUser.FirstName;
            user.LastName = registerUser.LastName;
            user.Email = registerUser.Email;
            user.ContactNumber = registerUser.ContactNumber;
            user.Address = registerUser.Address;
            user.Role = registerUser.Role;
            user.Password = registerUser.Password;
            user.Gender = registerUser.Gender;            
        }

        public User GetUser()
        {
            return user;
        }
    }
}
