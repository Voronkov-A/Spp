using Spp.Authorization.Application.Rbac.Errors;
using Spp.Authorization.Domain.Common.Exceptions;
using Spp.Authorization.Domain.Rbac;
using Spp.Authorization.Domain.Rbac.Repositories;
using Spp.Common.Miscellaneous;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Spp.Authorization.WebApi.Rbac.V1;
using Spp.Common.Domain;
using PermissionReference = Spp.Authorization.Domain.Rbac.PermissionReference;
using Translation = Spp.Authorization.Domain.Rbac.Translation;
using Spp.Common.Mediator;

namespace Spp.Authorization.Application.Rbac.Commands;

public class CreateRoleCommandHandler(
    IRoleRepository roleRepository,
    IPermissionGroupRepository permissionGroupRepository) :
    IRequestHandler<CreateRoleCommand, CreateRoleCommandResponse>
{
    public async Task<CreateRoleCommandResponse> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var permissions = await CreatePermissionReferences(request, cancellationToken);

        if (!permissions.IsSucceeded)
        {
            return new CreateRoleCommandResponse(permissions.Error);
        }

        var name = CreateName(request);

        if (!name.IsSucceeded)
        {
            return new CreateRoleCommandResponse(name.Error);
        }

        var role = new Role(
            id: new EntityId(),
            name: name.Value,
            isDefault: request.Data.IsDefault,
            permissions: permissions.Value);
        await roleRepository.Add(role, cancellationToken);
        return new CreateRoleCommandResponse(new CreateRoleResponse(role.Id.ToString()));
    }

    private async Task<TryResult<IEnumerable<PermissionReference>, PermissionNotFoundError>> CreatePermissionReferences(
        CreateRoleCommand request,
        CancellationToken cancellationToken)
    {
        var permissions = new List<PermissionReference>(request.Data.Permissions.Count);

        foreach (var permissionReference in request.Data.Permissions)
        {
            var permissionGroupId = new EntityId(permissionReference.PermissionGroupId);
            var permissionId = new EntityId(permissionReference.PermissionId);

            var permissionGroup = await permissionGroupRepository.Find(
                new EntityId(permissionReference.PermissionGroupId),
                cancellationToken);

            if (permissionGroup == null)
            {
                return new PermissionNotFoundError(permissionGroupId, permissionId);
            }

            var permission = permissionGroup.FindPermission(permissionId);

            if (permission == null)
            {
                return new PermissionNotFoundError(permissionGroupId, permissionId);
            }

            permissions.Add(new PermissionReference(permissionGroupId, permissionId));
        }

        return permissions;
    }

    private static TryResult<RoleName, InvalidNameError> CreateName(CreateRoleCommand request)
    {
        try
        {
            return new RoleName(
                def: request.Data.Name.Default,
                translations: request.Data.Name.Translations
                    .Select(x => new Translation(language: x.Language, value: x.Value)));
        }
        catch (InvalidNameException ex)
        {
            return new InvalidNameError(ex.Name);
        }
    }
}
