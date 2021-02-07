using System.Collections.Generic;
using System.Linq;

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

        protected void AddEvent(
            IDomainEvent @event
        )
        {
            if (!_events.Any())
                Version++;

            _events.Add(@event);
        }
    }
}