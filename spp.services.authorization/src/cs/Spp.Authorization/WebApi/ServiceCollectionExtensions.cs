using Microsoft.Extensions.DependencyInjection;
using Spp.Authorization.Client.Sdk;
using Spp.Authorization.WebApi.Common;
using Spp.Authorization.WebApi.Common.V1;
using Spp.Authorization.WebApi.Rbac.Authorization;
using Spp.Common.Errors;

namespace Spp.Authorization.WebApi;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWebApi(this IServiceCollection services)
    {
        services.AddHealthChecks();
        services.AddControllers().AddApplicationPart(typeof(AuthorizationStartup).Assembly);
        return services
            .AddDefaultAuthorization("authorization", c => c
                .WithClientOnlyPolicy(AuthorizationPolicies.ClientOnly)
                .WithUserOnlyPolicy(AuthorizationPolicies.UserOnly)
                .WithPermissionPolicy(
                    AuthorizationPolicies.ManageRoles,
                    AuthorizationPermissionIds.ManageRoles)
                .WithPermissionPolicy(
                    AuthorizationPolicies.ManageUserRoles,
                    AuthorizationPermissionIds.ManageUserRoles))
            .AddErrors()
            .AddSingleton<IAuthorizationErrorFactory, AuthorizationErrorFactory>();
    }
}
