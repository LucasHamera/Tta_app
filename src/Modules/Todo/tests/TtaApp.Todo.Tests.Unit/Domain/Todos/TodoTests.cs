using System.Linq;
using FluentAssertions;
using TtaApp.Shared.Domain.Base;
using TtaApp.Todo.Domain.Todos.Events;
using TtaApp.Todo.Domain.Todos.ValueObjects;
using Xunit;

namespace TtaApp.Todo.Tests.Unit.Domain.Todos
{
    public class TodoTests
    {
        [Fact]
        public void GivenValidDataShouldBeCreated()
        {
            // Arrange
            var id = new AggregateId();
            var todoName = new TodoName("name");

            // Act
            var todo = Todo.Domain.Todos.Todo.Create(id, todoName);


            // Assert
            todo.Should().NotBeNull();
            todo.Id.Should().Be(id);
            todo.Name.Should().Be(todoName);
            todo.Done.Should().Be(false);

            todo.Events.Should().NotBeNull();
            todo.Events.Count().Should().Be(1);
            var @event = todo.Events.FirstOrDefault();
            @event.Should().NotBeNull();

            var todoCreated = @event as TodoCreated;
            todoCreated.Should().NotBeNull();
            todoCreated!.Id.Should().Be(id);
            todoCreated!.Name.Should().Be(todoName);
        }

        [Fact]
        public void ChangeStatusShouldChanged()
        {
            // Arrange
            var id = new AggregateId();
            var name = new TodoName("name");
            var todo = new Todo.Domain.Todos.Todo(id, name, false, 0);
            var done = true;

            // Act
            todo.ChangeStatus(done);


            // Assert
            todo.Done.Should().Be(done);

            todo.Events.Should().NotBeNull();
            todo.Events.Count().Should().Be(1);
            var @event = todo.Events.FirstOrDefault();
            @event.Should().NotBeNull();

            var todoStatusChanged = @event as TodoStatusChanged;
            todoStatusChanged.Should().NotBeNull();
            todoStatusChanged!.Todo.Should().Be(todo);
        }

        [Fact]
        public void ChangeNameShouldChanged()
        {
            // Arrange
            var id = new AggregateId();
            var name = new TodoName("name");
            var todo = new Todo.Domain.Todos.Todo(id, name, false, 0);
            var newName = new TodoName("newName");

            // Act
            todo.ChangeName(newName);


            // Assert
            todo.Name.Should().Be(newName);

            todo.Events.Should().NotBeNull();
            todo.Events.Count().Should().Be(1);
            var @event = todo.Events.FirstOrDefault();
            @event.Should().NotBeNull();

            var todoNameChanged = @event as TodoNameChanged;
            todoNameChanged.Should().NotBeNull();
            todoNameChanged!.Todo.Should().Be(todo);
        }

        [Fact]
        public void DeleteShouldDeleted()
        {
            // Arrange
            var id = new AggregateId();
            var name = new TodoName("name");
            var todo = new Todo.Domain.Todos.Todo(id, name, false, 0);

            // Act
            todo.Delete();

            // Assert
            todo.Events.Should().NotBeNull();
            todo.Events.Count().Should().Be(1);
            var @event = todo.Events.FirstOrDefault();
            @event.Should().NotBeNull();

            var todoDeleted = @event as TodoDeleted;
            todoDeleted.Should().NotBeNull();
            todoDeleted!.Todo.Should().Be(todo);
        }
    }
}
