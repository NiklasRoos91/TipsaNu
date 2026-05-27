using MediatR;
using TipsaNu.Application.Features.Auth.Commands.ClearExpiredTokens;

namespace TipsaNu.Api.Services.BackgroundServices
{
    public class TokenCleanupService(
        IServiceScopeFactory scopeFactory,
        ILogger<TokenCleanupService> logger) : BackgroundService
    {
        private readonly TimeSpan _cleanupInterval = TimeSpan.FromDays(1);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Token Cleanup Service has started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    logger.LogInformation("Starting cleanup of expired refresh tokens...");

                    using (var scope = scopeFactory.CreateScope())
                    {
                        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                        
                        await mediator.Send(new ClearExpiredTokensCommand(), stoppingToken);
                    }

                    logger.LogInformation("Expired refresh tokens cleanup completed successfully.");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while cleaning up expired refresh tokens.");
                }

                await Task.Delay(_cleanupInterval, stoppingToken);
            }
        }
    }
}