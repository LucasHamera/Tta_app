using System.Threading.Tasks;
using Convey.CQRS.Commands;
using TtaApp.Todo.Application.Services;
using TtaApp.Todo.Application.Todos.Exceptions;
using TtaApp.Todo.Domain.Todos.Services;

namespace TtaApp.Todo.Application.Todos.Commands.Handlers
{
    internal class ChangeTodoStatusHandler: ICommandHandler<ChangeTodoStatus>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly ITransactionCommitter _transactionCommitter;

        public ChangeTodoStatusHandler(
            ITodoRepository todoRepository,
            ITransactionCommitter transactionCommitter
        )
        {
            _todoRepository = todoRepository;
            _transactionCommitter = transactionCommitter;
        }

        public async Task HandleAsync(
            ChangeTodoStatus command
        )
        {
            var todo = await _todoRepository
                .GetByIdAsync(command.Id);

            if (!todo.HasValue)
                throw new TodoNotFoundException(command.Id);

            todo
                .Value
                .ChangeStatus(command.Done);

            await _transactionCommitter
                .CommitWithEventsAsync(todo.Value.Events);
        }
    }
}
