using Microsoft.Extensions.DependencyInjection;
using Spp.IdentityProvider.WebApi.Errors;

namespace Spp.IdentityProvider.WebApi;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWebApi(this IServiceCollection services)
    {
        return services
            .AddSingleton<IIdentityProviderErrorFactory, IdentityProviderErrorFactory>();
    }
}
