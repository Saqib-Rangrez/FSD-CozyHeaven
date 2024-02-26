using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CozyHavenStayServer.Models
{
    public class HotelOwner
    {
        public HotelOwner()
        {
            Hotels = new List<Hotel>();
        }

        public int OwnerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string? ProfileImage { get; set; }
        public string? Role { get; set; }
        [NotMapped]
        public string? Token { get; set; }

        public virtual List<Hotel>? Hotels { get; set; }
    }
}
