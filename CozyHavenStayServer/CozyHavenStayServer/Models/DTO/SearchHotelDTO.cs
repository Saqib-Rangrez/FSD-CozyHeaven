namespace CozyHavenStayServer.Models.DTO
{
    public class SearchHotelDTO
    {
        public string? Location { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public int? NumberOfRooms { get; set; }
        public int? NumberOfAdults { get; set; }
        public int? NumberOfChildren { get; set; }
    }
}
