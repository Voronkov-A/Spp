using System;
using Spp.Authorization.Domain.Users;
using Spp.Authorization.Domain.Users.Repositories;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Spp.Common.Transactions;
using Spp.Common.Domain;
using Spp.Common.EventSourcing.EventStore;

namespace Spp.Authorization.Persistence.Users;

public class UserRepository(IEventStoreRepository repository, IChangeTracker changeTracker, IUserIndex index) :
    IUserRepository
{
    public Task Add(User item, CancellationToken cancellationToken)
    {
        changeTracker.Track(item);
        return Task.CompletedTask;
    }

    public async Task<bool> Exists(UserName name, CancellationToken cancellationToken)
    {
        return changeTracker.TrackedAggregates.OfType<User>().Any(x => x.Name == name)
            || await index.Exists(name, cancellationToken);
    }

    public async Task<User?> Find(EntityId id, CancellationToken cancellationToken)
    {
        var item = changeTracker.FindTrackedAggregate<User>(id);

        if (item != null)
        {
            return item;
        }

        item = await repository.Find(id, AggregateFactory<User>.Instance, cancellationToken);

        if (item != null)
        {
            changeTracker.Track(item);
        }

        return item;
    }

    public async Task<User?> Find(UserIdentity identity, CancellationToken cancellationToken)
    {
        var item = changeTracker.TrackedAggregates.OfType<User>().FirstOrDefault(x => x.HasIdentity(identity));

        if (item != null)
        {
            return item;
        }

        var id = await index.Find(identity, cancellationToken);

        if (id == null)
        {
            return null;
        }

        item = await this.Get(id.Value, cancellationToken);
        changeTracker.Track(item);
        return item;
    }

    public async Task<User?> FindLast(GeneratedUserNameStem userNameStem, CancellationToken cancellationToken)
    {
        var inMemoryLast = changeTracker.TrackedAggregates
            .OfType<User>()
            .Where(x => x.Name.ToString().StartsWith(userNameStem.ToString()))
            .OrderBy(x => x.Name.ToString().Length)
            .ThenBy(x => x.Name.ToString())
            .FirstOrDefault();

        var id = await index.FindLast(userNameStem, cancellationToken);

        if (id == null || id == inMemoryLast?.Id)
        {
            return inMemoryLast;
        }

        var item = changeTracker.FindTrackedAggregate<User>(id.Value)
            ?? await this.Get(id.Value, cancellationToken);

        if (inMemoryLast != null
            && (inMemoryLast.Name.ToString().Length > item.Name.ToString().Length
                || inMemoryLast.Name.ToString().Length == item.Name.ToString().Length
                && string.Compare(inMemoryLast.Name.ToString(), item.Name.ToString(), StringComparison.Ordinal) > 0))
        {
            return inMemoryLast;
        }

        changeTracker.Track(item);
        return item;
    }
}
