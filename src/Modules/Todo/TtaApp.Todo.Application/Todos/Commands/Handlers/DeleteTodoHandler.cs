﻿using System.Threading.Tasks;
using Convey.CQRS.Commands;

namespace TtaApp.Todo.Application.Todos.Commands.Handlers
{
    internal class DeleteTodoHandler: ICommandHandler<DeleteTodo>
    {
        public Task HandleAsync(
            DeleteTodo command
        )
        {
            throw new System.NotImplementedException();
        }
    }
}
