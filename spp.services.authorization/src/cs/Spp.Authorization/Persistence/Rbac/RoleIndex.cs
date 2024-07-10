using Dapper;
using Spp.Authorization.Persistence.Schemas.Indices;
using Spp.Common.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Authorization.Persistence.Rbac;

public class RoleIndex(IndicesDatabase database) : IRoleIndex
{
    public async Task<List<EntityId>> GetAll(bool isDefault, CancellationToken cancellationToken)
    {
        var command = new CommandDefinition(
            "select RoleId from indices.RoleIsDefaults where IsDefault;",
            cancellationToken: cancellationToken);
        await using var connection = database.CreateConnection();
        var entityIds = await connection.QueryAsync<string>(command);
        return entityIds.Select(x => new EntityId(x)).ToList();
    }
}
