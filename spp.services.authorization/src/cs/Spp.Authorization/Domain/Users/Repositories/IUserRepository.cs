using Spp.Common.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Authorization.Domain.Users.Repositories;

public interface IUserRepository
{
    Task<User?> Find(EntityId id, CancellationToken cancellationToken);

    Task<User?> Find(UserIdentity identity, CancellationToken cancellationToken);

    Task<User?> FindLast(GeneratedUserNameStem userNameStem, CancellationToken cancellationToken);

    Task<bool> Exists(UserName name, CancellationToken cancellationToken);

    Task Add(User item, CancellationToken cancellationToken);
}
