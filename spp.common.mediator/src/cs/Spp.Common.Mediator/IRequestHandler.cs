using System.Threading;
using System.Threading.Tasks;

namespace Spp.Common.Mediator;

public interface IRequestHandler<in TRequest, TResponse>
{
    Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
