using TtaApp.Shared.Domain.Base;

namespace TtaApp.Todo.Domain.Todos.Events
{
    public class TodoNameChanged: IDomainEvent
    {
        public TodoNameChanged(
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
