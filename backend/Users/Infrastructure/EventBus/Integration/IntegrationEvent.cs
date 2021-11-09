using System;

namespace MediaLakeUsers.Infrastructure.EventBus.Integration
{
    public abstract class IntegrationEvent
    {
        public Guid Id { get; }

        public DateTime OccurredOn { get; }

        protected IntegrationEvent(Guid id, DateTime occurredOn)
        {
            Id = id;
            OccurredOn = occurredOn;
        }
    }
}
