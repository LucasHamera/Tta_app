using System;
using System.Threading.Tasks;
using TtaApp.Shared.Domain.Common;

namespace TtaApp.Todo.Domain.Todos.Services
{
    public interface ITodoRepository
    {
        Task<Optional<Todo>> GetByIdAsync(
            Guid id
        );

        Task AddAsync(
            Todo todo
        );
    }
}
