namespace CozyHavenStayServer.Models
{
    public class RoomImage
    {
        public int ImageId { get; set; }
        public int? RoomId { get; set; }
        public string? ImageUrl { get; set; }

        public virtual Room? Room { get; set; }
    }
}
