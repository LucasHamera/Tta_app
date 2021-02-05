using System.Threading.Tasks;
using Convey.CQRS.Commands;
using TtaApp.Todo.Application.Services;
using TtaApp.Todo.Application.Todos.Exceptions;
using TtaApp.Todo.Domain.Todos.Services;
using TtaApp.Todo.Domain.Todos.ValueObjects;

namespace TtaApp.Todo.Application.Todos.Commands.Handlers
{
    internal class ChangeTodoNameHandler : ICommandHandler<ChangeTodoName>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly ITransactionCommitter _transactionCommitter;

        public ChangeTodoNameHandler(
            ITodoRepository todoRepository, 
            ITransactionCommitter transactionCommitter
        )
        {
            _todoRepository = todoRepository;
            _transactionCommitter = transactionCommitter;
        }

        public async Task HandleAsync(
            ChangeTodoName command
        )
        {
            var todo = await _todoRepository
                .GetByIdAsync(command.Id);

            if (!todo.HasValue)
                throw new TodoNotFoundException(command.Id);

            var newTodoName = new TodoName(command.Name);
            todo
                .Value
                .ChangeName(newTodoName);

            await _transactionCommitter
                .CommitWithEventsAsync(todo.Value.Events);
        }
    }
}
