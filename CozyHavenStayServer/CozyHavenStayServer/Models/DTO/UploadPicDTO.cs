namespace CozyHavenStayServer.Models.DTO
{
    public class UploadPicDTO
    {
        public int id { get; set; }
        public string Role { get; set; }
        public IFormFile file {get; set;}
    }
}
