using System;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Common.Synchronization;

public interface IDistributedLock : IAsyncDisposable, IDisposable
{
    Task Acquire(CancellationToken cancellationToken);

    Task Release(CancellationToken cancellationToken);
}
