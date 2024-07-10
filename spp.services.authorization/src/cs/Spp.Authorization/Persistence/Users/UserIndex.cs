using Dapper;
using Spp.Authorization.Domain.Users;
using Spp.Authorization.Persistence.Schemas.Indices;
using Spp.Common.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Authorization.Persistence.Users;

public class UserIndex(IndicesDatabase database) : IUserIndex
{
    public async Task<EntityId?> Find(UserIdentity identity, CancellationToken cancellationToken)
    {
        var command = new CommandDefinition(
            "select UserId from indices.UserIdentities where ProviderId = @ProviderId and SubjectId = @SubjectId;",
            identity,
            cancellationToken: cancellationToken);
        await using var connection = database.CreateConnection();
        var entityId = await connection.QuerySingleOrDefaultAsync<string>(command);
        return entityId == null ? null : new EntityId(entityId);
    }

    public async Task<EntityId?> FindLast(GeneratedUserNameStem userNameStem, CancellationToken cancellationToken)
    {
        var command = new CommandDefinition(
            """
            select UserId
            from indices.UserNames
            where coalesce(indices.GetGeneratedUserNameStem(Value), Value) = @Stem
            order by length(Value) desc, Value desc limit 1;
            """,
            new
            {
                Stem = userNameStem.ToString()
            },
            cancellationToken: cancellationToken);
        await using var connection = database.CreateConnection();
        var entityId = await connection.QuerySingleOrDefaultAsync<string>(command);
        return entityId == null ? null : new EntityId(entityId);
    }

    public async Task<bool> Exists(UserName userName, CancellationToken cancellationToken)
    {
        var command = new CommandDefinition(
            """
            select exists (select * from indices.UserNames where Value = @Value);
            """,
            new
            {
                Value = userName.ToString()
            },
            cancellationToken: cancellationToken);
        await using var connection = database.CreateConnection();
        return await connection.QuerySingleAsync<bool>(command);
    }
}
