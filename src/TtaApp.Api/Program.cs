using System.Threading.Tasks;
using Convey;
using Convey.Docs.Swagger;
using Convey.Logging;
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
            => await CreateWebHostBuilder(args)
                .Build()
                .RunAsync();

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
            => WebHost.CreateDefaultBuilder(args)
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
                .Configure((_, app) =>
                {
                    app
                        .UseConvey()
                        .UseWhenDebugEnvironment(
                            () =>
                            {
                                app
                                    .UseDeveloperExceptionPage()
                                    .UseWebSockets()
                                    .UseSwaggerDocs();
                            }
                        )
                        .UseSharedApplication()
                        .UseSharedInfrastructure()
                        .UseTodoModule()
                        .UseCors(config =>
                        {
                            config.AllowAnyOrigin();
                            config.AllowAnyMethod();
                            config.AllowAnyHeader();
                        })
                        .UseHttpsRedirection()
                        .UseRouting()
                        .UseAuthorization()
                        .UseAuthorization()
                        .UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllers();
                        });
                })
                .UseLogging();

    }
}
