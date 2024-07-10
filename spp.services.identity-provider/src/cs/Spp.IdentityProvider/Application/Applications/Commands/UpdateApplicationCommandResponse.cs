using Spp.Common.Miscellaneous;
using Spp.IdentityProvider.Application.Applications.Errors;

namespace Spp.IdentityProvider.Application.Applications.Commands;

public class UpdateApplicationCommandResponse(
    AnyOf<Unit, ApplicationNotFoundError> inner) :
    BaseAnyOf<Unit, ApplicationNotFoundError>(inner);
