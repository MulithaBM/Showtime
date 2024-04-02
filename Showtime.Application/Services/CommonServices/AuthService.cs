using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Showtime.Core.Enums;
using Showtime.Core.Interfaces.Services.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Showtime.Application.Services.Common
{
    public class AuthService(IConfiguration configuration, ILogger<AuthService> logger) : IAuthService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly ILogger<AuthService> _logger = logger;

        public string? CreateToken(string id, SystemRoles role)
        {
            try
            {
                var secret = _configuration["JwtSettings:SecretKey"];
                if (string.IsNullOrEmpty(secret))
                {
                    throw new FormatException("JwtSettings:Secret is not properly set");
                }

                var issuer = _configuration["JwtSettings:Issuer"];
                if (string.IsNullOrEmpty(issuer))
                {
                    throw new FormatException("JwtSettings:Issuer is not properly set");
                }

                var audience = _configuration["JwtSettings:Audience"];
                if (string.IsNullOrEmpty(audience))
                {
                    throw new FormatException("JwtSettings:Audience is not properly set");
                }

                var tokenLifetime = _configuration["JwtSettings:TokenLifetimeInMinutes"];
                if (string.IsNullOrEmpty(tokenLifetime))
                {
                    throw new FormatException("JwtSettings:TokenLifetime is not properly set in");
                }

                List<Claim> claims =
                [
                    new Claim(JwtRegisteredClaimNames.Sub, id),
                    new Claim(JwtRegisteredClaimNames.Iss, issuer),
                    new Claim(JwtRegisteredClaimNames.Aud, audience),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")),
                    new Claim(JwtRegisteredClaimNames.Exp, DateTime.UtcNow.AddMinutes(Convert.ToInt32(tokenLifetime)).ToString("yyyy-MM-dd HH:mm:ss")),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                    new Claim(ClaimTypes.Role, role.ToString())
                ];

                SymmetricSecurityKey key = new(Encoding.ASCII.GetBytes(secret));
                SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha512);

                SecurityTokenDescriptor tokenDescriptor = new()
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(tokenLifetime)),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
            }
        }
    }
}
