using OnePieceAPI.Exceptions.Common;
using System.Net;
using System.Text.Json;

namespace OnePieceAPI.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json; charset=utf-8";

            object response;
            int statusCode;

            switch (exception)
            {
                case BaseApiException apiEx:
                    statusCode = apiEx.StatusCode;
                    response = new
                    {
                        error = new
                        {
                            message = apiEx.Message,
                            code = apiEx.ErrorCode,
                            type = exception.GetType().Name
                        }
                    };
                    break;

                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    response = new
                    {
                        error = new
                        {
                            message = "Ha ocurrido un error interno del servidor",
                            code = "INTERNAL_SERVER_ERROR",
                            type = "InternalServerError"
                        }
                    };
                    break;
            }

            context.Response.StatusCode = statusCode;
            var jsonResponse = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}