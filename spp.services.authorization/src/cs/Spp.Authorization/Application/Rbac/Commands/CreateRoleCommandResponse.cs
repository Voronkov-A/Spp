using Spp.Authorization.Application.Rbac.Errors;
using Spp.Authorization.WebApi.Rbac.V1;
using Spp.Common.Miscellaneous;

namespace Spp.Authorization.Application.Rbac.Commands;

public class CreateRoleCommandResponse(
    AnyOf<CreateRoleResponse, InvalidNameError, PermissionNotFoundError> inner) :
    BaseAnyOf<CreateRoleResponse, InvalidNameError, PermissionNotFoundError>(inner);
