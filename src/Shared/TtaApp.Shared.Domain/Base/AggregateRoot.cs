using System.Collections.Generic;

namespace TtaApp.Shared.Domain.Base
{
    public abstract class AggregateRoot
    {
        private readonly List<IDomainEvent> _events 
            = new();

        public AggregateId Id
        {
            get; 
            protected set;
        }

        public IEnumerable<IDomainEvent> Events 
            => _events;

        public int Version
        {
            get; 
            protected set;
        }

        protected void AddEvent(IDomainEvent @event)
        {
            _events.Add(@event);
        }

        public void ClearEvents() 
            => _events.Clear();
    }
}