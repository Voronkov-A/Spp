using Spp.Common.Miscellaneous;
using Spp.IdentityProvider.Application.Users.Errors;
using Spp.IdentityProvider.WebApi.Users.V1;

namespace Spp.IdentityProvider.Application.Users.Commands;

public class CreateUserCommandResponse(
    AnyOf<CreateUserResponse, DuplicateUsernameError> inner) :
    BaseAnyOf<CreateUserResponse, DuplicateUsernameError>(inner);
