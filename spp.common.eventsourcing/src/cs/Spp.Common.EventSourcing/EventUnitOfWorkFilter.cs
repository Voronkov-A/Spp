using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Spp.Common.Domain;
using Spp.Common.Transactions;

namespace Spp.Common.EventSourcing;

public class EventUnitOfWorkFilter(IEventRepository repository) : IUnitOfWorkFilter
{
    public async Task Commit(IEnumerable<IAggregate> aggregates, CancellationToken cancellationToken)
    {
        foreach (var aggregate in aggregates)
        {
            await repository.Save(aggregate, cancellationToken);
        }
    }
}
