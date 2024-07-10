#nullable enable

namespace Spp.Authorization.WebApi.Common.V1;

/// <summary>
/// Authorization error code.
/// </summary>
public enum AuthorizationErrorCode
{
    [System.Runtime.Serialization.EnumMember(Value = "authorization.invalidName")]
    AuthorizationInvalidName = 0,
    [System.Runtime.Serialization.EnumMember(Value = "authorization.permissionNotFound")]
    AuthorizationPermissionNotFound
}



#nullable restore
