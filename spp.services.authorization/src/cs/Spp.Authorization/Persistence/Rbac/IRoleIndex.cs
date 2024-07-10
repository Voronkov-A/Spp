using Spp.Common.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Authorization.Persistence.Rbac;

public interface IRoleIndex
{
    Task<List<EntityId>> GetAll(bool isDefault, CancellationToken cancellationToken);
}
