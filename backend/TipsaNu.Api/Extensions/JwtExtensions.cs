using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TipsaNu.Api.Extensions
{
    public static class JwtExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
        {
            // Läs konfiguration och kasta om något saknas
            var secret = config["Jwt:Secret"]
                ?? throw new InvalidOperationException("Jwt:Secret saknas i appsettings.json");
            var key = Encoding.UTF8.GetBytes(secret);

            var issuer = config["Jwt:Issuer"]
                ?? throw new InvalidOperationException("Jwt:Issuer saknas i appsettings.json");

            var audience = config["Jwt:Audience"]
                ?? throw new InvalidOperationException("Jwt:Audience saknas i appsettings.json");

            // Lägg till JWT Bearer authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; // Sätt true i prod
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }
    }
}
