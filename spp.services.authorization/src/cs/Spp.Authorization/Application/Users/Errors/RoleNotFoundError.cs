using Spp.Common.Domain;

namespace Spp.Authorization.Application.Users.Errors;

public class RoleNotFoundError(EntityId roleId)
{
    public EntityId RoleId { get; } = roleId;
}
