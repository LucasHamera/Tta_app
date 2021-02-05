using System;
using TtaApp.Shared.Domain.Base;
using TtaApp.Todo.Domain.Todos.Events;
using TtaApp.Todo.Domain.Todos.ValueObjects;

namespace TtaApp.Todo.Domain.Todos
{
    public class Todo: AggregateRoot
    {
        private Todo(
            Guid id,
            TodoName name
        ) : this(id, name, false, 0)
        {
        }

        public Todo(
            AggregateId id,
            TodoName name,
            bool done, 
            int version
        )
        {
            Id = id;
            Name = name;
            Done = done;
            Version = version;
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

        public bool Done
        {
            get;
            private set;
        }

        public TodoName Name
        {
            get;
            private set;
        }

        public void ChangeStatus(
            bool done
        )
        {
            Done = done;
            AddEvent(
                new TodoStatusChanged(this)
            );
        }

        public void ChangeName(
            TodoName newName
        )
        {
            Name = newName;
            AddEvent(
                new TodoNameChanged(this)
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
