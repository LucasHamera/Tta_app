using System;
using System.Threading.Tasks;
using Convey;
using Convey.Docs.Swagger;
using Convey.Logging;
using Convey.MessageBrokers.RabbitMQ;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TtaApp.Shared.Application;
using TtaApp.Shared.Infrastructure;
using TtaApp.Todo.Api;

namespace TtaApp.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
            => await WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services
                        .AddControllers()
                        .AddNewtonsoftJson();

                    services
                        .AddConvey()
                        .AddTodoModule()
                        .AddSharedApplication()
                        .AddSharedInfrastructure()
                        .AddSwaggerDocs()
                        .AddRabbitMq()
                        .Build();

                    services
                        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(options =>
                        {
                            options.Authority = "https://localhost:5002";
                            options.Audience = "weatherapi";
                            options.TokenValidationParameters = new TokenValidationParameters()
                            {
                                NameClaimType = "name"
                            };
                        });
                })
                .Configure((webHostBuilder, app) =>
                {
                    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                    var isDevelopment = environment == "Development";

                    app
                        .UseConvey()
                        .UseRabbitMq();

                    if (isDevelopment)
                    {
                        app.UseDeveloperExceptionPage();

                        app
                            .UseWebSockets()
                            .UseSwaggerDocs();
                    }

                    app
                        .UseSharedApplication()
                        .UseSharedInfrastructure()
                        .UseTodoModule();

                    app.UseCors(config =>
                    {
                        config.AllowAnyOrigin();
                        config.AllowAnyMethod();
                        config.AllowAnyHeader();
                    });

                    app.UseHttpsRedirection();

                    app.UseRouting();

                    app.UseAuthorization();

                    app.UseAuthorization();

                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers();
                    });
                })
                .UseLogging()
                .Build()
                .RunAsync();

    }
}
