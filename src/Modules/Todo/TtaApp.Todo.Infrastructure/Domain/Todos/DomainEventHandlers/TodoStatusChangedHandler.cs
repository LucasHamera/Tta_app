using System;
using System.Threading.Tasks;
using Convey.Persistence.MongoDB;
using TtaApp.Todo.Domain.Todos.Events;
using TtaApp.Todo.Infrastructure.Database.Documents;
using TtaApp.Todo.Infrastructure.Services;

namespace TtaApp.Todo.Infrastructure.Domain.Todos.DomainEventHandlers
{
    internal class TodoStatusChangedHandler : IDomainEventHandler<TodoStatusChanged>
    {

        private readonly IMongoRepository<TodoDocument, Guid> _todoRepository;

        public TodoStatusChangedHandler(
            IMongoRepository<TodoDocument, Guid> todoRepository
        )
        {
            _todoRepository = todoRepository;
        }
        public async Task HandleAsync(
            TodoStatusChanged @event
        )
        {
            await _todoRepository
                .UpdateAsync(@event.Todo.AsDocument());
        }
    }
}
