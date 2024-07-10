#nullable enable

namespace Spp.IdentityProvider.WebApi.Applications.V1;


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
    public System.Collections.Generic.IReadOnlyList<ApplicationView> Items { get; }
}


#nullable restore
