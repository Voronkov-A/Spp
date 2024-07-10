#nullable enable

namespace Spp.Authorization.WebApi.Common.V1;

/// <summary>
/// Authorization permission ids.
/// </summary>
public enum AuthorizationPermissionIds
{
    [System.Runtime.Serialization.EnumMember(Value = "manageRoles")]
    ManageRoles = 0,
    [System.Runtime.Serialization.EnumMember(Value = "manageUserRoles")]
    ManageUserRoles
}



#nullable restore
