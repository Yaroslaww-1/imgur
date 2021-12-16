using MediaLakeCore.BuildingBlocks.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MediaLakeCore.Infrastructure.EventBus.Domain
{
    public interface IDomainEventBus
    {
        Task PublishAsync(IDomainEvent domainEvent);
        Task PublishAllAsync(IReadOnlyCollection<IDomainEvent> domainEvents);
    }
}
