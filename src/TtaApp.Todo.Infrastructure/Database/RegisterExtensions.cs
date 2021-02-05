using System;
using Convey;
using Convey.Persistence.MongoDB;
using Microsoft.AspNetCore.Builder;
using TtaApp.Todo.Infrastructure.Database.Documents;

namespace TtaApp.Todo.Infrastructure.Database
{
    internal static class RegisterExtensions
    {
        public static IConveyBuilder AddDatabaseModule(
            this IConveyBuilder builder
        )
        {
            builder
                .AddMongo()
                .AddMongoRepository<TodoDocument, Guid>("todos");
                        
            return builder;
        }

        public static IApplicationBuilder UseDatabaseModule(
            this IApplicationBuilder app
        )
        {
            return app;
        }
    }
}
