using System;
using Convey.CQRS.Commands;

namespace TtaApp.Todo.Application.Todos.Commands
{
    public class DeleteTodo: ICommand
    {
        public DeleteTodo(
            Guid id
        )
        {
            Id = id;
        }

        public Guid Id
        {
            get;
        }
    }
}
