using System.Collections.Generic;
using Convey.CQRS.Queries;
using TtaApp.Todo.Application.Todos.Queries.DTOs;

namespace TtaApp.Todo.Application.Todos.Queries
{
    public class GetTodos: IQuery<IEnumerable<TodoDTO>>
    {
    }
}
