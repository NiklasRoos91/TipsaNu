using MediatR;
using Microsoft.Extensions.Logging;

namespace TipsaNu.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Log the request
            logger.LogInformation("Handling {RequestName} with {Request}", typeof(TRequest).Name, request);

            try
            {
                // Call the next behavior in the pipeline
                var response = await next();

                // Log the response
                logger.LogInformation("Handled {RequestName} with {Response}", typeof(TRequest).Name, response);

                return response;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error handling {RequestName} with {@Request}", typeof(TRequest).Name, request);
                throw;
            }
        }
    }
}