using Spp.Common.Initialization;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Common.Synchronization.Initialization;

public class CriticalSectionInitializer(
    IInitializer inner,
    IDistributedLockFactory distributedLockFactory) : IInitializer
{
    private readonly IInitializer _inner = inner;
    private readonly IDistributedLockFactory _distributedLockFactory = distributedLockFactory;

    public async Task Initialize(CancellationToken cancellationToken)
    {
        await using var criticalSectionLock = await _distributedLockFactory.Create(
            _inner.GetType().ToString(),
            cancellationToken);
        await criticalSectionLock.Acquire(cancellationToken);
        await _inner.Initialize(cancellationToken);
    }
}
