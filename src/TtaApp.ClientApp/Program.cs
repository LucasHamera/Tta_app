using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Configuration;
using TtaApp.ClientApp.Identity;

namespace TtaApp.ClientApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");



            builder
                .Services
                .AddScoped(_ => 
                    new HttpClient
                    {
                        BaseAddress = new Uri("https://localhost:5000/")
                    }
                );


            builder
                .Services
                .AddOidcAuthentication(options =>
                {
                    options.ProviderOptions.Authority = "https://localhost:5002"; //The IdentityServer URL 
                    options.ProviderOptions.ClientId = "client-app"; // The client ID
                    options.ProviderOptions.ResponseType = "code";
                });
            
            await builder.Build().RunAsync();
        }
    }
}
