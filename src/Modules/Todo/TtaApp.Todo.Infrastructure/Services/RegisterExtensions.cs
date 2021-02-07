using System;
using Convey;
using Microsoft.Extensions.DependencyInjection;

namespace TtaApp.Todo.Infrastructure.Services
{
    internal static class RegisterExtensions
    {
        public static IConveyBuilder AddDomainEventHandlers(
            this IConveyBuilder builder
        )
        {
            builder
                .Services
                .Scan(s => s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                    .AddClasses(c => c.AssignableTo(typeof(IDomainEventHandler<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()
                );

            return builder;
        }
    }
}
