#nullable enable

namespace Spp.Authorization.TestClient.Auth.V1;


/// <summary>
/// User info.
/// </summary>
public partial class GetUserInfoResponse
{
    public GetUserInfoResponse(
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
