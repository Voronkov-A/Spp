#nullable enable

namespace Spp.Authorization.Client.Auth.V1;


/// <summary>
/// Permission.
/// </summary>
public partial class Permission
{
    public Permission(
        string id
    )
    {
        this.Id = id;
    }

    /// <summary>
    /// Identifier.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("id")]
    public string Id { get; }
}


#nullable restore
