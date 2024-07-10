using Spp.Authorization.Domain.Rbac;
using Spp.Authorization.Domain.Rbac.Repositories;
using Spp.Common.Domain;
using Spp.Common.EventSourcing.EventStore;
using Spp.Common.Transactions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Authorization.Persistence.Rbac;

public class RoleRepository(IEventStoreRepository repository, IChangeTracker changeTracker, IRoleIndex index) :
    IRoleRepository
{
    public Task Add(Role item, CancellationToken cancellationToken)
    {
        changeTracker.Track(item);
        return Task.CompletedTask;
    }

    public async Task<Role?> Find(EntityId id, CancellationToken cancellationToken)
    {
        var item = changeTracker.FindTrackedAggregate<Role>(id);

        if (item != null)
        {
            return IfNotDeleted(item);
        }

        item = await repository.Find(id, AggregateFactory<Role>.Instance, cancellationToken);

        if (item == null || item.IsDeleted)
        {
            return null;
        }

        changeTracker.Track(item);
        return item;
    }

    public async Task<List<Role>> GetAll(bool isDefault, CancellationToken cancellationToken)
    {
        var ids = (await index.GetAll(isDefault, cancellationToken))
            .Concat(
                changeTracker.TrackedAggregates
                    .OfType<Role>()
                    .Where(x => x.IsDefault == isDefault && !x.IsDeleted)
                    .Select(x => x.Id))
            .ToHashSet();
        var items = new List<Role>(ids.Count);
        var untrackedIds = new List<EntityId>(ids.Count);

        foreach (var id in ids)
        {
            var item = changeTracker.FindTrackedAggregate<Role>(id);

            if (item == null)
            {
                untrackedIds.Add(id);
            }
            else if (!item.IsDeleted)
            {
                items.Add(item);
            }
        }

        if (untrackedIds.Count == 0)
        {
            return items;
        }

        var untrackedItems = await repository.GetAll(ids, AggregateFactory<Role>.Instance, cancellationToken);

        foreach (var item in untrackedItems.Where(x => !x.IsDeleted))
        {
            changeTracker.Track(item);
            items.Add(item);
        }

        return items;
    }

    private static Role? IfNotDeleted(Role? item)
    {
        return item == null || item.IsDeleted ? null : item;
    }
}
