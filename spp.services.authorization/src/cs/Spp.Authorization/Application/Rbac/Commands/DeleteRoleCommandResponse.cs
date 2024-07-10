using Spp.Authorization.Application.Rbac.Errors;
using Spp.Common.Miscellaneous;

namespace Spp.Authorization.Application.Rbac.Commands;

public class DeleteRoleCommandResponse(
    AnyOf<Unit, RoleNotFoundError> inner) :
    BaseAnyOf<Unit, RoleNotFoundError>(inner);
