#nullable enable

namespace Spp.IdentityProvider.Client.Applications.V1;


/// <summary>
/// Application list.
/// </summary>
public partial class ApplicationViewList
{
    public ApplicationViewList(
        System.Collections.Generic.IReadOnlyList<ApplicationView> items
    )
    {
        this.Items = items;
    }

    /// <summary>
    /// 
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("items")]
    public System.Collections.Generic.IReadOnlyList<ApplicationView> Items { get; }
}


#nullable restore
