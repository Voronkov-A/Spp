using Spp.Common.Domain;
using Spp.Common.Miscellaneous;
using System;

namespace Spp.Authorization.Domain.Common.Exceptions;

public class EntityNotFoundException(string message, EntityId entityId) : ApplicationException(message)
{
    public EntityId EntityId { get; } = entityId;

    public override string ToString()
    {
        return ExceptionUtils.ToString(this, $"EntityId: {EntityId}");
    }
}
