using Microsoft.Extensions.DependencyInjection;
using Spp.Authorization.Domain.Users;

namespace Spp.Authorization.Domain;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        return services.AddScoped<IUserNameGenerator, UserNameGenerator>();
    }
}
