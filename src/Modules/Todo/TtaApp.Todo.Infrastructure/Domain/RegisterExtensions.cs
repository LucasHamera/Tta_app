using Convey;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TtaApp.Todo.Domain.Todos.Services;
using TtaApp.Todo.Infrastructure.Domain.Todos;
using TtaApp.Todo.Infrastructure.Domain.Todos.Services;

namespace TtaApp.Todo.Infrastructure.Domain
{
    internal static class RegisterExtensions
    {
        public static IConveyBuilder AddDomainModule(
            this IConveyBuilder builder
        )
        {
            builder
                .Services
                .AddTransient<ITodoRepository, TodoRepository>();

            return builder;
        }

        public static IApplicationBuilder UseDomainModule(
            this IApplicationBuilder app
        )
        {
            return app;
        }
    }
}
