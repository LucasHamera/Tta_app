using System;
using Convey.CQRS.Commands;

namespace TtaApp.Todo.Application.Todos.Commands
{
    public class AddTodo : ICommand
    {
        public AddTodo(
            Guid id, 
            string name
        )
        {
            Id = id;
            Name = name;
        }

        public Guid Id
        {
            get;
        }

        public string Name
        {
            get;
        }
    }
}
