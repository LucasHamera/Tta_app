using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using TtaApp.Shared.Domain.Base;
using TtaApp.Todo.Application.Services;
using TtaApp.Todo.Application.Todos.Commands;
using TtaApp.Todo.Application.Todos.Commands.Handlers;
using TtaApp.Todo.Domain.Todos.Services;
using TtaApp.Todo.Domain.Todos.ValueObjects;
using Xunit;

namespace TtaApp.Todo.Tests.Unit.Application.Todos.Commands
{
    public class AddTodoHandlerTests
    {
        private Task Act(AddTodo command)
            => _handler.HandleAsync(command);

        [Fact]
        public async Task GivenValidDataShouldAdded()
        {
            // Arrange
            var command = new AddTodo(
                Guid.NewGuid(),
                "Test"
            );

            // Act
            await Act(command);

            // Arrange
            await _repository
                .Received()
                .AddAsync(Arg.Any<Todo.Domain.Todos.Todo>());
            await _transactionCommitter
                .Received()
                .CommitWithEventsAsync(Arg.Any<IEnumerable<IDomainEvent>>());
        }

        private readonly AddTodoHandler _handler;
        private readonly ITodoRepository _repository;
        private readonly ITransactionCommitter _transactionCommitter;

        public AddTodoHandlerTests()
        {
            _repository = Substitute.For<ITodoRepository>();
            _transactionCommitter = Substitute.For<ITransactionCommitter>();
            _handler = new AddTodoHandler(
                _repository,
                _transactionCommitter
            );
        }
    }
}
