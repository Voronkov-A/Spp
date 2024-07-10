using System.Threading;
using System.Threading.Tasks;

namespace Spp.Common.Authentication.TokenAuthenticationScheme;

public interface ITokenParser
{
    Task<TokenParseResult> Parse(string token, CancellationToken cancellationToken);
}
