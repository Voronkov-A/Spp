#nullable enable

namespace Spp.Authorization.Client.Auth.V1;


/// <summary>
/// Localized name.
/// </summary>
public partial class LocalizedName
{
    public LocalizedName(
        string @default,
        System.Collections.Generic.IReadOnlyList<Translation> translations
    )
    {
        this.@Default = @default;
        this.Translations = translations;
    }

    /// <summary>
    /// Default name.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("default")]
    public string @Default { get; }
    /// <summary>
    /// Translations.
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("translations")]
    public System.Collections.Generic.IReadOnlyList<Translation> Translations { get; }
}


#nullable restore
