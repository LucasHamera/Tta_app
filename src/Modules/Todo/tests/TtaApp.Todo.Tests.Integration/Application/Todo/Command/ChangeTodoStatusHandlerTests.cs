using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Convey.CQRS.Commands;
using Convey.CQRS.Queries;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using TtaApp.Api;
using TtaApp.Todo.Application.Todos.Commands;
using TtaApp.Todo.Application.Todos.Exceptions;
using TtaApp.Todo.Application.Todos.Queries;
using TtaApp.Todo.Application.Todos.Queries.DTOs;
using TtaApp.Todo.Domain.Todos.Exceptions;
using TtaApp.Todo.Domain.Todos.Services;
using TtaApp.Todo.Domain.Todos.ValueObjects;
using TtaApp.Todo.Tests.Shared.Factories;
using Xunit;

namespace TtaApp.Todo.Tests.Integration.Application.Todo.Command
{
    public class ChangeTodoStatusHandlerTests : IClassFixture<TestApplicationFactory<Program>>, IDisposable
    {
        private Task ActCommand(
            ChangeTodoStatus command
        ) => _commandHandler.HandleAsync(command);

        private Task<IEnumerable<TodoDTO>> ActQuery()
            => _queryHandler.HandleAsync(new GetTodos());

        [Fact]
        public async Task GivenValidDataShouldChanged()
        {
            // Arrange
            var todo = await AddTestTodo(
                Guid.Parse("5A3E5BA7-A7A3-42D5-A195-327636421710")
            );
            var command = new ChangeTodoStatus(
                todo.Id,
                true
            );

            // Act
            await ActCommand(command);
            var todos = await ActQuery();

            // Assert
            todos.Should().NotBeNull();
            todos!.Should().Contain(
                new TodoDTO(todo.Id, todo.Name.Value, command.Done)
            );
        }
        
        [Fact]
        public async Task GivenNotExistsTodoShouldThrowTodoNotFoundException()
        {
            // Arrange
            var command = new ChangeTodoStatus(
                Guid.Parse("1C619510-EF54-40AE-90C7-BA7E510ADD57"),
                true
            );

            // Act
            var exception = await Record.ExceptionAsync(
                async () => await ActCommand(command)
            );

            // Assert
            exception.Should().NotBeNull();
            var todoNotFound = exception as TodoNotFoundException;
            todoNotFound.Should().NotBeNull();
        }

        private readonly IServiceScope _scope;
        private readonly ITodoRepository _todoRepository;
        private readonly ICommandHandler<ChangeTodoStatus> _commandHandler;
        private readonly IQueryHandler<GetTodos, IEnumerable<TodoDTO>> _queryHandler;

        public ChangeTodoStatusHandlerTests(
            TestApplicationFactory<Program> factory
        )
        {
            _scope = factory.Services.CreateScope();
            _todoRepository = _scope.ServiceProvider.GetService<ITodoRepository>();
            _commandHandler = _scope.ServiceProvider.GetService<ICommandHandler<ChangeTodoStatus>>();
            _queryHandler = _scope.ServiceProvider.GetService<IQueryHandler<GetTodos, IEnumerable<TodoDTO>>>();
        }

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
