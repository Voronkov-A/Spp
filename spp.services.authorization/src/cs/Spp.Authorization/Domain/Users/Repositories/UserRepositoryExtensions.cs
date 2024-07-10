using Spp.Common.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Authorization.Domain.Users.Repositories;

public static class UserRepositoryExtensions
{
    public static async Task<User> Get(this IUserRepository self, EntityId id, CancellationToken cancellationToken)
    {
        return await self.Find(id, cancellationToken)
            ?? throw new InvalidOperationException($"Could not find aggregate with id '{id}'.");
    }
}
