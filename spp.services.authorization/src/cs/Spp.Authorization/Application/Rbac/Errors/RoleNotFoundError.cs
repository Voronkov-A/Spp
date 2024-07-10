using Spp.Common.Domain;

namespace Spp.Authorization.Application.Rbac.Errors;

public class RoleNotFoundError(EntityId roleId)
{
    public EntityId RoleId { get; } = roleId;
}
