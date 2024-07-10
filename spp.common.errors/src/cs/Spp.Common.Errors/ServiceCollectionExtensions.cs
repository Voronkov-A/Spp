using Microsoft.Extensions.DependencyInjection;
using Spp.Common.Configuration;

namespace Spp.Common.Errors;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddErrors(this IServiceCollection services, string errorUriSection = "Errors")
    {
        return services
            .AddSettings<ErrorUriSettings>(errorUriSection)
            .AddSingleton<IErrorUriProvider, ErrorUriProvider>()
            .AddSingleton<IErrorFactory, ErrorFactory>()
            .AddSingleton<ICommonErrorFactory, CommonErrorFactory>();
    }
}
