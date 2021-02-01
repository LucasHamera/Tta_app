using System;

namespace TtaApp.Todo.Application.Todos.Queries.DTOs
{
    public record TodoDTO(Guid Id, string Name, bool Done);
}
