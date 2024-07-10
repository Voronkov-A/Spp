using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Spp.Authorization.Domain.GeneratedUserNameParts;
using Spp.Authorization.Events.GeneratedUserNameParts;
using Spp.Authorization.Persistence.Schemas.Indices;
using Spp.Common.Cqs;
using Spp.Common.Domain;
using Spp.Common.Mediator;
using Spp.Common.Miscellaneous;
using GeneratedUserNamePartType = Spp.Authorization.Domain.GeneratedUserNameParts.GeneratedUserNamePartType;

namespace Spp.Authorization.Persistence.GeneratedUserNameParts;

public class GeneratedUserNamePartIndexEventHandler(IndicesDatabase database) :
    IRequestHandler<UpdateIndexCommand<GeneratedUserNamePartCreated>, Unit>,
    IRequestHandler<UpdateIndexCommand<GeneratedUserNamePartDeleted>, Unit>
{
    public async Task<Unit> Handle(
        UpdateIndexCommand<GeneratedUserNamePartCreated> request,
        CancellationToken cancellationToken)
    {
        var tableName = GeneratedUserNamePartIndex.GetTableName(request.Event.Type.ToDomain());

        await using var connection = database.CreateConnection();
        var command = new CommandDefinition(
            $"""
            insert into indices.{tableName} (GeneratedUserNamePartId, Index)
            select @GeneratedUserNamePartId, coalesce(max(Index), 0) + 1 from indices.{tableName};
            """,
            new
            {
                GeneratedUserNamePartId = request.AggregateId.ToString()
            },
            cancellationToken: cancellationToken);
        await connection.ExecuteAsync(command);
        return default;
    }

    public async Task<Unit> Handle(
        UpdateIndexCommand<GeneratedUserNamePartDeleted> request,
        CancellationToken cancellationToken)
    {
        await using var connection = database.CreateConnection();

        var tableName = GeneratedUserNamePartIndex.GetTableName(GeneratedUserNamePartType.FirstName);
        var index = await Delete(tableName, connection, request.AggregateId, cancellationToken);

        if (index == null)
        {
            tableName = GeneratedUserNamePartIndex.GetTableName(GeneratedUserNamePartType.LastName);
            index = await Delete(tableName, connection, request.AggregateId, cancellationToken);
        }

        if (index == null)
        {
            return default;
        }

        var command = new CommandDefinition(
            $"""
            update indices.{tableName} set Index = @Index where Index = (select max(Index) from indices.{tableName});
            """,
            new
            {
                GeneratedUserNamePartId = request.AggregateId.ToString()
            },
            cancellationToken: cancellationToken);
        await connection.ExecuteAsync(command);
        return default;
    }

    private static async Task<long?> Delete(
        string tableName,
        IDbConnection connection,
        EntityId id,
        CancellationToken cancellationToken)
    {
        var command = new CommandDefinition(
            $"delete from indices.{tableName} where Index GeneratedUserNamePartId = @Id returning Index;",
            new
            {
                Id = id
            },
            cancellationToken: cancellationToken);
        return await connection.QuerySingleOrDefaultAsync<long?>(command);
    }
}
