using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using TtaApp.Api;
using TtaApp.Todo.Application.Todos.Commands;
using TtaApp.Todo.Application.Todos.Queries;
using TtaApp.Todo.Application.Todos.Queries.DTOs;
using TtaApp.Todo.Domain.Todos.Exceptions;
using TtaApp.Todo.Tests.Shared.Factories;
using Xunit;

namespace TtaApp.Todo.Tests.Integration.Application.Todo.Command
{
    public class AddTodoHandlerTests : IClassFixture<TestApplicationFactory<Program>>, IDisposable
    {
        private Task ActCommand(
            AddTodo command
        ) => _commandHandler.HandleAsync(command);

        private Task<IEnumerable<TodoDTO>> ActQuery() 
            => _queryHandler.HandleAsync(new GetTodos());

        [Fact]
        public async Task GivenValidDataShouldAdded()
        {
            // Arrange
            var id = Guid.Parse("27452690-02D6-4844-A3A3-F6F67A4E4784");
            var name = "Test";
            var command = new AddTodo(
                id,
                name
            );

            // Act
            await ActCommand(command);
            var todos = await ActQuery();

            // Assert
            todos.Should().NotBeNull();
            todos.Should().Contain(
                new TodoDTO(id, name, false)
            );
        }

        [Fact]
        public async Task GivenEmptyNameShouldThrowEmptyTodoNameException()
        {
            // Arrange
            var id = Guid.Parse("20511505-2EEF-45D7-AC91-6FF8CFB97FCA");
            var name = string.Empty;
            var command = new AddTodo(
                id,
                name
            );

            // Act
            var exception = await Record.ExceptionAsync(
                async () => await ActCommand(command)
            );

            // Assert
            exception.Should().NotBeNull();
            var emptyTodoName = exception as EmptyTodoNameException;
            emptyTodoName.Should().NotBeNull();
        }

        private readonly IServiceScope _scope;
        private readonly ICommandHandler<AddTodo> _commandHandler;
        private readonly IQueryHandler<GetTodos, IEnumerable<TodoDTO>> _queryHandler;

        public AddTodoHandlerTests(
            TestApplicationFactory<Program> factory
        )
        {
            _scope = factory.Services.CreateScope();
            _commandHandler = _scope.ServiceProvider.GetService<ICommandHandler<AddTodo>>();
            _queryHandler = _scope.ServiceProvider.GetService<IQueryHandler<GetTodos, IEnumerable<TodoDTO>>>();
        }

        public void Dispose()
        {
            _scope.Dispose();
        }
    }
}
