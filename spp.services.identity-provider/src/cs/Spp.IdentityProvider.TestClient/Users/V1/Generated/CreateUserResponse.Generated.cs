#nullable enable

namespace Spp.IdentityProvider.TestClient.Users.V1;


/// <summary>
/// Create user response.
/// </summary>
public partial class CreateUserResponse
{
    public CreateUserResponse(
        string id
    )
    {
        this.Id = id;
    }

    /// <summary>
    /// User identifier.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("id")]
    public string Id { get; }
}


#nullable restore
