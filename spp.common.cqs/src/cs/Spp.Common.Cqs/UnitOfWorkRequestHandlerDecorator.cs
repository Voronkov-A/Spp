using Spp.Common.Mediator;
using Spp.Common.Transactions;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Common.Cqs;

public class UnitOfWorkRequestHandlerDecorator<TRequest, TResponse>(IUnitOfWork unitOfWork) :
    IRequestHandlerDecorator<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, IRequiresUnitOfWork
{
    public async Task<TResponse> Handle(
        IRequestHandler<TRequest, TResponse> inner,
        TRequest request,
        CancellationToken cancellationToken)
    {
        var response = await inner.Handle(request, cancellationToken);
        await unitOfWork.Commit(cancellationToken);
        return response;
    }
}
