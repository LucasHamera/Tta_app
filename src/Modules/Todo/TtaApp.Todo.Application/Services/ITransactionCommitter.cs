using System.Collections.Generic;
using System.Threading.Tasks;
using TtaApp.Shared.Domain.Base;

namespace TtaApp.Todo.Application.Services
{
    public interface ITransactionCommitter
    {
        Task CommitWithEventAsync(
            IDomainEvent @event
        );

        Task CommitWithEventsAsync(
            IEnumerable<IDomainEvent> events
        );
    }
}
