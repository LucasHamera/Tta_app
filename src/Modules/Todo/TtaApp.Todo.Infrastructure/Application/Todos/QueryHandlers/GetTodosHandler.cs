using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Convey.CQRS.Queries;
using Convey.Persistence.MongoDB;
using MongoDB.Driver;
using TtaApp.Todo.Application.Todos.Queries;
using TtaApp.Todo.Application.Todos.Queries.DTOs;
using TtaApp.Todo.Infrastructure.Database.Documents;

namespace TtaApp.Todo.Infrastructure.Application.Todos.QueryHandlers
{
    internal class GetTodosHandler : IQueryHandler<GetTodos, IEnumerable<TodoDTO>>
    {
        private readonly IMongoRepository<TodoDocument, Guid> _todoRepository;

        public GetTodosHandler(
            IMongoRepository<TodoDocument, Guid> todoRepository
        )
        {
            _todoRepository = todoRepository;
        }


        public async Task<IEnumerable<TodoDTO>> HandleAsync(
            GetTodos query
        )
        {
            var todoDocuments = await _todoRepository
                .Collection
                .Find(x => true)
                .ToListAsync();

            return todoDocuments
                .AsDTOs();
        }
    }
}
