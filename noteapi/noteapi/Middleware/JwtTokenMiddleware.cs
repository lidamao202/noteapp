using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace noteapi.Middleware
{
    public class JwtTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<JwtTokenMiddleware> _logger;

        public JwtTokenMiddleware(RequestDelegate next, ILogger<JwtTokenMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                var token = authHeader.ToString().Split(" ").Last();

                var handler = new JwtSecurityTokenHandler();
                if (handler.CanReadToken(token))
                {
                    try
                    {
                        // Decode the token
                        var jwtToken = handler.ReadJwtToken(token);

                        // Store claims in HttpContext.Items
                        context.Items["JwtClaims"] = jwtToken.Claims.ToDictionary(c => c.Type, c => c.Value);

                        // Optional: Store specific claims as needed
                        context.Items["UserId"] = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
                    }
                    catch (Exception ex)
                    {
                        // Log the exception
                        _logger.LogError(ex, "Error decoding JWT token");
                    }
                }
                else
                {
                    _logger.LogWarning("Invalid JWT token format");
                }
            }

            context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'");

            // Call the next middleware in the pipeline
            await _next(context);
        }
    }
}




