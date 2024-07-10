#nullable enable

namespace Spp.Common.Errors.V1;

/// <summary>
/// Error codes that are common for all services.
/// </summary>
public enum CommonErrorCode
{
    [System.Runtime.Serialization.EnumMember(Value = "common.unknown")]
    CommonUnknown = 0,
    [System.Runtime.Serialization.EnumMember(Value = "common.authenticationFailure")]
    CommonAuthenticationFailure,
    [System.Runtime.Serialization.EnumMember(Value = "common.authorizationFailure")]
    CommonAuthorizationFailure,
    [System.Runtime.Serialization.EnumMember(Value = "common.resourceNotFound")]
    CommonResourceNotFound
}



#nullable restore
