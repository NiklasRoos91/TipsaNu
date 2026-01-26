using System.Net;
using System.Text.Json;
using TipsaNu.Application.Commons.Results;

namespace TipsaNu.Api.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                // Skapar en OperationResult<object> med fel
                var errorMessages = new List<string>
                {
                    _env.IsDevelopment() ? ex.Message : "Something went wrong."
                };

                var result = OperationResult<object>.Failure(errorMessages);

                // Returnerar som JSON
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                await context.Response.WriteAsync(JsonSerializer.Serialize(result, options));
            }
        }
    }

}
