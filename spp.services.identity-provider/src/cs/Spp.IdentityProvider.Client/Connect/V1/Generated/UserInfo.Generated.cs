#nullable enable

namespace Spp.IdentityProvider.Client.Connect.V1;


/// <summary>
/// User info.
/// </summary>
public partial class UserInfo
{
    public UserInfo(
        string? sub,
        string? name,
        string? givenName,
        string? familyName,
        string? preferredUsername,
        string? email,
        string? picture
    )
    {
        this.Sub = sub;
        this.Name = name;
        this.GivenName = givenName;
        this.FamilyName = familyName;
        this.PreferredUsername = preferredUsername;
        this.Email = email;
        this.Picture = picture;
    }

    /// <summary>
    /// 
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("sub")]
    public string? Sub { get; }
    /// <summary>
    /// 
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("name")]
    public string? Name { get; }
    /// <summary>
    /// 
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("given_name")]
    public string? GivenName { get; }
    /// <summary>
    /// 
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("family_name")]
    public string? FamilyName { get; }
    /// <summary>
    /// 
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("preferred_username")]
    public string? PreferredUsername { get; }
    /// <summary>
    /// 
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("email")]
    public string? Email { get; }
    /// <summary>
    /// 
    /// </summary>
    [System.Text.Json.Serialization.JsonPropertyName("picture")]
    public string? Picture { get; }
}


#nullable restore
