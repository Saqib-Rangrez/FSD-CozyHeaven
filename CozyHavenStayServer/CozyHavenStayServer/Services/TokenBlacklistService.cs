using CozyHavenStayServer.Interfaces;
using System.Linq;

namespace CozyHavenStayServer.Services
{
    public class TokenBlacklistService : ITokenBlacklistService
    {
        private readonly HashSet<TokenEntry> _invalidTokens = new HashSet<TokenEntry>();

        private readonly object _lock = new object();

        public void AddTokenToBlacklist(string token, DateTime expiryTime)
        {
            lock (_lock)
            {
                _invalidTokens.Add(new TokenEntry { Token = token, ExpiryTime = expiryTime });
            }
        }

        public bool IsTokenBlacklisted(string token)
        {
            lock (_lock)
            {
                return _invalidTokens.Any(entry => entry.Token == token && entry.ExpiryTime > DateTime.UtcNow);
            }
        }

        public void CleanupExpiredTokens()
        {
            lock (_lock)
            {
                _invalidTokens.RemoveWhere(entry => entry.ExpiryTime <= DateTime.UtcNow);
            }
        }

        private class TokenEntry
        {
            public string Token { get; set; }
            public DateTime ExpiryTime { get; set; }
        }

    }
}
