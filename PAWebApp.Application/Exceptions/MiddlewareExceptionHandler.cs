using Microsoft.Extensions.Logging;
using System.Net.Mime;
using System.Net;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace PAWebApp.Application.Exceptions
{
    internal class MiddlewareExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MiddlewareExceptionHandler> _logger;

        private static readonly JsonSerializerOptions _defaultOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        public MiddlewareExceptionHandler(RequestDelegate next, ILogger<MiddlewareExceptionHandler> logger)
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
                await HandleGlobalExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleGlobalExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, exception.Message);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var error = new GlobalErrorModel
            {
                Message = exception.Message
            };

            if (exception is BaseHttpException baseException)
            {
                context.Response.StatusCode = (int)baseException.StatusCode;
            }

            var json = JsonSerializer.Serialize(error, _defaultOptions);

            context.Response.ContentType = MediaTypeNames.Application.Json;
            return context.Response.WriteAsync(json);
        }
    }
}
