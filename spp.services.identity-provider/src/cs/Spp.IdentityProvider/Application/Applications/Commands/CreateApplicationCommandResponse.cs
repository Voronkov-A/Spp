using Spp.Common.Miscellaneous;
using Spp.IdentityProvider.Application.Applications.Errors;
using Spp.IdentityProvider.WebApi.Applications.V1;

namespace Spp.IdentityProvider.Application.Applications.Commands;

public class CreateApplicationCommandResponse(
    AnyOf<CreateApplicationResponse, DuplicateClientIdError> inner) :
    BaseAnyOf<CreateApplicationResponse, DuplicateClientIdError>(inner);
