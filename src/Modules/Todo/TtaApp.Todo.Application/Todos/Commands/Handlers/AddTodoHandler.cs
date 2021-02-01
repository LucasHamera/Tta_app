using System.Threading.Tasks;
using Convey.CQRS.Commands;

namespace TtaApp.Todo.Application.Todos.Commands.Handlers
{
    internal class AddTodoHandler : ICommandHandler<AddTodo>
    {
        public Task HandleAsync(
            AddTodo command
        )
        {
            throw new System.NotImplementedException();
        }
    }
}
