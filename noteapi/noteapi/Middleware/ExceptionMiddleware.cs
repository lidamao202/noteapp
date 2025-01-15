using System.Net;
using System.Text.Json;

namespace noteapi.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Call the next middleware
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex); // Handle exceptions globally
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Set the response status code and content type
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            // Create a standardized error response
            var errorResponse = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "An unexpected error occurred.",
                //Details = exception.Message // Optional: Remove in production for security
            };

            var errorJson = JsonSerializer.Serialize(errorResponse);
            return context.Response.WriteAsync(errorJson);
        }
    }
}
