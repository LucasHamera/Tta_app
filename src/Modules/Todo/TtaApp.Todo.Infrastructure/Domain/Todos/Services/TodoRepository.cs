using System;
using System.Threading.Tasks;
using Convey.Persistence.MongoDB;
using TtaApp.Shared.Domain.Common;
using TtaApp.Todo.Domain.Todos.Services;
using TtaApp.Todo.Infrastructure.Database.Documents;

namespace TtaApp.Todo.Infrastructure.Domain.Todos.Services
{
    internal class TodoRepository : ITodoRepository
    {
        private readonly IMongoRepository<TodoDocument, Guid> _todoRepository;

        public TodoRepository(
            IMongoRepository<TodoDocument, Guid> todoRepository
        )
        {
            _todoRepository = todoRepository;
        }

        public async Task<Optional<Todo.Domain.Todos.Todo>> GetByIdAsync(
            Guid id
        )
        {
            var todoDocument = await _todoRepository
                .GetAsync(todo => todo.Id.Equals(id));

            return todoDocument?.AsEntity();
        }

        public Task AddAsync(
            Todo.Domain.Todos.Todo todo
        )
        {
            return _todoRepository
                .AddAsync(todo.AsDocument());
        }
    }
}
