using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Common.Authentication.TokenAuthenticationScheme;

public interface ITokenGenerator
{
    Task<TokenGenerationResult> GenerateTokens(
        ClaimsPrincipal user,
        AuthenticationProperties? properties,
        CancellationToken cancellationToken);
}
