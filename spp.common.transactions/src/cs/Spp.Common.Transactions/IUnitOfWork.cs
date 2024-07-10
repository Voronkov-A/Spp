using System.Threading;
using System.Threading.Tasks;

namespace Spp.Common.Transactions;

public interface IUnitOfWork
{
    Task Commit(CancellationToken cancellationToken);
}
