namespace CozyHavenStayServer.Models
{
    public class Booking
    {      
        public int BookingId { get; set; }
        public int? UserId { get; set; }
        public int? RoomId { get; set; }
        public int? HotelId { get; set; }
        public int NumberOfGuests { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalFare { get; set; }
        public string Status { get; set; }
        public virtual Room? Room { get; set; }
        public virtual Hotel? Hotel { get; set; }
        public virtual User? User { get; set; }
    }
}
