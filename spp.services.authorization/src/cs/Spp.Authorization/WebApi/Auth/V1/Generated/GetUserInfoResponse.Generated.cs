#nullable enable

namespace Spp.Authorization.WebApi.Auth.V1;


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
    public string Id { get; }
}


#nullable restore
