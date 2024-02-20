using CozyHavenStayServer.Models.DTO;
using CozyHavenStayServer.Models;

namespace CozyHavenStayServer.Mappers
{
    public class RegisterToOwner
    {
        HotelOwner owner;

        public RegisterToOwner(RegisterUserDTO registerUser)
        {
            owner = new HotelOwner();
            owner.FirstName = registerUser.FirstName;
            owner.LastName = registerUser.LastName;
            owner.Email = registerUser.Email;
            owner.ContactNumber = registerUser.ContactNumber;
            owner.Address = registerUser.Address;
            owner.Role = "Owner";
            owner.Password = registerUser.Password;
            owner.Gender = registerUser.Gender;
            owner.ProfileImage = $"https://api.dicebear.com/5.x/initials/svg?seed={registerUser.FirstName}{registerUser.LastName}";
        }

        public HotelOwner GetUser()
        {
            return owner;
        }
    }
}
