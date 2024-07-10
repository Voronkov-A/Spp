using Duende.IdentityServer.Validation;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Common.Authentication.TokenAuthenticationScheme.IdentityServer;

public class IdentityServerTokenParser(ITokenValidator tokenValidator) : ITokenParser
{
    public async Task<TokenParseResult> Parse(string token, CancellationToken cancellationToken)
    {
        var result = await tokenValidator.ValidateAccessTokenAsync(token);
        return result.IsError
            ? TokenParseResult.Fail(result.ErrorDescription ?? result.Error ?? "Unknown error.")
            : TokenParseResult.Success(new Token(result.Claims ?? Enumerable.Empty<Claim>()));
    }
}
