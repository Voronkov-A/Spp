using Microsoft.AspNetCore.Authorization;
using Spp.Authorization.Application.Rbac.Commands;
using Spp.Common.Domain;
using System.Threading;
using System.Threading.Tasks;
using Spp.Authorization.Application.Rbac.Errors;
using Spp.Authorization.WebApi.Common;
using Spp.Authorization.WebApi.Common.V1;
using Microsoft.AspNetCore.Mvc;
using Spp.Authorization.WebApi.Rbac.Authorization;
using Spp.Common.Miscellaneous;
using Spp.Common.Mediator;
using Spp.Common.Errors;

namespace Spp.Authorization.WebApi.Rbac.V1;

[Authorize]
public class RbacController(
    IMediator mediator,
    IAuthorizationErrorFactory authorizationErrorFactory,
    ICommonErrorFactory commonErrorFactory) :
    BaseRbacController
{
    [Authorize(AuthorizationPolicies.ClientOnly)]
    public override async Task<IActionResult> CreateOrUpdatePermissionGroupEndpoint(
        string id,
        CreateOrUpdatePermissionGroupRequest createOrUpdatePermissionGroupRequest,
        CancellationToken cancellationToken)
    {
        return await base.CreateOrUpdatePermissionGroupEndpoint(
            id,
            createOrUpdatePermissionGroupRequest,
            cancellationToken);
    }

    [Authorize(AuthorizationPolicies.ManageRoles)]
    public override async Task<IActionResult> CreateRoleEndpoint(
        CreateRoleRequest createRoleRequest,
        CancellationToken cancellationToken)
    {
        return await base.CreateRoleEndpoint(createRoleRequest, cancellationToken);
    }

    [Authorize(AuthorizationPolicies.ManageRoles)]
    public override async Task<IActionResult> DeleteRoleEndpoint(string id, CancellationToken cancellationToken)
    {
        return await base.DeleteRoleEndpoint(id, cancellationToken);
    }

    protected override async Task<CreateOrUpdatePermissionGroupActionResult> CreateOrUpdatePermissionGroup(
        string id,
        CreateOrUpdatePermissionGroupRequest createOrUpdatePermissionGroupRequest,
        CancellationToken cancellationToken)
    {
        await mediator.Dispatch<CreateOrUpdatePermissionGroupCommand, Unit>(
            new CreateOrUpdatePermissionGroupCommand(new EntityId(id), createOrUpdatePermissionGroupRequest),
            cancellationToken);
        return CreateOrUpdatePermissionGroupActionResult.Create204();
    }

    protected override async Task<CreateRoleActionResult> CreateRole(
        CreateRoleRequest createRoleRequest,
        CancellationToken cancellationToken)
    {
        var result = await mediator.Dispatch<CreateRoleCommand, CreateRoleCommandResponse>(
            new CreateRoleCommand(createRoleRequest),
            cancellationToken);
        return result.Switch(
            (CreateRoleResponse x) => CreateRoleActionResult.Create201(x),
            (InvalidNameError x) => CreateRoleActionResult.Create400(
                authorizationErrorFactory.InvalidName(
                    $"Invalid name format: {x.Name}.",
                    new InvalidNameErrorParameters(x.Name))),
            (PermissionNotFoundError x) => CreateRoleActionResult.Create400(
                authorizationErrorFactory.PermissionNotFound(
                    $"Permission not found: {x.PermissionGroupId}/{x.PermissionId}.",
                    new PermissionNotFoundErrorParameters(x.PermissionGroupId.ToString(), x.PermissionId.ToString()))));
    }

    protected override async Task<DeleteRoleActionResult> DeleteRole(string id, CancellationToken cancellationToken)
    {
        var result = await mediator.Dispatch<DeleteRoleCommand, DeleteRoleCommandResponse>(
            new DeleteRoleCommand(new EntityId(id)),
            cancellationToken);
        return result.Switch(
            (Unit x) => DeleteRoleActionResult.Create204(),
            (RoleNotFoundError x) => DeleteRoleActionResult.Create404(
                commonErrorFactory.ResourceNotFound($"Role with id '{x.RoleId}' has not been found.")));
    }
}
