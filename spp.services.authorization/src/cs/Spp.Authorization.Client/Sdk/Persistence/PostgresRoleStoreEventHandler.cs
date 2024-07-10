using Dapper;
using Spp.Authorization.Events.Rbac;
using Spp.Common.Mediator;
using Spp.Common.Miscellaneous;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Authorization.Client.Sdk.Persistence;

internal class PostgresRoleStoreEventHandler(AuthorizationDatabase database) :
    IRequestHandler<UpdateAuthorizationPersistenceCommand<RoleCreated>, Unit>,
    IRequestHandler<UpdateAuthorizationPersistenceCommand<RoleDeleted>, Unit>
{
    public async Task<Unit> Handle(
        UpdateAuthorizationPersistenceCommand<RoleCreated> request,
        CancellationToken cancellationToken)
    {
        await using var connection = database.CreateConnection();
        var command = new CommandDefinition(
            $"""
            insert into {database.SchemaName}.RolePermissions (RoleId, PermissionGroupId, PermissionId)
            select @RoleId, unnest(@PermissionGroupIds), unnest(@PermissionIds);
            """,
            new
            {
                RoleId = request.AggregateId.ToString(),
                PermissionGroupIds = request.Event.Permissions.Select(x => x.PermissionGroupId.ToString()).ToArray(),
                PermissionIds = request.Event.Permissions.Select(x => x.PermissionId.ToString()).ToArray()
            },
            cancellationToken: cancellationToken);
        await connection.ExecuteAsync(command);
        return default;
    }

    public async Task<Unit> Handle(
        UpdateAuthorizationPersistenceCommand<RoleDeleted> request,
        CancellationToken cancellationToken)
    {
        await using var connection = database.CreateConnection();
        var command = new CommandDefinition(
            $"delete from {database.SchemaName}.RolePermissions where RoleId = @RoleId;",
            new
            {
                RoleId = request.AggregateId.ToString()
            },
            cancellationToken: cancellationToken);
        await connection.ExecuteAsync(command);
        return default;
    }
}
