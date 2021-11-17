using System.Threading.Tasks;

namespace MediaLakeCore.Infrastructure.EventBus.Integration
{
    public interface IIntegrationEventBus
    {
        void Subscribe<T>()
            where T : IntegrationEvent, new();
    }
}
