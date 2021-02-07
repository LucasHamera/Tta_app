using FluentAssertions;
using TtaApp.Todo.Domain.Todos.Exceptions;
using TtaApp.Todo.Domain.Todos.ValueObjects;
using Xunit;

namespace TtaApp.Todo.Tests.Unit.Domain.Todos
{
    public class TodoNameTests
    {
        private TodoName Act(string name)
            => new(name);

        [Fact]
        public void GivenValidDataShouldBeCreated()
        {
            // Arrange
            var name = "Name";

            // Act
            var todoName = Act(name);

            // Assert
            todoName.Should().NotBeNull();
            todoName.Value.Should().Be(name);
        }

        [Fact]
        public void GivenEmptyNameShouldThrowEmptyTaskNameException()
        {
            // Arrange
            var name = string.Empty;

            // Act
            var exception = Record.Exception(
                () => Act(name)
            );

            // Assert
            exception.Should().NotBeNull();
            var emptyTodoNameException = exception as EmptyTodoNameException;
            emptyTodoNameException.Should().NotBeNull();
            emptyTodoNameException!.Code.Should().Be("empty_todo_name");
            emptyTodoNameException!.Message.Should().Be("Empty todo name.");
        }
    }
}
