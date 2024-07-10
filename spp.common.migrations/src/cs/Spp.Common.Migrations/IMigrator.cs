using System.Threading;
using System.Threading.Tasks;

namespace Spp.Common.Migrations;

public interface IMigrator
{
    Task Up(CancellationToken cancellationToken);
}
