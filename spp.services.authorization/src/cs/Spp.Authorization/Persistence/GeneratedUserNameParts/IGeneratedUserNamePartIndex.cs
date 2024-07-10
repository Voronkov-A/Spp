using System.Collections.Generic;
using Spp.Authorization.Domain.GeneratedUserNameParts;
using System.Threading;
using System.Threading.Tasks;
using Spp.Common.Domain;

namespace Spp.Authorization.Persistence.GeneratedUserNameParts;

public interface IGeneratedUserNamePartIndex
{
    Task<List<EntityId>> GetSomeRandom(
        GeneratedUserNamePartType type,
        int maxCount,
        CancellationToken cancellationToken);
}
