using System.Threading;
using System.Threading.Tasks;

namespace Spp.Common.Initialization;

public interface IInitializer
{
    Task Initialize(CancellationToken cancellationToken);
}
