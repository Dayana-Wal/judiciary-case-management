
using CaseManagement.Business.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CaseManagement.API.Middlewares
{
    public class JwtTokenValidatorMiddleware : IMiddleware
    {
        private readonly JwtSettings _jwtSettings;
        public JwtTokenValidatorMiddleware(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                try
                {
                    var claims = ValidateToken(token);
                    if (claims != null)
                    {
                        context.User = claims;
                        await next(context);
                    }
                    else
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync("Invalid or expired token");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"error from token middleware: {ex.Message}");
                }


            }
            else
            {
                Console.WriteLine("Token not found");
            }
        }

        public ClaimsPrincipal? ValidateToken(string token) {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);

            try
            {
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
            catch (Exception ex) { 
                Console.WriteLine($"Token validation with error: {ex.Message}");
                return null;
            }
        }
    }
}
