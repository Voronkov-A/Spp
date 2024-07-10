#nullable enable

namespace Spp.Authorization.WebApi.Rbac.V1;


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
    public string @Default { get; }
    /// <summary>
    /// Translations.
    /// </summary>
    public System.Collections.Generic.IReadOnlyList<Translation> Translations { get; }
}


#nullable restore
