using Spp.Common.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Authorization.Domain.Rbac.Repositories;

public interface IPermissionGroupRepository
{
    Task<PermissionGroup?> Find(EntityId id, CancellationToken cancellationToken);

    Task Add(PermissionGroup item, CancellationToken cancellationToken);
}
