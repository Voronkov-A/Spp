using Spp.Authorization.Domain.Users;
using Spp.Common.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Authorization.Persistence.Users;

public interface IUserIndex
{
    Task<EntityId?> Find(UserIdentity identity, CancellationToken cancellationToken);

    Task<EntityId?> FindLast(GeneratedUserNameStem userNameStem, CancellationToken cancellationToken);

    Task<bool> Exists(UserName name, CancellationToken cancellationToken);
}
