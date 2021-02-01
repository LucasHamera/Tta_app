using System.Threading.Tasks;
using Convey.CQRS.Commands;

namespace TtaApp.Todo.Application.Todos.Commands.Handlers
{
    internal class ChangeTodoStatusHandler: ICommandHandler<ChangeTodoStatus>
    {
        public Task HandleAsync(
            ChangeTodoStatus command
        )
        {
            throw new System.NotImplementedException();
        }
    }
}
