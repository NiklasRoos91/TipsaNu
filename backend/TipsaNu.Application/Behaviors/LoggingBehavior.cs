using MediatR;
using Microsoft.Extensions.Logging;

namespace TipsaNu.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Log the request
            _logger.LogInformation("Handling {RequestName} with {Request}", typeof(TRequest).Name, request);

            try
            {
                // Call the next behavior in the pipeline
                var response = await next();

                // Log the response
                _logger.LogInformation("Handled {RequestName} with {Response}", typeof(TRequest).Name, response);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error handling {RequestName} with {@Request}", typeof(TRequest).Name, request);
                throw;
            }
        }
    }
}