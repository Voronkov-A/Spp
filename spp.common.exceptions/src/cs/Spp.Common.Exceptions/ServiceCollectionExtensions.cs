using Microsoft.Extensions.DependencyInjection;

namespace Spp.Common.Exceptions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddExceptions(this IServiceCollection services)
    {
        return services.AddSingleton<IExceptionResolver, ExceptionResolver>();
    }
}
