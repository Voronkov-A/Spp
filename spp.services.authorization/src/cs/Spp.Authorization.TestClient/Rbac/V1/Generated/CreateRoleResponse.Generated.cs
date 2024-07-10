#nullable enable

namespace Spp.Authorization.TestClient.Rbac.V1;


/// <summary>
/// Create role response.
/// </summary>
public partial class CreateRoleResponse
{
    public CreateRoleResponse(
        string id
    )
    {
        this.Id = id;
    }

    /// <summary>
    /// Role identifier.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("id")]
    public string Id { get; }
}


#nullable restore
