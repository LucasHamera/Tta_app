using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace TtaApp.Todo.Tests.Shared.Factories
{
    public class TestApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
    {
        protected override IWebHostBuilder CreateWebHostBuilder()
            => base
                .CreateWebHostBuilder()
                .UseEnvironment("tests");
    }
}
