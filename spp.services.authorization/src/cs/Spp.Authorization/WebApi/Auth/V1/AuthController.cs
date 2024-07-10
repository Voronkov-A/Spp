using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spp.Authorization.Domain.Users;
using Spp.Authorization.WebApi.Rbac.Authorization;
using Spp.Common.Authorization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Spp.Authorization.WebApi.Auth.V1;

public class AuthController : BaseAuthController
{
    [Authorize(AuthorizationPolicies.UserOnly)]
    public override async Task<IActionResult> GetUserInfoEndpoint(CancellationToken cancellationToken)
    {
        return await base.GetUserInfoEndpoint(cancellationToken);
    }

    protected override Task<GetUserInfoActionResult> GetUserInfo(CancellationToken cancellationToken)
    {
        var id = User.Claims.First(x => x.Type == UserIdClaim.Type).Value;
        var response = new GetUserInfoResponse(id);
        var result = GetUserInfoActionResult.Create200(response);
        return Task.FromResult(result);
    }

    protected override Task<SignInWithIndentityProviderActionResult> SignInWithIndentityProvider(
        string redirectUri,
        CancellationToken cancellationToken)
    {
        var authenticationProperties = new AuthenticationProperties
        {
            RedirectUri = redirectUri
        };
        authenticationProperties.SetScope(Scopes.All);
        authenticationProperties.SetProviderId(UserIdentityProviders.IdentityProviderId);
        var challengeResult = Challenge(authenticationProperties, OpenIdConnectDefaults.AuthenticationScheme);
        var result = SignInWithIndentityProviderActionResult.CreateRaw(challengeResult);
        return Task.FromResult(result);
    }
}
