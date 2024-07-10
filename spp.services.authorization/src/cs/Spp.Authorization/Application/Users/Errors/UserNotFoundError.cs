using Spp.Common.Domain;

namespace Spp.Authorization.Application.Users.Errors;

public class UserNotFoundError(EntityId userId)
{
    public EntityId UserId { get; } = userId;
}
