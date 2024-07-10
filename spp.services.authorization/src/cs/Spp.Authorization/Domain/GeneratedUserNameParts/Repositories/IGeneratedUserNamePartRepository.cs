using System.Threading;
using System.Threading.Tasks;
using Spp.Common.Domain;

namespace Spp.Authorization.Domain.GeneratedUserNameParts.Repositories;

public interface IGeneratedUserNamePartRepository
{
    Task<GeneratedUserNamePart?> Find(EntityId id, CancellationToken cancellationToken);

    Task<GeneratedUserNamePart?> FindRandom(GeneratedUserNamePartType type, CancellationToken cancellationToken);
}
