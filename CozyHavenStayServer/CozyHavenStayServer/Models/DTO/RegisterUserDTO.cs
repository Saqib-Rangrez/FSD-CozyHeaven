using System.ComponentModel.DataAnnotations;

namespace CozyHavenStayServer.Models.DTO
{
    public class RegisterUserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }        
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
/*        public string? Role { get; set; }
        public string? ProfileImage { get; set; }*/
    }
}
