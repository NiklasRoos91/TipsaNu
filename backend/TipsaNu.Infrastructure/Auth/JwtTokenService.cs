using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TipsaNu.Application.Features.Auth.Interfaces;
using TipsaNu.Domain.Entities;

namespace TipsaNu.Infrastructure.Auth
{
    public sealed class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _config;

        public JwtTokenService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(User user)
        {
            var secret = _config["Jwt:Secret"];
            if (string.IsNullOrWhiteSpace(secret))
                throw new InvalidOperationException("Jwt:Secret saknas i appsettings.json");

            var issuer = _config["Jwt:Issuer"];
            if (string.IsNullOrWhiteSpace(issuer))
                throw new InvalidOperationException("Jwt:Issuer saknas i appsettings.json");

            var audience = _config["Jwt:Audience"];
            if (string.IsNullOrWhiteSpace(audience))
                throw new InvalidOperationException("Jwt:Audience saknas i appsettings.json");

            var expiryHours = _config.GetValue<int?>("Jwt:ExpiryHours") ?? 4;

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("username", user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(expiryHours),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
