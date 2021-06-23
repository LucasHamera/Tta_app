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
    public class ChangeTodoNameHandlerTests : IClassFixture<TestApplicationFactory<Program>>, IDisposable
    {
        private Task ActCommand(
            ChangeTodoName command
        ) => _commandHandler.HandleAsync(command);

        private Task<IEnumerable<TodoDTO>> ActQuery()
            => _queryHandler.HandleAsync(new GetTodos());

        [Fact]
        public async Task GivenValidDataShouldChanged()
        {
            // Arrange
            var todo = await AddTestTodo(
                Guid.Parse("25A17395-22B8-41CE-9853-ADAB92F6112E")
            );
            var command = new ChangeTodoName(
                todo.Id,
                "NewName"
            );

            // Act
            await ActCommand(command);
            var todos = await ActQuery();

            // Assert
            todos.Should().NotBeNull();
            todos!.Should().Contain(
                new TodoDTO(todo.Id, command.Name, false)
            );
        }

        [Fact]
        public async Task GivenEmptyNameShouldThrowEmptyTodoNameException()
        {
            // Arrange
            var todo = await AddTestTodo(
                Guid.Parse("6A86A119-C992-43D6-B5DE-196C9CCA461C")
            );
            var command = new ChangeTodoName(
                todo.Id,
                string.Empty
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

        [Fact]
        public async Task GivenNotExistsTodoShouldThrowTodoNotFoundException()
        {
            // Arrange
            var command = new ChangeTodoName(
                Guid.Parse("2002378A-525F-4A70-99C7-D61CB5EBEF7E"),
                "Test"
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
        private readonly ICommandHandler<ChangeTodoName> _commandHandler;
        private readonly IQueryHandler<GetTodos, IEnumerable<TodoDTO>> _queryHandler;

        public ChangeTodoNameHandlerTests(
            TestApplicationFactory<Program> factory
        )
        {
            _scope = factory.Services.CreateScope();
            _todoRepository = _scope.ServiceProvider.GetService<ITodoRepository>();
            _commandHandler = _scope.ServiceProvider.GetService<ICommandHandler<ChangeTodoName>>();
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
