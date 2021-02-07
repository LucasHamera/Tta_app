using System;
using TtaApp.Shared.Domain.Base;
using TtaApp.Todo.Domain.Todos.ValueObjects;

namespace TtaApp.Todo.Domain.Todos.Events
{
    public class TodoCreated: IDomainEvent
    {
        public TodoCreated(
            AggregateId id, 
            TodoName name
        )
        {
            Id = id;
            Name = name;
        }

        public AggregateId Id
        {
            get;
        }

        public TodoName Name
        {
            get;
        }
    }
}
