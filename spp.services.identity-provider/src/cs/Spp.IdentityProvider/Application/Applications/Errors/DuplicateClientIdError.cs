namespace Spp.IdentityProvider.Application.Applications.Errors;

public readonly struct DuplicateClientIdError(string clientId)
{
    public string ClientId { get; } = clientId;
}
