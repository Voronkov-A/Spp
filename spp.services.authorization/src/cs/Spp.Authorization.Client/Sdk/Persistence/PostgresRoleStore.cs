using Dapper;
using Spp.Authorization.Client.Sdk.Domain;
using Spp.Common.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Authorization.Client.Sdk.Persistence;

internal class PostgresRoleStore(AuthorizationDatabase database) : IRoleStore
{
    public async Task<IEnumerable<Role>> GetRoles(IEnumerable<EntityId> ids, CancellationToken cancellationToken)
    {
        await using var connection = database.CreateConnection();
        var command = new CommandDefinition(
            $"""
            select RoleId, PermissionGroupId, PermissionId
            from {database.SchemaName}.RolePermissions
            where RoleId = any(@RoleIds);
            """,
            new
            {
                RoleIds = ids.Select(x => x.ToString()).ToArray()
            },
            cancellationToken: cancellationToken);
        return (await connection.QueryAsync<RolePermissionDto>(command))
            .GroupBy(x => x.RoleId)
            .Select(x => new Role(
                new EntityId(x.Key),
                x.Select(dto => new PermissionReference(
                    new EntityId(dto.PermissionGroupId),
                    new EntityId(dto.PermissionId)))));
    }
}
