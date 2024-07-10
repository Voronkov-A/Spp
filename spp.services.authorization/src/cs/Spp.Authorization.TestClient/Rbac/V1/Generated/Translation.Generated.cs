#nullable enable

namespace Spp.Authorization.TestClient.Rbac.V1;


/// <summary>
/// Translation.
/// </summary>
public partial class Translation
{
    public Translation(
        string language,
        string value
    )
    {
        this.Language = language;
        this.Value = value;
    }

    /// <summary>
    /// Language.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("language")]
    public string Language { get; }
    /// <summary>
    /// Value.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("value")]
    public string Value { get; }
}


#nullable restore
