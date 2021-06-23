using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TtaApp.Api;
using TtaApp.Todo.Application.Todos.Commands;
using TtaApp.Todo.Domain.Todos.Services;
using TtaApp.Todo.Tests.Shared.Factories;
using Xunit;

namespace TtaApp.Todo.Tests.EndToEnd.Application.Todo.Command
{
    public class AddTodoHandlerTests : IClassFixture<TestApplicationFactory<Program>>, IDisposable
    {
        private Task<HttpResponseMessage> Act(AddTodo command)
            => _httpClient.PostAsync("Todo/Add", GetContent(command));

        [Fact]
        public async Task GivenValidDataShouldAdded()
        {
            // Arrange
            var id = Guid.Parse("CEDA3B23-C934-4F9F-9D14-A0071AB11E98");
            var name = "Test";
            var command = new AddTodo(
                id,
                name
            );

            // Act
            var response = await Act(command);

            // Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var addedTodo = await _todoRepository.GetByIdAsync(command.Id);
            addedTodo.Should().NotBeNull();
            addedTodo.HasValue.Should().BeTrue();
        }

        [Fact]
        public async Task GivenEmptyNameShouldResponse400()
        {
            // Arrange
            var id = Guid.Parse("A0409AB8-C651-4FD5-A4EF-183B3C8BB6B8");
            var name = string.Empty;
            var command = new AddTodo(
                id,
                name
            );

            // Act
            var response = await Act(command);

            // Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        private readonly IServiceScope _scope;
        private readonly ITodoRepository _todoRepository;
        private readonly HttpClient _httpClient;

        public AddTodoHandlerTests(
            TestApplicationFactory<Program> factory
        )
        {
            _scope = factory.Services.CreateScope();
            _todoRepository = _scope.ServiceProvider.GetService<ITodoRepository>();
            _httpClient = factory.CreateClient();
        }
        private static StringContent GetContent(object value)
            => new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}
