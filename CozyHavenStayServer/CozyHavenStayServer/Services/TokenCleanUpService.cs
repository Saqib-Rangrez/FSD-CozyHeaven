using CozyHavenStayServer.Interfaces;

namespace CozyHavenStayServer.Services
{
    public class TokenCleanUpService : BackgroundService
    {
        private readonly ITokenBlacklistService _tokenBlacklistService;
        private readonly TimeSpan _cleanupInterval = TimeSpan.FromMinutes(60);

        public TokenCleanUpService(ITokenBlacklistService tokenBlacklistService)
        {
            _tokenBlacklistService = tokenBlacklistService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _tokenBlacklistService.CleanupExpiredTokens();
                await Task.Delay(_cleanupInterval, stoppingToken);
            }
        }
    }

}
