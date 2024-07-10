#nullable enable

namespace Spp.Authorization.WebApi.Auth.V1;

[Microsoft.AspNetCore.Mvc.ApiController]
public abstract partial class BaseAuthController : Microsoft.AspNetCore.Mvc.ControllerBase
{
    /// <summary>
    /// Get user info.
    /// </summary>
    [Microsoft.AspNetCore.Mvc.HttpGet("/v1/auth/user-info")]
    public virtual async System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> GetUserInfoEndpoint(
        System.Threading.CancellationToken cancellationToken
    )
    {
        return await GetUserInfo(
            cancellationToken
        );
    }

    /// <summary>
    /// Sign-in with IdentityProvider.
    /// </summary>
    [Microsoft.AspNetCore.Mvc.HttpGet("/v1/auth/sign-in/identity-provider")]
    public virtual async System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> SignInWithIndentityProviderEndpoint(
        [Microsoft.AspNetCore.Mvc.FromQuery(Name = "redirect_uri")]
        [System.ComponentModel.DataAnnotations.Required]
        string redirectUri,
        System.Threading.CancellationToken cancellationToken
    )
    {
        return await SignInWithIndentityProvider(
            redirectUri,
            cancellationToken
        );
    }

    /// <summary>
    /// Get user info.
    /// </summary>
    protected abstract System.Threading.Tasks.Task<GetUserInfoActionResult> GetUserInfo(
        System.Threading.CancellationToken cancellationToken
    );
    /// <summary>
    /// Sign-in with IdentityProvider.
    /// </summary>
    protected abstract System.Threading.Tasks.Task<SignInWithIndentityProviderActionResult> SignInWithIndentityProvider(
        string redirectUri,
        System.Threading.CancellationToken cancellationToken
    );

    public readonly struct GetUserInfoActionResult : Microsoft.AspNetCore.Mvc.IActionResult
    {
        private const int Default = -1;

        private readonly Microsoft.AspNetCore.Mvc.IActionResult? _raw;

        private GetUserInfoActionResult(Microsoft.AspNetCore.Mvc.IActionResult? raw)
        {
            _raw = raw;
        }

        public async System.Threading.Tasks.Task ExecuteResultAsync(Microsoft.AspNetCore.Mvc.ActionContext context)
        {
            await (_raw ?? new Microsoft.AspNetCore.Mvc.NoContentResult()).ExecuteResultAsync(context);
        }

        public static GetUserInfoActionResult Create200(
            Microsoft.AspNetCore.Mvc.ActionResult<GetUserInfoResponse> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new GetUserInfoActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 200
            });
        }

        public static GetUserInfoActionResult Create200(
            GetUserInfoResponse result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 200
            };
            return new GetUserInfoActionResult(raw: actionResult);
        }

        public static GetUserInfoActionResult Create401(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new GetUserInfoActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 401
            });
        }

        public static GetUserInfoActionResult Create401(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 401
            };
            return new GetUserInfoActionResult(raw: actionResult);
        }

        public static GetUserInfoActionResult Create403(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new GetUserInfoActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 403
            });
        }

        public static GetUserInfoActionResult Create403(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 403
            };
            return new GetUserInfoActionResult(raw: actionResult);
        }

        public static GetUserInfoActionResult Create404(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new GetUserInfoActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 404
            });
        }

        public static GetUserInfoActionResult Create404(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 404
            };
            return new GetUserInfoActionResult(raw: actionResult);
        }

        public static GetUserInfoActionResult CreateRaw(Microsoft.AspNetCore.Mvc.IActionResult result)
        {
            return new GetUserInfoActionResult(raw: result);
        }
    }
    public readonly struct SignInWithIndentityProviderActionResult : Microsoft.AspNetCore.Mvc.IActionResult
    {
        private const int Default = -1;

        private readonly Microsoft.AspNetCore.Mvc.IActionResult? _raw;

        private SignInWithIndentityProviderActionResult(Microsoft.AspNetCore.Mvc.IActionResult? raw)
        {
            _raw = raw;
        }

        public async System.Threading.Tasks.Task ExecuteResultAsync(Microsoft.AspNetCore.Mvc.ActionContext context)
        {
            await (_raw ?? new Microsoft.AspNetCore.Mvc.NoContentResult()).ExecuteResultAsync(context);
        }

        public static SignInWithIndentityProviderActionResult Create302(
            Microsoft.AspNetCore.Mvc.Infrastructure.IStatusCodeActionResult result
        )
        {
            if (result.StatusCode != 302 && 302 != Default)
            {
                throw new System.InvalidOperationException($"Expected status code 302, but got {result.StatusCode}.");
            }

            return new SignInWithIndentityProviderActionResult(raw: result);
        }

        public static SignInWithIndentityProviderActionResult Create302(
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.StatusCodeResult(302);
            return new SignInWithIndentityProviderActionResult(raw: actionResult);
        }

        public static SignInWithIndentityProviderActionResult Create400(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new SignInWithIndentityProviderActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 400
            });
        }

        public static SignInWithIndentityProviderActionResult Create400(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 400
            };
            return new SignInWithIndentityProviderActionResult(raw: actionResult);
        }

        public static SignInWithIndentityProviderActionResult CreateDefault(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new SignInWithIndentityProviderActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = Default
            });
        }

        public static SignInWithIndentityProviderActionResult CreateDefault(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = Default
            };
            return new SignInWithIndentityProviderActionResult(raw: actionResult);
        }

        public static SignInWithIndentityProviderActionResult CreateRaw(Microsoft.AspNetCore.Mvc.IActionResult result)
        {
            return new SignInWithIndentityProviderActionResult(raw: result);
        }
    }
}

#nullable restore
