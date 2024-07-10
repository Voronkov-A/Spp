#nullable enable

namespace Spp.Authorization.TestClient.Rbac.V1;


/// <summary>
/// Create or update permission group request.
/// </summary>
public partial class CreateOrUpdatePermissionGroupRequest
{
    public CreateOrUpdatePermissionGroupRequest(
        System.Collections.Generic.IReadOnlyList<Permission> permissions
    )
    {
        this.Permissions = permissions;
    }

    /// <summary>
    /// Permissions.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("permissions")]
    public System.Collections.Generic.IReadOnlyList<Permission> Permissions { get; }
}


#nullable restore
