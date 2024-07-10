using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Spp.Authorization.Client.Auth.V1;
using Spp.Authorization.Client.Sdk.Domain;
using Spp.Common.Authentication.Abstractions;
using Spp.Common.Exceptions;
using System;
using System.Linq;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Authorization.Client.Sdk.Background;

internal class RegisterPermissionGroupBackgroundService(
    IRbacClient client,
    PermissionGroupDefinition permissionGroup,
    IExceptionResolver exceptionResolver,
    IAuthenticationContext<AuthorizationAuthenticationContext> authenticationContext,
    ILogger<RegisterPermissionGroupBackgroundService> logger) :
    BackgroundService
{
    private readonly static TimeSpan RetryTimeout = TimeSpan.FromSeconds(15);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var id = permissionGroup.Id.ToString();
        var request = new CreateOrUpdatePermissionGroupRequest(
            permissionGroup.Permissions.Select(x => new Permission(x.ToString())).ToList());
        var transientError = false;
        using (authenticationContext.Activate())
        {
            do
            {
                try
                {
                    await client.CreateOrUpdatePermissionGroup(id, request, stoppingToken);
                    transientError = false;
                }
                catch (Exception ex) when (exceptionResolver.IsTransient(ex) || ex is AuthenticationException)
                {
                    logger.LogWarning(ex, "Transient error while registering permission group.");
                    transientError = true;
                    await Task.Delay(RetryTimeout, stoppingToken);
                }
            }
            while (transientError);
        }
    }
}
