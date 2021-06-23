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
    public class ChangeTodoStatusHandlerTests : IClassFixture<TestApplicationFactory<Program>>, IDisposable
    {
        private Task<HttpResponseMessage> Act(ChangeTodoStatus command)
            => _httpClient.PostAsync("Todo/ChangeStatus", GetContent(command));

        [Fact]
        public async Task GivenValidDataShouldChanged()
        {
            // Arrange
            var testTodo = await AddTestTodo(
                Guid.Parse("8E591A57-5DFA-4E22-B781-89EDF3018836")
            );
            var command = new ChangeTodoStatus(
                testTodo.Id,
                true
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
            changedTodo.Value.Done.Should().Be(command.Done);
        }

        [Fact]
        public async Task GivenNotExistsShouldResponse400()
        {
            // Arrange
            var id = Guid.Parse("A827F260-C868-491E-A97F-E9AA1295CA2E");
            var command = new ChangeTodoStatus(
                id,
                true
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

        public ChangeTodoStatusHandlerTests(
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
