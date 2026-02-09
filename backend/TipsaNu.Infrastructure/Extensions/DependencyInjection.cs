using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TipsaNu.Application.Commons.Interfaces;
using TipsaNu.Application.Features.Auth.Interfaces;
using TipsaNu.Domain.Interfaces;
using TipsaNu.Infrastructure.Auth;
using TipsaNu.Infrastructure.Persistence.Seeders;
using TipsaNu.Infrastructure.Presistence;
using TipsaNu.Infrastructure.Repositories;
using TipsaNu.Infrastructure.Services;

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

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IMatchRepository, MatchRepository>();
            services.AddScoped<IGroupStandingRepository, GroupStandingRepository>();
            services.AddScoped<IPredictionRepository, PredictionRepository>();
            services.AddScoped<IPointRuleRepository, PointRuleRepository>();
            services.AddScoped<ILeagueRepository, LeagueRepository>();
            services.AddScoped<IExtraBetRepository, ExtraBetRepository>();

            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();


            using (var serviceProvider = services.BuildServiceProvider())
            {
                var context = serviceProvider.GetRequiredService<AppDbContext>();
                var passwordService = serviceProvider.GetRequiredService<IPasswordService>();

                DBSeeder.SeedAllAsync(context, passwordService).GetAwaiter().GetResult();
            }

            return services;
        }
    }
}