using System.Threading;
using System.Threading.Tasks;

namespace Spp.Common.Synchronization;

public interface IDistributedLockFactory
{
    Task<IDistributedLock> Create(string id, CancellationToken cancellationToken);
}
