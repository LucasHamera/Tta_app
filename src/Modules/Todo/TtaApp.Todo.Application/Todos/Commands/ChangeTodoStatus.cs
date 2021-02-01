using System;
using Convey.CQRS.Commands;

namespace TtaApp.Todo.Application.Todos.Commands
{
    public class ChangeTodoStatus : ICommand
    {
        public ChangeTodoStatus(
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
