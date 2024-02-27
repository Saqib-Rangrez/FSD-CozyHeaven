namespace CozyHavenStayServer.Mappers
{
    public class EmailConfiguration
    {
        public string SmtpHost { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string From { get; set; }
    }
}
