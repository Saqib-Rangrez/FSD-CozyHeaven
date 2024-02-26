using CozyHavenStayServer.Interfaces;

namespace CozyHavenStayServer.Middleware
{
    public class TokenValidationMiddleware
    {
        private readonly ITokenBlacklistService _tokenBlacklistService;
        private readonly RequestDelegate _next;

        public TokenValidationMiddleware(RequestDelegate next, ITokenBlacklistService tokenBlacklistService)
        {
            _tokenBlacklistService = tokenBlacklistService;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (_tokenBlacklistService.IsTokenBlacklisted(token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Token is invalid");
                return;
            }
            await _next(context);
        }
    }
}
