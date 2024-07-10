#nullable enable

namespace Spp.Authorization.Client.Auth.V1;


/// <summary>
/// Create role request.
/// </summary>
public partial class CreateRoleRequest
{
    public CreateRoleRequest(
        LocalizedName name,
        bool isDefault,
        System.Collections.Generic.IReadOnlyList<PermissionReference> permissions
    )
    {
        this.Name = name;
        this.IsDefault = isDefault;
        this.Permissions = permissions;
    }

    /// <summary>
    /// 
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("name")]
    public LocalizedName Name { get; }
    /// <summary>
    /// If true, the role will be assigned to all newly registered users.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("isDefault")]
    public bool IsDefault { get; }
    /// <summary>
    /// Role permission references.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("permissions")]
    public System.Collections.Generic.IReadOnlyList<PermissionReference> Permissions { get; }
}


#nullable restore
