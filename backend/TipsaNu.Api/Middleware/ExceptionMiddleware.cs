using System.Net;
using System.Text.Json;
using TipsaNu.Application.Commons.Results;

namespace TipsaNu.Api.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

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

                var result = OperationResult<object>.Failure(BuildErrorMessages(ex));

                await context.Response.WriteAsync(JsonSerializer.Serialize(result, _jsonOptions));
            }
        }

        private List<string> BuildErrorMessages(Exception ex)
        {
            var messages = new List<string>();

            if (_env.IsDevelopment())
            {
                var current = ex;
                while (current != null)
                {
                    messages.Add(current.Message);
                    current = current.InnerException;
                }
            }
            else
            {
                messages.Add("Something went wrong.");
            }

            return messages;
        }
    }
}
