using Microsoft.AspNetCore.Builder;

namespace TtaApp.Shared.Application.Exceptions
{
    public static class ErrorHandlingExtensions
    {
        public static IApplicationBuilder UseErrorHandling(
            this IApplicationBuilder app
        )
        {
            app.UseMiddleware<ExceptionMiddleware>();
            return app;
        }
    }
}
