using TtaApp.Todo.Domain.Todos.ValueObjects;

namespace TtaApp.Todo.Infrastructure.Database.Documents
{
    internal static class DocumentExtensions
    {
        public static TodoDocument AsDocument(
            this Todo.Domain.Todos.Todo todo
        ) => new(
            todo.Id,
            todo.Name.Value,
            todo.Done,
            todo.Version
        );

        public static Todo.Domain.Todos.Todo AsEntity(
            this TodoDocument todoDocument
        ) => new(
            todoDocument.Id,
            new TodoName(todoDocument.Name),
            todoDocument.Done,
            todoDocument.Version
        );
    }
}
