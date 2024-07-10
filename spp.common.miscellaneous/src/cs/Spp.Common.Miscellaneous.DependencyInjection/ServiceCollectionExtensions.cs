using Microsoft.Extensions.DependencyInjection;
using Spp.Common.Miscellaneous.DependencyInjection.Decoration;
using System;

namespace Spp.Common.Miscellaneous.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDecorator<TService, TDecorator>(this IServiceCollection services)
        where TService : class
        where TDecorator : class
    {
        ArgumentNullException.ThrowIfNull(services);

        return AddDecorator(services, typeof(TService), typeof(TDecorator));
    }

    public static IServiceCollection AddDecorator<TService>(
        this IServiceCollection services,
        Func<IServiceProvider, TService, TService> decoratorFactory)
        where TService : class
    {
        ArgumentNullException.ThrowIfNull(services);

        return AddDecorator(services, typeof(TService), (sp, inner) => decoratorFactory(sp, (TService)inner));
    }

    public static IServiceCollection AddDecorator(
        this IServiceCollection services,
        Type serviceType,
        Type decoratorType)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(serviceType);
        ArgumentNullException.ThrowIfNull(decoratorType);

        return AddDecorator(services, DecorationStrategy.WithType(serviceType, decoratorType));
    }

    public static IServiceCollection AddDecorator(
        this IServiceCollection services,
        Type serviceType,
        Func<IServiceProvider, object, object> decoratorFactory)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(serviceType);
        ArgumentNullException.ThrowIfNull(decoratorFactory);

        return AddDecorator(
            services,
            DecorationStrategy.WithFactory(serviceType, (inner, sp) => decoratorFactory(sp, inner)));
    }

    private static IServiceCollection AddDecorator(IServiceCollection services, DecorationStrategy strategy)
    {
        var count = services.Count;

        for (var i = 0; i < count; ++i)
        {
            var serviceDescriptor = services[i];

            if (strategy.CanDecorate(serviceDescriptor.ServiceType))
            {
                var decoratedType = new DecoratedType(serviceDescriptor.ServiceType);
                services[i] = serviceDescriptor.WithServiceType(decoratedType);
                services.Add(serviceDescriptor.WithImplementationFactory(strategy.CreateDecorator(decoratedType)));
                return services;
            }
        }

        throw new InvalidOperationException(
                $"Could not add decorator - service {strategy.ServiceType} is not registered.");
    }
}
