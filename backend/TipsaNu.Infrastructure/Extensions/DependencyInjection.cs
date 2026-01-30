using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TipsaNu.Application.Feature.Auth.Interfaces;
using TipsaNu.Domain.Interfaces;
using TipsaNu.Infrastructure.Auth;
using TipsaNu.Infrastructure.Presistence;
using TipsaNu.Infrastructure.Repositories;

namespace TipsaNu.Infrastructure.Extensions
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddScoped(typeof(IGenericInterface<>), typeof(GenericRepository<>));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<IPasswordService, PasswordService>();

            return services;
        }
    }
}