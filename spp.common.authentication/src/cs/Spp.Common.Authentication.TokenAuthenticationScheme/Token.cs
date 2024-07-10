using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Spp.Common.Authentication.TokenAuthenticationScheme;

public readonly struct Token(IEnumerable<Claim> claims)
{
    public Token() : this(Enumerable.Empty<Claim>())
    {
    }

    public IReadOnlyCollection<Claim> Claims { get; } = claims.ToList();
}
