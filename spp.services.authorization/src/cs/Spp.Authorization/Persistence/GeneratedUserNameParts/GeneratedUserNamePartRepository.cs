using System.Linq;
using Spp.Authorization.Domain.GeneratedUserNameParts;
using Spp.Authorization.Domain.GeneratedUserNameParts.Repositories;
using System.Threading;
using System.Threading.Tasks;
using Spp.Common.Transactions;
using Spp.Common.Domain;
using Spp.Common.EventSourcing.EventStore;

namespace Spp.Authorization.Persistence.GeneratedUserNameParts;

public class GeneratedUserNamePartRepository(
    IEventStoreRepository repository,
    IChangeTracker changeTracker,
    IGeneratedUserNamePartIndex index) :
    IGeneratedUserNamePartRepository
{
    public async Task<GeneratedUserNamePart?> Find(EntityId id, CancellationToken cancellationToken)
    {
        var item = changeTracker.FindTrackedAggregate<GeneratedUserNamePart>(id);

        if (item != null)
        {
            return IfNotDeleted(item);
        }

        item = await repository.Find(id, AggregateFactory<GeneratedUserNamePart>.Instance, cancellationToken);

        if (item != null)
        {
            changeTracker.Track(item);
        }

        return IfNotDeleted(item);
    }

    public async Task<GeneratedUserNamePart?> FindRandom(
        GeneratedUserNamePartType type,
        CancellationToken cancellationToken)
    {
        var ids = await index.GetSomeRandom(
            type,
            changeTracker.TrackedAggregates.OfType<GeneratedUserNamePart>().Count() + 1,
            cancellationToken);

        if (ids.Count == 0)
        {
            return changeTracker.TrackedAggregates.OfType<GeneratedUserNamePart>().FirstOrDefault(x => !x.IsDeleted);
        }

        foreach (var id in ids)
        {
            var item = changeTracker.FindTrackedAggregate<GeneratedUserNamePart>(id)
                       ?? await this.Get(id, cancellationToken);

            if (!item.IsDeleted)
            {
                return item;
            }
        }

        return null;
    }

    private static GeneratedUserNamePart? IfNotDeleted(GeneratedUserNamePart? item)
    {
        return item == null || item.IsDeleted ? null : item;
    }
}
