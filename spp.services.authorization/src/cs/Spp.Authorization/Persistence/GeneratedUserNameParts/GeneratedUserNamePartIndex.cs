using Dapper;
using Spp.Authorization.Domain.GeneratedUserNameParts;
using Spp.Authorization.Persistence.Schemas.Indices;
using Spp.Common.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Authorization.Persistence.GeneratedUserNameParts;

public class GeneratedUserNamePartIndex(IndicesDatabase database) : IGeneratedUserNamePartIndex
{
    private readonly Random _random = new();

    public async Task<List<EntityId>> GetSomeRandom(
        GeneratedUserNamePartType type,
        int maxCount,
        CancellationToken cancellationToken)
    {
        var tableName = GetTableName(type);
        var command = new CommandDefinition(
            $"""
             select GeneratedUserNamePartId
             from indices.{tableName}
             where Index + @MaxCount - 1 >= random() * (select max(Index) from indices.{tableName})
             order by Index
             limit 1;
             """,
            new
            {
                MaxCount = maxCount
            },
            cancellationToken: cancellationToken);
        await using var connection = database.CreateConnection();
        var ids = await connection.QueryAsync<string>(command);
        return ids.Select(x => new EntityId(x)).OrderBy(_ => _random.Next()).ToList();
    }

    public static string GetTableName(GeneratedUserNamePartType type)
    {
        return type switch
        {
            GeneratedUserNamePartType.FirstName => "FirstNames",
            GeneratedUserNamePartType.LastName => "LastNames",
            _ => throw new NotSupportedException($"{nameof(GeneratedUserNamePartType)}.{type} is not supported.")
        };
    }
}
