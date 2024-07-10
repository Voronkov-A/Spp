using System.Threading;
using System.Threading.Tasks;

namespace Spp.Common.Authentication.Abstractions;

public interface IAccessTokenAcquirer
{
    Task<AccessToken> AcquireAccessToken(CancellationToken cancellationToken);
}

public interface IAccessTokenAcquirer<T> : IAccessTokenAcquirer;
