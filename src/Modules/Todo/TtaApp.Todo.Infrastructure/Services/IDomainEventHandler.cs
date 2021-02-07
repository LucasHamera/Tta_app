using System.Threading.Tasks;
using TtaApp.Shared.Domain.Base;

namespace TtaApp.Todo.Infrastructure.Services
{
    internal interface IDomainEventHandler<in T>
        where T : class, IDomainEvent
    {
        Task HandleAsync(
            T @event
        );
    }
}
