using System.Threading;
using System.Threading.Tasks;

namespace Spp.Common.Mediator;

public interface IRequestHandlerDecorator<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    Task<TResponse> Handle(
        IRequestHandler<TRequest, TResponse> inner,
        TRequest request,
        CancellationToken cancellationToken);
}
