using System;
using System.Threading.Tasks;
using Convey.Persistence.MongoDB;
using TtaApp.Todo.Domain.Todos.Events;
using TtaApp.Todo.Infrastructure.Database.Documents;
using TtaApp.Todo.Infrastructure.Services;

namespace TtaApp.Todo.Infrastructure.Domain.Todos.DomainEventHandlers
{
    internal class TodoDeletedHandler: IDomainEventHandler<TodoDeleted>
    {
        private readonly IMongoRepository<TodoDocument, Guid> _todoRepository;

        public TodoDeletedHandler(
            IMongoRepository<TodoDocument, Guid> todoRepository
        )
        {
            _todoRepository = todoRepository;
        }

        public async Task HandleAsync(
            TodoDeleted @event
        )
        {
            await _todoRepository
                .DeleteAsync(
                    @event.Todo.Id
                );
        }
    }
}
