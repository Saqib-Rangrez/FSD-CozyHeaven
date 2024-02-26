namespace CozyHavenStayServer.Interfaces
{
    public interface ITokenBlacklistService
    {
        void AddTokenToBlacklist(string token, DateTime expiryTime);
        bool IsTokenBlacklisted(string token);
        void CleanupExpiredTokens();
    }
}
