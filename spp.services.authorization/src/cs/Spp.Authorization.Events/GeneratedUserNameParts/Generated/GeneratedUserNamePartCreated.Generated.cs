#nullable enable

namespace Spp.Authorization.Events.GeneratedUserNameParts;


/// <summary>
/// Generated user name part created.
/// </summary>
public partial class GeneratedUserNamePartCreated
{
    public GeneratedUserNamePartCreated(
        string value,
        GeneratedUserNamePartType type
    )
    {
        this.Value = value;
        this.Type = type;
    }

    /// <summary>
    /// Value.
    /// </summary>
    public string Value { get; }
    /// <summary>
    /// Type.
    /// </summary>
    public GeneratedUserNamePartType Type { get; }
}


#nullable restore
