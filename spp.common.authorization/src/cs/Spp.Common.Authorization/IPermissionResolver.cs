using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Common.Authorization;

public interface IPermissionResolver
{
    Task<bool> HasPermission(ClaimsPrincipal user, string permission, CancellationToken cancellationToken);
}
