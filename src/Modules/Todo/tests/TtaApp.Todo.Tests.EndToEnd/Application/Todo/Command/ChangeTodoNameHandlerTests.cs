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
using TtaApp.Todo.Domain.Todos.ValueObjects;
using TtaApp.Todo.Tests.Shared.Factories;
using Xunit;

namespace TtaApp.Todo.Tests.EndToEnd.Application.Todo.Command
{
    public class ChangeTodoNameHandlerTests : IClassFixture<TestApplicationFactory<Program>>, IDisposable
    {
        private Task<HttpResponseMessage> Act(ChangeTodoName command)
            => _httpClient.PostAsync("Todo/ChangeName", GetContent(command));

        [Fact]
        public async Task GivenValidDataShouldChanged()
        {
            // Arrange
            var testTodo = await AddTestTodo(
                Guid.Parse("91314B9A-1B47-44A3-A52E-29AC3BFC19F0")
            );
            var name = "NewTest";
            var command = new ChangeTodoName(
                testTodo.Id,
                name
            );

            // Act
            var response = await Act(command);

            // Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var changedTodo = await _todoRepository.GetByIdAsync(command.Id);
            changedTodo.Should().NotBeNull();
            changedTodo.HasValue.Should().BeTrue();
            changedTodo.Value.Id.Value.Should().Be(testTodo.Id);
            changedTodo.Value.Name.Value.Should().Be(name);
        }

        [Fact]
        public async Task GivenEmptyNameShouldResponse400()
        {
            // Arrange
            var testTodo = await AddTestTodo(
                Guid.Parse("3FED80A9-6A9C-4342-9401-49EAC3912C3D")
            );
            var name = string.Empty;
            var command = new ChangeTodoName(
                testTodo.Id,
                name
            );

            // Act
            var response = await Act(command);

            // Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task GivenNotExistsShouldResponse400()
        {
            // Arrange
            var id = Guid.Parse("58C86ABA-B38C-4F32-B05A-7019CD497E0F");
            var name = string.Empty;
            var command = new ChangeTodoName(
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

        public ChangeTodoNameHandlerTests(
            TestApplicationFactory<Program> factory
        )
        {
            _scope = factory.Services.CreateScope();
            _todoRepository = _scope.ServiceProvider.GetService<ITodoRepository>();
            _httpClient = factory.CreateClient();
        }
        private static StringContent GetContent(object value)
            => new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");

        private async Task<Domain.Todos.Todo> AddTestTodo(
            Guid id
        )
        {
            var todo = new Domain.Todos.Todo(
                id,
                new TodoName("Test"),
                false,
                0
            );
            await _todoRepository
                .AddAsync(todo);
            return todo;
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}
