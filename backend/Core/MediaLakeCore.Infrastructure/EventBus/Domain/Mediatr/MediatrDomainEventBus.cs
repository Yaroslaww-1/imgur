using MediaLakeCore.BuildingBlocks.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaLakeCore.Infrastructure.EventBus.Domain.MediatR
{
    public class MediatrDomainEventBus : IDomainEventBus
    {
        private readonly IMediator _mediator;

        public MediatrDomainEventBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishAsync(IDomainEvent domainEvent)
        {
            await _mediator.Publish(domainEvent);
        }

        public async Task PublishAllAsync(IReadOnlyCollection<IDomainEvent> domainEvents)
        {
            foreach (IDomainEvent domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent);
            }
        }
    }
}
