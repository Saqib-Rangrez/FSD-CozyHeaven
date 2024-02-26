namespace CozyHavenStayServer.Models.DTO
{
    public class RoomDTO
    {
        public int? HotelId { get; set; }
        public string RoomType { get; set; }
        public int MaxOccupancy { get; set; }
        public string BedType { get; set; }
        public decimal BaseFare { get; set; }
        public string RoomSize { get; set; }
        public string Acstatus { get; set; }
        public List<IFormFile>? Files { get; set; }

    }
}
