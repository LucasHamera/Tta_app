namespace TtaApp.Todo.Domain.Todos.ValueObjects
{
    public class TodoName
    {
        public TodoName(
            string name
        )
        {
            Value = name;
        }

        public string Value
        {
            get;
        }
    }
}
