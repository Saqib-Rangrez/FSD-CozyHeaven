namespace CozyHavenStayServer.Models.DTO
{
    public class LoginUserDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Token { get; set; }
    }
}
