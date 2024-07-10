#nullable enable

namespace Spp.Authorization.WebApi.Common.V1;


/// <summary>
/// Parameters for authorization.invalidName error code.
/// </summary>
public partial class InvalidNameErrorParameters
{
    public InvalidNameErrorParameters(
        string name
    )
    {
        this.Name = name;
    }

    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; }
}


#nullable restore
