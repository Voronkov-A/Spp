using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Spp.Common.Domain;

namespace Spp.Authorization.Client.Sdk.Domain;

internal interface IRoleStore
{
    Task<IEnumerable<Role>> GetRoles(IEnumerable<EntityId> ids, CancellationToken cancellationToken);
}
