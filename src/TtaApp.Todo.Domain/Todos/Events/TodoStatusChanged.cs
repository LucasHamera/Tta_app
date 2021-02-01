using System;
using TtaApp.Shared.Domain.Base;

namespace TtaApp.Todo.Domain.Todos.Events
{
    public class TodoStatusChanged: IDomainEvent
    {
        public TodoStatusChanged(
            Guid id, 
            bool done
        )
        {
            Id = id;
            Done = done;
        }

        public Guid Id
        {
            get;
        }

        public bool Done
        {
            get;
        }
    }
}
