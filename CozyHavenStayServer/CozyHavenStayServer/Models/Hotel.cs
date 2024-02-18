namespace CozyHavenStayServer.Models
{
    public class Hotel
    {
        public Hotel()
        {
            HotelImages = new List<HotelImage>();
            Reviews = new List<Review>();
            Rooms = new List<Room>();

        }

        public int HotelId { get; set; }
        public int? OwnerId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Amenities { get; set; }

        public virtual HotelOwner? Owner { get; set; }
        public virtual List<Booking> Bookings { get; set; }
        public virtual List<HotelImage> HotelImages { get; set; }
        public virtual List<Review> Reviews { get; set; }
        public virtual List<Room> Rooms { get; set; }
    }
}
