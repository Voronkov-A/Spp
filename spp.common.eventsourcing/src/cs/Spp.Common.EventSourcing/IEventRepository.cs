using System.Threading;
using System.Threading.Tasks;
using Spp.Common.Domain;

namespace Spp.Common.EventSourcing;

public interface IEventRepository
{
    Task Save(IAggregate aggregate, CancellationToken cancellationToken);
}
