#nullable enable

namespace Spp.IdentityProvider.TestClient.Connect.V1;

/// <summary>
/// Token type.
/// </summary>
public enum TokenType
{
    [System.Runtime.Serialization.EnumMember(Value = "access_token")]
    AccessToken = 0,
    [System.Runtime.Serialization.EnumMember(Value = "refresh_token")]
    RefreshToken
}



#nullable restore
