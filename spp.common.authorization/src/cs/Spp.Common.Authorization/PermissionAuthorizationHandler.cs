using Microsoft.AspNetCore.Authorization;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Common.Authorization;

public class PermissionAuthorizationHandler(IPermissionResolver permissionResolver) :
    AuthorizationHandler<PermissionRequirement>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        if (await permissionResolver.HasPermission(context.User, requirement.Permission, CancellationToken.None))
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail(new AuthorizationFailureReason(
                this,
                $"User does not have permission '{requirement.Permission}'."));
        }
    }
}
