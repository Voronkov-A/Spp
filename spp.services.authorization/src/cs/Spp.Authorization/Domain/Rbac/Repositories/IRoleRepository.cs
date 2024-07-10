using Spp.Common.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Authorization.Domain.Rbac.Repositories;

public interface IRoleRepository
{
    Task Add(Role item, CancellationToken cancellationToken);

    Task<Role?> Find(EntityId id, CancellationToken cancellationToken);

    Task<List<Role>> GetAll(bool isDefault, CancellationToken cancellationToken);
}
