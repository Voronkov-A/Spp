#nullable enable

namespace Spp.Authorization.Events.Users;


/// <summary>
/// User identity.
/// </summary>
public partial class UserIdentity
{
    public UserIdentity(
        string providerId,
        string subjectId
    )
    {
        this.ProviderId = providerId;
        this.SubjectId = subjectId;
    }

    /// <summary>
    /// 
    /// </summary>
    public string ProviderId { get; }
    /// <summary>
    /// 
    /// </summary>
    public string SubjectId { get; }
}


#nullable restore
