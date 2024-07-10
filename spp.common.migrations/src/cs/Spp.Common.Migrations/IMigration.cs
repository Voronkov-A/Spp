using System.Threading;
using System.Threading.Tasks;

namespace Spp.Common.Migrations;

public interface IMigration
{
    int Index { get; }

    Task Up(CancellationToken cancellationToken);

    Task Down(CancellationToken cancellationToken);
}

public interface IMigration<T> : IMigration;
