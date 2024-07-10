using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Common.Mediator;

internal class Mediator(IServiceProvider services, RequestHandlerRegistry handlerRegistry) : IMediator
{
    public async Task<TResponse> Dispatch<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken)
        where TRequest : IRequest<TResponse>
    {
        var (handlerType, filterType) = handlerRegistry.GetTypes(typeof(TRequest), typeof(TResponse));
        var handler = (IRequestHandler<TRequest, TResponse>)services.GetRequiredService(handlerType);
        var filters = (IEnumerable<IRequestHandlerDecorator<TRequest, TResponse>>)services.GetServices(filterType);
        var combinedHandler = filters
            .Aggregate(handler, (inner, filter) => new DecoratingRequestHandler<TRequest, TResponse>(inner, filter));
        return await combinedHandler.Handle(request, cancellationToken);
    }

    private class DecoratingRequestHandler<TRequest, TResponse>(
        IRequestHandler<TRequest, TResponse> inner,
        IRequestHandlerDecorator<TRequest, TResponse> decorator) :
        IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            return await decorator.Handle(inner, request, cancellationToken);
        }
    }
}
