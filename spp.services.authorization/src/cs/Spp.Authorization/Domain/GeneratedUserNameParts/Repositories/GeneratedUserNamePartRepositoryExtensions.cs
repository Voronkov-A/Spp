using Spp.Common.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Authorization.Domain.GeneratedUserNameParts.Repositories;

public static class GeneratedUserNamePartRepositoryExtensions
{
    public static async Task<GeneratedUserNamePart> Get(
        this IGeneratedUserNamePartRepository self,
        EntityId id,
        CancellationToken cancellationToken)
    {
        return await self.Find(id, cancellationToken)
            ?? throw new InvalidOperationException($"Could not find aggregate with id '{id}'.");
    }
}
