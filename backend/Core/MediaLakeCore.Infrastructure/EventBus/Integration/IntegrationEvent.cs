using MediatR;
using System;

namespace MediaLakeCore.Infrastructure.EventBus.Integration
{
    public abstract class IntegrationEvent : INotification
    {
        public Guid Id { get; }
        public DateTime OccurredOn { get; }
        public virtual string AggregateEntityName { get; }


        protected IntegrationEvent(Guid id, DateTime occurredOn, string aggregateEntityName)
        {
            Id = id;
            OccurredOn = occurredOn;
            AggregateEntityName = aggregateEntityName;
        }

        protected IntegrationEvent()
        {
            Id = Guid.NewGuid();
            OccurredOn = DateTime.Now;
        }
    }
}
