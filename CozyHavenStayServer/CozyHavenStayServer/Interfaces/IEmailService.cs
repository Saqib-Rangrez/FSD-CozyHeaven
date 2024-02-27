namespace CozyHavenStayServer.Interfaces
{
    public interface IEmailService
    {
        bool SendEmailAsync(string toEmail, string subject, string body);
    }
}
