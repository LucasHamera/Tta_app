using Convey;
using Microsoft.AspNetCore.Builder;
using TtaApp.Todo.Application;
using TtaApp.Todo.Infrastructure;

namespace TtaApp.Todo.Api
{
    public static class RegisterExtensions
    {
        public static IConveyBuilder AddTodoModule(
            this IConveyBuilder builder
        )
        {
            return builder
                .AddApplication()
                .AddInfrastructure();
        }

        public static IApplicationBuilder UseTodoModule(
            this IApplicationBuilder app
        )
        {
            return app
                .UseApplication()
                .UseInfrastructure();
        }
    }
}
