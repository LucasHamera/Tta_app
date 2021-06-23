using System.Threading.Tasks;
using Convey.CQRS.Commands;
using TtaApp.Todo.Application.Services;
using TtaApp.Todo.Application.Todos.Exceptions;
using TtaApp.Todo.Domain.Todos.Services;

namespace TtaApp.Todo.Application.Todos.Commands.Handlers
{
    internal class DeleteTodoHandler: ICommandHandler<DeleteTodo>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly ITransactionCommitter _transactionCommitter;

        public DeleteTodoHandler(
            ITodoRepository todoRepository,
            ITransactionCommitter transactionCommitter
        )
        {
            _todoRepository = todoRepository;
            _transactionCommitter = transactionCommitter;
        }

        public async Task HandleAsync(
            DeleteTodo command
        )
        {
            var todo = await _todoRepository
                .GetByIdAsync(command.Id);

            if (!todo.HasValue)
                throw new TodoNotFoundException(command.Id);

            todo
                .Value
                .Delete();

            await _transactionCommitter
                .CommitWithEventsAsync(todo.Value.Events);
        }
    }
}
