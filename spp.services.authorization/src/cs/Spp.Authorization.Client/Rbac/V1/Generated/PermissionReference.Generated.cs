#nullable enable

namespace Spp.Authorization.Client.Auth.V1;


/// <summary>
/// Permission reference.
/// </summary>
public partial class PermissionReference
{
    public PermissionReference(
        string permissionGroupId,
        string permissionId
    )
    {
        this.PermissionGroupId = permissionGroupId;
        this.PermissionId = permissionId;
    }

    /// <summary>
    /// Permission group identifier.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("permissionGroupId")]
    public string PermissionGroupId { get; }
    /// <summary>
    /// Permission identifier.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("permissionId")]
    public string PermissionId { get; }
}


#nullable restore
