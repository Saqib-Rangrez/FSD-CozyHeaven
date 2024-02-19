namespace CozyHavenStayServer.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int? UserId { get; set; }
        public int? HotelId { get; set; }
        public int Rating { get; set; }
        public string Comments { get; set; }

        public virtual Hotel? Hotel { get; set; }
        public virtual User? User { get; set; }
    }
}
