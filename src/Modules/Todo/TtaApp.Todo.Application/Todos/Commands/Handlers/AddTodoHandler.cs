using System.Threading.Tasks;
using Convey.CQRS.Commands;
using TtaApp.Todo.Application.Services;
using TtaApp.Todo.Domain.Todos.Services;
using TtaApp.Todo.Domain.Todos.ValueObjects;

namespace TtaApp.Todo.Application.Todos.Commands.Handlers
{
    internal class AddTodoHandler : ICommandHandler<AddTodo>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly ITransactionCommitter _transactionCommitter;

        public AddTodoHandler(
            ITodoRepository todoRepository,
            ITransactionCommitter transactionCommitter
        )
        {
            _todoRepository = todoRepository;
            _transactionCommitter = transactionCommitter;
        }

        public async Task HandleAsync(
            AddTodo command
        )
        {
            var todoName = new TodoName(
                command.Name
            );
            var todo = TtaApp.Todo.Domain.Todos.Todo.Create(
                command.Id,
                todoName
            );

            await _todoRepository
                .AddAsync(todo);

            await _transactionCommitter
                .CommitWithEventsAsync(todo.Events);
        }
    }
}
