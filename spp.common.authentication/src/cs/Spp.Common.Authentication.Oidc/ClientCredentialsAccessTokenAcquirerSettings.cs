namespace Spp.Common.Authentication.Oidc;

public class ClientCredentialsAccessTokenAcquirerSettings
{
    public required string ClientId { get; init; }

    public required string ClientSecret { get; init; }

    public required string Scope { get; init; }
}

public class ClientCredentialsAccessTokenAcquirerSettings<T> : ClientCredentialsAccessTokenAcquirerSettings;
