using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Convey.CQRS.Queries;
using Microsoft.AspNetCore.Builder;
using TtaApp.Shared.Application.Exceptions;

namespace TtaApp.Shared.Application
{
    public static class RegisterExtensions
    {
        public static IConveyBuilder AddSharedApplication(
            this IConveyBuilder builder
        )
        {
            builder
                .AddInMemoryCommandDispatcher()
                .AddInMemoryQueryDispatcher()
                .AddInMemoryEventDispatcher()
                .AddCommandHandlers()
                .AddQueryHandlers()
                .AddEventHandlers();

            return builder;
        }

        public static IApplicationBuilder UseSharedApplication(
            this IApplicationBuilder app
        )
        {
            app.UseErrorHandling();
            return app;
        }
    }
}
