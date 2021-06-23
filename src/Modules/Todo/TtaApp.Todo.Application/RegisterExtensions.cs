using System.Runtime.CompilerServices;
using Convey;
using Microsoft.AspNetCore.Builder;

[assembly: InternalsVisibleTo("TtaApp.Todo.Tests.Unit")]
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
