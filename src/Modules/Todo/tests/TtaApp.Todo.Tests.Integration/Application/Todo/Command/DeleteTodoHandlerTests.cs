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
    public class DeleteTodoHandlerTests : IClassFixture<TestApplicationFactory<Program>>, IDisposable
    {
        private Task ActCommand(
            DeleteTodo command
        ) => _commandHandler.HandleAsync(command);

        private Task<IEnumerable<TodoDTO>> ActQuery()
            => _queryHandler.HandleAsync(new GetTodos());

        [Fact]
        public async Task GivenValidDataShouldDeleted()
        {
            // Arrange
            var todo = await AddTestTodo(
                Guid.Parse("32532941-8807-48CC-A83A-51037077D224")
            );
            var command = new DeleteTodo(
                todo.Id
            );

            // Act
            await ActCommand(command);
            var todos = await ActQuery();

            // Assert
            todos.Should().NotBeNull();
            todos!.Should().NotContain(
                new TodoDTO(todo.Id, todo.Name.Value, false)
            );
        }

        [Fact]
        public async Task GivenNotExistsTodoShouldThrowTodoNotFoundException()
        {
            // Arrange
            var command = new DeleteTodo(
                Guid.Parse("A81BE3B8-97FC-41F1-AF4F-89C790943A32")
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
        private readonly ICommandHandler<DeleteTodo> _commandHandler;
        private readonly IQueryHandler<GetTodos, IEnumerable<TodoDTO>> _queryHandler;

        public DeleteTodoHandlerTests(
            TestApplicationFactory<Program> factory
        )
        {
            _scope = factory.Services.CreateScope();
            _todoRepository = _scope.ServiceProvider.GetService<ITodoRepository>();
            _commandHandler = _scope.ServiceProvider.GetService<ICommandHandler<DeleteTodo>>();
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
