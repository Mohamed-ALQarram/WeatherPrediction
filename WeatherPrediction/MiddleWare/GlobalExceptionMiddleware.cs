using System.Net;
using System.Text.Json;

namespace WeatherPrediction.Middle_ware
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Continue pipeline
                await _next(context);
            }
            catch (Exception ex)
            {
                // Log error
                _logger.LogError(ex, "Unhandled exception occurred while processing request");

                // Return clean JSON response
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = JsonSerializer.Serialize(new
            {
                success = false,
                error = "An unexpected error occurred.",
                details = exception.Message // ⚠️ for debugging only. In production, hide details.
            });

            return context.Response.WriteAsync(result);
        }
    }
}
