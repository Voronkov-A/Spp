using Spp.Common.Authorization;
using Spp.Common.Domain;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Authorization.Client.Sdk.Domain;

internal class PermissionResolver(IRoleStore roleStore) : IPermissionResolver
{
    public async Task<bool> HasPermission(
        ClaimsPrincipal user,
        string permissionId,
        CancellationToken cancellationToken)
    {
        var roleIds = user.Claims.Where(x => x.Type == RoleClaim.Type).Select(x => new EntityId(x.Value)).ToList();

        if (roleIds.Count == 0)
        {
            return false;
        }

        var roles = await roleStore.GetRoles(roleIds, cancellationToken);
        return PermissionReference.TryParse(permissionId, out var reference)
            && roles.Any(x => x.HasPermission(reference));
    }
}
