using System.Threading;
using System.Threading.Tasks;

namespace Spp.Common.Mediator;

public interface IMediator
{
    Task<TResponse> Dispatch<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken)
        where TRequest : IRequest<TResponse>;
}
