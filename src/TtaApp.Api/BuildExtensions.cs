using System;
using Microsoft.AspNetCore.Builder;

namespace TtaApp.Api
{
    internal static class BuildExtensions
    {
        public static IApplicationBuilder UseForEnvironment(
            this IApplicationBuilder builder,
            string environment,
            Action<IApplicationBuilder> builderMethod
        )
        {
            var currentEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (!(currentEnvironment?.Equals(environment) ?? false))
                return builder;

            builderMethod(builder);
            return builder;
        }

        public static IApplicationBuilder UseForEnvironment(
            this IApplicationBuilder builder,
            string environment,
            Action builderMethod
        )
        {
            var currentEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (!(currentEnvironment?.Equals(environment) ?? false))
                return builder;

            builderMethod();
            return builder;
        }

        public static IApplicationBuilder UseWhenDebugEnvironment(
            this IApplicationBuilder builder,
            Action<IApplicationBuilder> builderMethod
        ) => UseForEnvironment(
            builder,
            "Development",
            builderMethod
        );

        public static IApplicationBuilder UseWhenDebugEnvironment(
            this IApplicationBuilder builder,
            Action builderMethod
        ) => UseForEnvironment(
            builder,
            "Development",
            builderMethod
        );
    }
}
