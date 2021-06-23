using System;
using TtaApp.Shared.Application.Base;

namespace TtaApp.Todo.Application.Todos.Exceptions
{
    public class TodoNotFoundException: AppException
    {
        public TodoNotFoundException(
            Guid todoId
        ) : base($"Todo with {todoId} not found.")
        {
            TodoId = todoId;
        }

        public override string Code
            => "todo_not_found";

        public Guid TodoId
        {
            get;
        }
    }
}
