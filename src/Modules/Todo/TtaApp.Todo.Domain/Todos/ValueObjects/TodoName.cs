using TtaApp.Todo.Domain.Todos.Exceptions;

namespace TtaApp.Todo.Domain.Todos.ValueObjects
{
    public class TodoName
    {
        public TodoName(
            string name
        )
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new EmptyTodoNameException();

            Value = name;
        }

        public string Value
        {
            get;
        }
    }
}
