using Spp.Authorization.Application.Users.Errors;
using Spp.Common.Miscellaneous;

namespace Spp.Authorization.Application.Users.Commands;

public class AssignUserRoleCommandResponse(
    AnyOf<Unit, UserNotFoundError, RoleNotFoundError> inner) :
    BaseAnyOf<Unit, UserNotFoundError, RoleNotFoundError>(inner);
