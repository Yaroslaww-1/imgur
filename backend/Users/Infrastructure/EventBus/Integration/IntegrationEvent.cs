using System;

namespace MediaLakeUsers.Infrastructure.EventBus.Integration
{
    public abstract class IntegrationEvent
    {
        public Guid Id { get; }
        public DateTime OccurredOn { get; }
        public string AggregateEntityName { get; }

        protected IntegrationEvent(Guid id, DateTime occurredOn, string aggregateEntityName)
        {
            Id = id;
            OccurredOn = occurredOn;
            AggregateEntityName = aggregateEntityName;
        }
    }
}
