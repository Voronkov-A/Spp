using Spp.Common.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Common.Transactions;

public interface IUnitOfWorkFilter
{
    Task Commit(IEnumerable<IAggregate> aggregates, CancellationToken cancellationToken);
}
