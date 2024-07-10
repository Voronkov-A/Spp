#nullable enable

namespace Spp.IdentityProvider.WebApi.Errors.V1;

/// <summary>
/// IdentityProvider error code.
/// </summary>
public enum IdentityProviderErrorCode
{
    [System.Runtime.Serialization.EnumMember(Value = "identityProvider.duplicateClientId")]
    IdentityProviderDuplicateClientId = 0,
    [System.Runtime.Serialization.EnumMember(Value = "identityProvider.duplicateUsername")]
    IdentityProviderDuplicateUsername
}



#nullable restore
