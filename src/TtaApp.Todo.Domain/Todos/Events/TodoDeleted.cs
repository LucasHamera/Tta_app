using System;
using TtaApp.Shared.Domain.Base;

namespace TtaApp.Todo.Domain.Todos.Events
{
    public class TodoDeleted: IDomainEvent
    {
        public TodoDeleted(
            Todo todo
        )
        {
            Todo = todo;
        }

        public Todo Todo
        {
            get;
        }
    }
}
