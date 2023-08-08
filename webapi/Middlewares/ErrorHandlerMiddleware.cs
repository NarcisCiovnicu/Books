using Domain.CustomExceptions;
using System.Text.Json;

namespace webapi.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException error)
            {
                await HandleErrorAsync(context, error, StatusCodes.Status400BadRequest);
            }
            catch (Exception error)
            {
                await HandleErrorAsync(context, error, StatusCodes.Status500InternalServerError);
            }
        }

        private Task HandleErrorAsync(HttpContext context, Exception error, int statusCode)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.Headers["CustomErrorResponse"] = "true";
            response.StatusCode = statusCode;

            var result = JsonSerializer.Serialize(new
            {
                error.Message,
                statusCode
            });


            _logger.LogError(error, "");

            return response.WriteAsync(result);
        }
    }
}
