using Microsoft.AspNetCore.Builder;

namespace PAWebApp.Application.Exceptions
{
    public static class GlobalExceptionMiddleware
    {
        public static void UseGlobalExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<MiddlewareExceptionHandler>();
        }
    }
}
