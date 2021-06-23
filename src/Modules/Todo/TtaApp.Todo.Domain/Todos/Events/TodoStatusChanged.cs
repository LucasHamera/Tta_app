using TtaApp.Shared.Domain.Base;

namespace TtaApp.Todo.Domain.Todos.Events
{
    public class TodoStatusChanged: IDomainEvent
    {
        public TodoStatusChanged(
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
