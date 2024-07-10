namespace Spp.IdentityProvider.Application.Users.Errors;

public readonly struct DuplicateUsernameError(string username)
{
    public string Username { get; } = username;
}
