using System;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using TtaApp.Todo.Application.Services;
using TtaApp.Todo.Application.Todos.Commands;
using TtaApp.Todo.Application.Todos.Commands.Handlers;
using TtaApp.Todo.Application.Todos.Exceptions;
using TtaApp.Todo.Domain.Todos.Services;
using TtaApp.Todo.Domain.Todos.ValueObjects;
using Xunit;

namespace TtaApp.Todo.Tests.Unit.Application.Todos.Commands
{
    public class ChangeTodoStatusHandlerTests
    {
        private Task Act(ChangeTodoStatus command)
            => _handler.HandleAsync(command);

        [Fact]
        public async Task GivenValidDataShouldAdded()
        {
            // Arrange
            var command = new ChangeTodoStatus(
                Guid.NewGuid(),
                true
            );
            var todo = new Todo.Domain.Todos.Todo(
                command.Id,
                new TodoName("Name"),
                false,
                0
            );
            _repository
                .GetByIdAsync(command.Id)
                .Returns(todo);

            // Act
            await Act(command);

            // Arrange
            todo.Done.Should().Be(command.Done);
            await _transactionCommitter
                .Received()
                .CommitWithEventsAsync(todo.Events);
        }

        [Fact]
        public async Task GivenNotExistsTodoIdShouldThrowTodoNotFoundException()
        {
            // Arrange
            var command = new ChangeTodoStatus(
                Guid.NewGuid(),
                true
            );
            Todo.Domain.Todos.Todo todo = null!;
            _repository
                .GetByIdAsync(command.Id)
                .Returns(todo);

            // Act
            var exception = await Record.ExceptionAsync(
                async () => await Act(command)
            );

            // Arrange
            exception.Should().NotBeNull();
            var todoNotFound = exception as TodoNotFoundException;
            todoNotFound.Should().NotBeNull();
            todoNotFound!.Code.Should().Be("todo_not_found");
            todoNotFound!.Message.Should().Be($"Todo with {command.Id} not found.");
        }

        private readonly ChangeTodoStatusHandler _handler;
        private readonly ITodoRepository _repository;
        private readonly ITransactionCommitter _transactionCommitter;

        public ChangeTodoStatusHandlerTests()
        {
            _repository = Substitute.For<ITodoRepository>();
            _transactionCommitter = Substitute.For<ITransactionCommitter>();
            _handler = new ChangeTodoStatusHandler(
                _repository,
                _transactionCommitter
            );
        }
    }
}
