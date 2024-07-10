using Spp.Authorization.Domain.Rbac;
using Spp.Authorization.Domain.Rbac.Repositories;
using Spp.Common.Domain;
using Spp.Common.Mediator;
using Spp.Common.Miscellaneous;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Authorization.Application.Rbac.Commands;

public class CreateOrUpdatePermissionGroupCommandHandler(IPermissionGroupRepository repository) :
    IRequestHandler<CreateOrUpdatePermissionGroupCommand, Unit>
{
    public async Task<Unit> Handle(CreateOrUpdatePermissionGroupCommand request, CancellationToken cancellationToken)
    {
        var permissionGroupId = request.PermissionGroupId;
        var permissions = request.Data.Permissions.Select(x => new Permission(new EntityId(x.Id)));

        var permissionGroup = await repository.Find(permissionGroupId, cancellationToken);

        if (permissionGroup == null)
        {
            permissionGroup = new PermissionGroup(permissionGroupId, permissions);
            await repository.Add(permissionGroup, cancellationToken);
        }
        else
        {
            permissionGroup.Update(permissions);
        }

        return default;
    }
}
