namespace Spp.IdentityProvider.Application.Users.Settings;

public class DefaultUserSettings
{
    public required string Username { get; init; }

    public required string Password { get; init; }

    public string? DefaultId { get; init; }
}
