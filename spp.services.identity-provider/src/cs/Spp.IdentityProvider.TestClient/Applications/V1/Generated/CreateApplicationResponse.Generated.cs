#nullable enable

namespace Spp.IdentityProvider.TestClient.Applications.V1;


/// <summary>
/// Create application response.
/// </summary>
public partial class CreateApplicationResponse
{
    public CreateApplicationResponse(
        string id
    )
    {
        this.Id = id;
    }

    /// <summary>
    /// Application identifier.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("id")]
    public string Id { get; }
}


#nullable restore
