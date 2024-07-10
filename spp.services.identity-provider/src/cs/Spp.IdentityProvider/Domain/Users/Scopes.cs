using IdentityModel;
using System.Collections.Generic;

namespace Spp.IdentityProvider.Domain.Users;

public static class Scopes
{
    public const string OpenId = OidcConstants.StandardScopes.OpenId;

    public const string OfflineAccess = OidcConstants.StandardScopes.OfflineAccess;

    public const string Profile = OidcConstants.StandardScopes.Profile;

    public const string Api = "api";

    public static readonly string All = string.Join(' ', Enumerate());

    public static IEnumerable<string> Enumerate()
    {
        yield return OpenId;
        yield return OfflineAccess;
        yield return Profile;
        yield return Api;
    }
}
