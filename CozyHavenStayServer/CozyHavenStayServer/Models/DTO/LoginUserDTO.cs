using System.ComponentModel.DataAnnotations;

namespace CozyHavenStayServer.Models.DTO
{
    public class LoginUserDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string? Token { get; set; }
        public string? Role { get; set; }

    }
}
