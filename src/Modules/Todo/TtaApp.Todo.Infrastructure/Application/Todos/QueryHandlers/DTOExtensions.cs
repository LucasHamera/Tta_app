using System.Collections.Generic;
using System.Linq;
using TtaApp.Todo.Application.Todos.Queries.DTOs;
using TtaApp.Todo.Infrastructure.Database.Documents;

namespace TtaApp.Todo.Infrastructure.Application.Todos.QueryHandlers
{
    internal static class DTOExtensions
    {
        public static TodoDTO AsDTO(
            this TodoDocument todoDocument
        ) => new(
            todoDocument.Id,
            todoDocument.Name,
            todoDocument.Done
        );

        public static IEnumerable<TodoDTO> AsDTOs(
            this IEnumerable<TodoDocument> todoDocuments
        ) => todoDocuments.Select(AsDTO);
    }
}
