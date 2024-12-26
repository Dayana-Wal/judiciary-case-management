
using CaseManagement.API.Common;
using CaseManagement.Business.Common;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CaseManagement.API.Middlewares
{
    public class JwtAuthMiddleware
    {
        private readonly JwtSettings _jwtSettings;
        private readonly RequestDelegate _next;
        public JwtAuthMiddleware(RequestDelegate next,IOptions<JwtSettings> jwtSettings)
        {
            _next = next;
            _jwtSettings = jwtSettings.Value;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {

                var claims = ValidateToken(token);
                if (claims != null)
                {
                    context.User = claims;
                    await _next(context);
                }
                else
                {
                    await SendResponse.ResponseWithError(context, 401, "Invalid or expired token");
                }

            }
            else
            {
                await SendResponse.ResponseWithError(context, 400, "Token not found");
            }

        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero
            };

            return tokenHandler.ValidateToken(token, validationParameters, out _);
        }
    }
}
