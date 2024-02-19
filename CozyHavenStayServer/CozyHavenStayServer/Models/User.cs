using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CozyHavenStayServer.Models
{
    public class User
    {
        public User()
        {
            Bookings = new List<Booking>();
            Reviews = new List<Review>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string? Role { get; set; }
        public string? ProfileImage { get; set; }
        [NotMapped]
        public string? Token {  get; set; }

        public virtual List<Booking> Bookings { get; set; }
        public virtual List<Review> Reviews { get; set; }
    }
}
