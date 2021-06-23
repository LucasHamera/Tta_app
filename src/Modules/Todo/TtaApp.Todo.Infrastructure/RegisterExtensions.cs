using Convey;
using Microsoft.AspNetCore.Builder;
using TtaApp.Todo.Infrastructure.Application;
using TtaApp.Todo.Infrastructure.Database;
using TtaApp.Todo.Infrastructure.Domain;
using TtaApp.Todo.Infrastructure.Services;

namespace TtaApp.Todo.Infrastructure
{
    public static class RegisterExtensions
    {
        public static IConveyBuilder AddInfrastructure(
            this IConveyBuilder builder
        )
        {
            return builder
                .AddDomainModule()
                .AddDatabaseModule()
                .AddApplicationModule()
                .AddDomainEventHandlers();
        }

        public static IApplicationBuilder UseInfrastructure(
            this IApplicationBuilder app
        )
        {
            return app
                .UseDatabaseModule()
                .UseDomainModule();
        }
    }
}
