using Convey;
using Microsoft.AspNetCore.Builder;
using TtaApp.Shared.Infrastructure.Exceptions;

namespace TtaApp.Shared.Infrastructure
{
    public static class RegisterExtensions
    {
        public static IConveyBuilder AddSharedInfrastructure(
            this IConveyBuilder builder
        )
        {
            builder
                .AddExceptionsModule();

            return builder;
        }

        public static IApplicationBuilder UseSharedInfrastructure(
            this IApplicationBuilder app
        )
        {
            return app;
        }
    }
}
