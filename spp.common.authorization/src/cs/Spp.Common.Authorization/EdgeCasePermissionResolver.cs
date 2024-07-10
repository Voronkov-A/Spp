using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Common.Authorization;

public class EdgeCasePermissionResolver(IPermissionResolver inner) : IPermissionResolver
{
    public async Task<bool> HasPermission(
        ClaimsPrincipal user,
        string permission,
        CancellationToken cancellationToken)
    {
        return user.IsClient()
            || user.IsSuperuser()
            || await inner.HasPermission(user, permission, cancellationToken);
    }
}
