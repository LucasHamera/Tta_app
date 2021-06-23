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
    public class DeleteTodoHandlerTests : IClassFixture<TestApplicationFactory<Program>>, IDisposable
    {
        private Task<HttpResponseMessage> Act(DeleteTodo command)
            => _httpClient.PostAsync("Todo/Delete", GetContent(command));

        [Fact]
        public async Task GivenValidDataShouldChanged()
        {
            // Arrange
            var testTodo = await AddTestTodo(
                Guid.Parse("25F1087E-AADC-4DBB-8CE8-6286FB9CF22E")
            );
            var command = new DeleteTodo(
                testTodo.Id
            );

            // Act
            var response = await Act(command);

            // Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var changedTodo = await _todoRepository.GetByIdAsync(command.Id);
            changedTodo.Should().NotBeNull();
            changedTodo.HasValue.Should().BeFalse();
        }

        [Fact]
        public async Task GivenNotExistsShouldResponse400()
        {
            // Arrange
            var id = Guid.Parse("45D3AE88-4E5F-4184-8A97-67C47B225954");
            var command = new DeleteTodo(
                id
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

        public DeleteTodoHandlerTests(
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
