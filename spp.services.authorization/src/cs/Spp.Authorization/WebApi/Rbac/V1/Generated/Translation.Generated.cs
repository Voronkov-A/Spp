#nullable enable

namespace Spp.Authorization.WebApi.Rbac.V1;


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
    public string Language { get; }
    /// <summary>
    /// Value.
    /// </summary>
    public string Value { get; }
}


#nullable restore
