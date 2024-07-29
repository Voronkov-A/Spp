using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Spp.IdentityProvider.Domain.Users;
using Microsoft.AspNetCore.Authentication;
using Spp.Common.Errors;

namespace Spp.IdentityProvider.WebApi.Auth.V1;

public class AuthController(
    ICommonErrorFactory commonErrorFactory,
    UserManager<User> userManager,
    SignInManager<User> signInManager) :
    BaseAuthController
{
    private readonly ICommonErrorFactory _commonErrorFactory = commonErrorFactory;
    private readonly UserManager<User> _userManager = userManager;
    private readonly SignInManager<User> _signInManager = signInManager;

    protected override Task<CallbackActionResult> Callback(
        string code,
        string scope,
        string iss,
        CancellationToken cancellationToken)
    {
        var result = CallbackActionResult.Create200(new AuthorizationCallbackParameters(
            code: code,
            scope: scope,
            iss: iss));
        return Task.FromResult(result);
    }

    protected override async Task<SignInActionResult> SignIn(
        SignInRequest signInRequest,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(signInRequest.Username);

        if (user == null)
        {
            return SignInActionResult.Create401(_commonErrorFactory.AuthenticationFailure(
                "The provided credentials are invalid."));
        }

        var signInResult = await _signInManager.CheckPasswordSignInAsync(
            user,
            signInRequest.Password,
            lockoutOnFailure: false);
        var principal = await _signInManager.CreateUserPrincipalAsync(user);

        if (!signInResult.Succeeded)
        {
            return SignInActionResult.Create401(_commonErrorFactory.AuthenticationFailure(
                "The provided credentials are invalid."));
        }

        var authenticationProperties = new AuthenticationProperties();
        authenticationProperties.SetScope(signInRequest.Scopes);
        await HttpContext.SignInAsync(principal, authenticationProperties);
        return SignInActionResult.Create204();
    }
}
