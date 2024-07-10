#nullable enable

namespace Spp.IdentityProvider.Client.Connect.V1;

/// <summary>
/// Grant type.
/// </summary>
public enum GrantType
{
    [System.Runtime.Serialization.EnumMember(Value = "authorization_code")]
    AuthorizationCode = 0,
    [System.Runtime.Serialization.EnumMember(Value = "client_credentials")]
    ClientCredentials,
    [System.Runtime.Serialization.EnumMember(Value = "refresh_token")]
    RefreshToken
}



#nullable restore
