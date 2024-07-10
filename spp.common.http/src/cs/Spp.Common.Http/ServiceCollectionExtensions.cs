using Microsoft.Extensions.DependencyInjection;
using Spp.Common.Exceptions;

namespace Spp.Common.Http;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHttp(this IServiceCollection services)
    {
        return services.AddSingleton<IExceptionDescriptor, ExceptionDescriptor>();
    }
}
