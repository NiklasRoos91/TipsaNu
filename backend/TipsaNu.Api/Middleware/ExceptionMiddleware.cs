using System.Net;
using System.Text.Json;
using TipsaNu.Application.Commons.Results;

namespace TipsaNu.Api.Middleware
{
    public class ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, IHostEnvironment env) : IMiddleware
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled exception");

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var result = OperationResult<object>.Failure(BuildErrorMessages(ex));

                await context.Response.WriteAsync(JsonSerializer.Serialize(result, JsonOptions));
            }
        }

        private List<string> BuildErrorMessages(Exception ex)
        {
            var messages = new List<string>();

            if (env.IsDevelopment())
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
