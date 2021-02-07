using TtaApp.Shared.Domain.Base;

namespace TtaApp.Todo.Domain.Todos.Exceptions
{
    public class EmptyTodoNameException: DomainException
    {
        public EmptyTodoNameException() 
            : base("Empty todo name.")
        {
        }

        public override string Code
            => "empty_todo_name";
    }
}
