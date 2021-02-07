using Convey;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TtaApp.Todo.Application.Services;
using TtaApp.Todo.Infrastructure.Application.Services;

namespace TtaApp.Todo.Infrastructure.Application
{
    internal static class RegisterExtensions
    {
        public static IConveyBuilder AddApplicationModule(
            this IConveyBuilder builder
        )
        {
            builder
                .Services
                .AddTransient<ITransactionCommitter, TransactionCommitter>();

            return builder;
        }

        public static IApplicationBuilder UseApplicationModule(
            this IApplicationBuilder app
        )
        {
            return app;
        }
    }
}
