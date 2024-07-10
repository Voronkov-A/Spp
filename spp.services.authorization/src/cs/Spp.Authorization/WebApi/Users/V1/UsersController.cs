using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spp.Authorization.Application.Users.Commands;
using Spp.Authorization.Application.Users.Errors;
using Spp.Authorization.WebApi.Rbac.Authorization;
using Spp.Common.Domain;
using Spp.Common.Errors;
using Spp.Common.Mediator;
using Spp.Common.Miscellaneous;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Authorization.WebApi.Users.V1;

[Authorize]
public class UsersController(
    IMediator mediator,
    ICommonErrorFactory commonErrorFactory) :
    BaseUsersController
{
    [Authorize(AuthorizationPolicies.ManageUserRoles)]
    public override Task<IActionResult> AssignRoleEndpoint(
        string id,
        string roleId,
        CancellationToken cancellationToken)
    {
        return base.AssignRoleEndpoint(id, roleId, cancellationToken);
    }

    [Authorize(AuthorizationPolicies.ManageUserRoles)]
    public override Task<IActionResult> UnassignRoleEndpoint(
        string id,
        string roleId,
        CancellationToken cancellationToken)
    {
        return base.UnassignRoleEndpoint(id, roleId, cancellationToken);
    }

    protected override async Task<AssignRoleActionResult> AssignRole(
        string id,
        string roleId,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Dispatch<AssignUserRoleCommand, AssignUserRoleCommandResponse>(
            new AssignUserRoleCommand(new EntityId(id), new EntityId(roleId)),
            cancellationToken);
        return result.Switch(
            (Unit x) => AssignRoleActionResult.Create204(),
            (UserNotFoundError x) => AssignRoleActionResult.Create404(
                commonErrorFactory.ResourceNotFound($"User with id '{x.UserId}' has not been found.")),
            (RoleNotFoundError x) => AssignRoleActionResult.Create404(
                commonErrorFactory.ResourceNotFound($"Role with id '{x.RoleId}' has not been found.")));
    }

    protected override async Task<UnassignRoleActionResult> UnassignRole(
        string id,
        string roleId,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Dispatch<UnassignUserRoleCommand, UnassignUserRoleCommandResponse>(
            new UnassignUserRoleCommand(new EntityId(id), new EntityId(roleId)),
            cancellationToken);
        return result.Switch(
            (Unit x) => UnassignRoleActionResult.Create204(),
            (UserNotFoundError x) => UnassignRoleActionResult.Create404(
                commonErrorFactory.ResourceNotFound(
                    $"User with id '{x.UserId}' has not been found.")),
            (RoleNotFoundError x) => UnassignRoleActionResult.Create404(
                commonErrorFactory.ResourceNotFound(
                    $"Role with id '{x.RoleId}' has not been found or the user is not assigned to it.")));
    }
}
