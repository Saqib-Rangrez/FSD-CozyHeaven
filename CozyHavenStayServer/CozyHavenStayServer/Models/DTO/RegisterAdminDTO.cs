using System.ComponentModel.DataAnnotations;

namespace CozyHavenStayServer.Models.DTO
{
    public class RegisterAdminDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        //public string? ProfileImage { get; set; }
    }
}
