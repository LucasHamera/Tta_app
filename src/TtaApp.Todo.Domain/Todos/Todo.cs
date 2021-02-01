using System;
using TtaApp.Shared.Domain.Base;
using TtaApp.Todo.Domain.Todos.Events;
using TtaApp.Todo.Domain.Todos.ValueObjects;

namespace TtaApp.Todo.Domain.Todos
{
    public class Todo: AggregateRoot
    {
        private bool _done;
        private string _name;

        private Todo()
        {

        }

        private Todo(
            Guid id,
            TodoName name
        )
        {
            Id = id;
            _name = name.Value;
        }

        public static Todo Create(
            Guid id,
            TodoName name
        )
        {
            var todo = new Todo(
                id,
                name
            );
            todo.AddEvent(new TodoCreated(todo.Id, name));
            return todo;
        }

        public void ChangeStatus(
            bool done
        )
        {
            _done = done;
            AddEvent(
                new TodoStatusChanged(Id, _done)
            );
        }

        public void Delete()
        {
            AddEvent(
                new TodoDeleted(this)
            );
        }
    }
}
