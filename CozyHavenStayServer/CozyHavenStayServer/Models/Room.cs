namespace CozyHavenStayServer.Models
{
    public class Room
    {
        public Room()
        {
            Bookings = new List<Booking>();
            RoomImages = new List<RoomImage>();
        }

        public int RoomId { get; set; }
        public int? HotelId { get; set; }
        public string RoomType { get; set; }
        public int MaxOccupancy { get; set; }
        public string BedType { get; set; }
        public decimal BaseFare { get; set; }
        public string RoomSize { get; set; }
        public string Acstatus { get; set; }

        public virtual Hotel? Hotel { get; set; }
        public virtual List<Booking>? Bookings { get; set; }
        public virtual List<RoomImage>? RoomImages { get; set; }
    }
}
