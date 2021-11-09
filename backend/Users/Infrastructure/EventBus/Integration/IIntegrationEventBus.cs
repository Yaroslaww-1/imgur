using System;
using System.Threading.Tasks;

namespace MediaLakeUsers.Infrastructure.EventBus.Integration
{
    public interface IIntegrationEventBus
    {
        Task Publish<T>(T @event)
            where T : IntegrationEvent;
    }
}
