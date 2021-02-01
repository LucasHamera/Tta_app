using Convey;
using Convey.WebApi.Exceptions;
using Microsoft.Extensions.DependencyInjection;
using TtaApp.Shared.Application.Exceptions;

namespace TtaApp.Shared.Infrastructure.Exceptions
{
    internal static class RegisterExtensions
    {
        public static IConveyBuilder AddExceptionsModule(
            this IConveyBuilder builder
        )
        {
            builder
                .Services
                .AddTransient<IExceptionCompositionRoot, ExceptionCompositionRoot>()
                .AddSingleton<IExceptionToResponseMapper, MainExceptionToResponseMapper>();
            return builder;
        }
    }
}
