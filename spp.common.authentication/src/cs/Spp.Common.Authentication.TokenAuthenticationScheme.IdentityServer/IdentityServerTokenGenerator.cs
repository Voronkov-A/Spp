using Duende.IdentityServer.ResponseHandling;
using Duende.IdentityServer.Stores;
using Duende.IdentityServer.Validation;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Common.Authentication.TokenAuthenticationScheme.IdentityServer;

public class IdentityServerTokenGenerator(
    ITokenResponseGenerator tokenResponseGenerator,
    ITokenRequestValidator tokenRequestValidator,
    IClientStore clients,
    IAuthorizationCodeStore authorizationCodeStore,
    TimeProvider timeProvider,
    IOptions<IdentityServerTokenGeneratorOptions> options) :
    ITokenGenerator
{
    private readonly AuthorizationCode _dummyAuthorizationCode = new();

    public async Task<TokenGenerationResult> GenerateTokens(
        ClaimsPrincipal user,
        AuthenticationProperties? properties,
        CancellationToken cancellationToken)
    {
        var settings = options.Value;

        var client = await clients.FindEnabledClientByIdAsync(settings.ClientId)
            ?? throw new InvalidOperationException($"Could not find default client {settings.ClientId}.");

        if (properties == null
            || !properties.Items.TryGetValue(settings.ScopePropertyKey, out var scope)
            || scope == null)
        {
            scope = settings.DefaultScope;
        }

        const string redirectUri = "http://localhost";

        var result = await authorizationCodeStore.StoreAuthorizationCodeAsync(new Duende.IdentityServer.Models.AuthorizationCode
        {
            ClientId = client.ClientId,
            RedirectUri = redirectUri,
            CodeChallenge = _dummyAuthorizationCode.HashedChallenge,
            CodeChallengeMethod = OidcConstants.CodeChallengeMethods.Sha256,
            RequestedScopes = scope.Split(' ', StringSplitOptions.RemoveEmptyEntries),
            Subject = user,
            Lifetime = 60, // seconds
            CreationTime = timeProvider.GetUtcNow().UtcDateTime
        });

        var req = await tokenRequestValidator.ValidateRequestAsync(
            new TokenRequestValidationContext
            {
                RequestParameters = new System.Collections.Specialized.NameValueCollection
                {
                    [OidcConstants.TokenRequest.GrantType] = OidcConstants.GrantTypes.AuthorizationCode,
                    [OidcConstants.TokenRequest.ClientId] = client.ClientId,
                    [OidcConstants.TokenRequest.ClientSecret] = settings.ClientSecret,
                    [OidcConstants.TokenRequest.Scope] = scope,
                    [OidcConstants.TokenRequest.Code] = result,
                    [OidcConstants.TokenRequest.CodeVerifier] = _dummyAuthorizationCode.Verifier,
                    [OidcConstants.TokenRequest.RedirectUri] = redirectUri
                },
                ClientValidationResult = new ClientSecretValidationResult
                {
                    Client = client
                }
            });

        var tokenData = await tokenResponseGenerator.ProcessAsync(req);
        return new TokenGenerationResult(tokenData.AccessToken, tokenData.RefreshToken);
    }
}
