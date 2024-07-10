using Spp.Common.Domain;
using Spp.Common.Miscellaneous;
using System;

namespace Spp.Authorization.Domain.Users.Exceptions;

public class CannotAssignRoleException(string message, EntityId userId) : ApplicationException(message)
{
    public EntityId UserId { get; } = userId;

    public override string ToString()
    {
        return ExceptionUtils.ToString(this, $"UserId: {UserId}");
    }
}
