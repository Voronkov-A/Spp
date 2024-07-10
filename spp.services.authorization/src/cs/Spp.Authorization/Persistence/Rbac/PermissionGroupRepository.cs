using Spp.Authorization.Domain.Rbac;
using Spp.Authorization.Domain.Rbac.Repositories;
using Spp.Common.Domain;
using Spp.Common.EventSourcing.EventStore;
using Spp.Common.Transactions;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Authorization.Persistence.Rbac;

public class PermissionGroupRepository(IEventStoreRepository repository, IChangeTracker changeTracker) :
    IPermissionGroupRepository
{
    public Task Add(PermissionGroup item, CancellationToken cancellationToken)
    {
        changeTracker.Track(item);
        return Task.CompletedTask;
    }

    public async Task<PermissionGroup?> Find(EntityId id, CancellationToken cancellationToken)
    {
        var item = changeTracker.FindTrackedAggregate<PermissionGroup>(id);

        if (item != null)
        {
            return item;
        }

        item = await repository.Find(id, AggregateFactory<PermissionGroup>.Instance, cancellationToken);

        if (item != null)
        {
            changeTracker.Track(item);
        }

        return item;
    }
}
