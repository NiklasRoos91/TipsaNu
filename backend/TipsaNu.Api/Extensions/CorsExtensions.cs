namespace TipsaNu.Api.Extensions
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddCustomCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    var allowedOriginsEnv = Environment.GetEnvironmentVariable("ALLOWED_ORIGINS");
                    
                    var origins = !string.IsNullOrEmpty(allowedOriginsEnv)
                        ? allowedOriginsEnv.Split(',', StringSplitOptions.RemoveEmptyEntries)
                        : ["http://localhost:3000"];

                    policy.WithOrigins(origins)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            return services;
        }
    }
}
