using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TtaApp.Shared.Domain.Base;
using TtaApp.Todo.Application.Services;
using TtaApp.Todo.Infrastructure.Services;

namespace TtaApp.Todo.Infrastructure.Application.Services
{
    internal class TransactionCommitter: ITransactionCommitter
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ITransactionCommitter> _logger;

        public TransactionCommitter(
            IServiceProvider serviceProvider,
            ILogger<ITransactionCommitter> logger
        )
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async Task CommitWithEventAsync(
            IDomainEvent @event
        )
        {
            if (@event is null)
                return;

            _logger
                .LogInformation($"Started processing event {@event.GetType().Name}");

            await HandleDomainEventHandler(
                @event
            );
            

            // TODO Add external events mapper
        }

        public async Task CommitWithEventsAsync(
            IEnumerable<IDomainEvent> events
        )
        {
            if (events is null)
                return;
            
            foreach (var @event in events)
            {
                _logger
                    .LogInformation($"Started processing event {@event.GetType().Name}");

                await HandleDomainEventHandler(
                    @event
                );
            }

            // TODO Add external events mapper
        }

        private async Task HandleDomainEventHandler(
            IDomainEvent @event
        )
        {
            var eventType = @event.GetType();
            var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(eventType);
            IEnumerable<object> handlers = _serviceProvider
                .GetServices(handlerType);
            foreach (var handler in handlers)
            {
                await (Task)handler
                    .GetType()
                    .GetMethod("HandleAsync")
                    ?.Invoke(handler, new[] { @event });
            }
        }
    }
}
