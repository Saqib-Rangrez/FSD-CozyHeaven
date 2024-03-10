namespace CozyHavenStayServer.Models.DTO
{
    public class HotelDTO
    {
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string Amenities { get; set; }
        public List<IFormFile>? Files { get; set; }
    }
}
