using Convey;
using Microsoft.AspNetCore.Builder;

namespace TtaApp.Todo.Application
{
    public static class RegisterExtensions
    {
        public static IConveyBuilder AddApplication(
            this IConveyBuilder builder
        )
        {
            return builder;
        }

        public static IApplicationBuilder UseApplication(
            this IApplicationBuilder app
        )
        {
            return app;
        }
    }
}
