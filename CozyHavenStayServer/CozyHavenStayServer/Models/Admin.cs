using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CozyHavenStayServer.Models
{
    public class Admin
    {
        public int AdminId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string? ProfileImage { get; set; }
        public string? Role { get; set; }
        public string? Token { get; set; }

        public DateTime? ResetPasswordExpires { get; set; }
    }
}
