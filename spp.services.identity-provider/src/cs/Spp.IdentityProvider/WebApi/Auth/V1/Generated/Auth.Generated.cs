#nullable enable

namespace Spp.IdentityProvider.WebApi.Auth.V1;

[Microsoft.AspNetCore.Mvc.ApiController]
public abstract partial class BaseAuthController : Microsoft.AspNetCore.Mvc.ControllerBase
{
    /// <summary>
    /// Authorization callback for debug purposes.
    /// </summary>
    [Microsoft.AspNetCore.Mvc.HttpGet("/v1/auth/callback")]
    public virtual async System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> CallbackEndpoint(
        [Microsoft.AspNetCore.Mvc.FromQuery(Name = "code")]
        [System.ComponentModel.DataAnnotations.Required]
        string code,
        [Microsoft.AspNetCore.Mvc.FromQuery(Name = "scope")]
        [System.ComponentModel.DataAnnotations.Required]
        string scope,
        [Microsoft.AspNetCore.Mvc.FromQuery(Name = "session_state")]
        [System.ComponentModel.DataAnnotations.Required]
        string sessionState,
        [Microsoft.AspNetCore.Mvc.FromQuery(Name = "iss")]
        [System.ComponentModel.DataAnnotations.Required]
        string iss,
        System.Threading.CancellationToken cancellationToken
    )
    {
        return await Callback(
            code,
            scope,
            sessionState,
            iss,
            cancellationToken
        );
    }

    /// <summary>
    /// Sign in.
    /// </summary>
    [Microsoft.AspNetCore.Mvc.HttpPost("/v1/auth/sign-in")]
    public virtual async System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> SignInEndpoint(
        [Microsoft.AspNetCore.Mvc.FromBody]
        [System.ComponentModel.DataAnnotations.Required]
        SignInRequest signInRequest,
        System.Threading.CancellationToken cancellationToken
    )
    {
        return await SignIn(
            signInRequest,
            cancellationToken
        );
    }

    /// <summary>
    /// Authorization callback for debug purposes.
    /// </summary>
    protected abstract System.Threading.Tasks.Task<CallbackActionResult> Callback(
        string code,
        string scope,
        string sessionState,
        string iss,
        System.Threading.CancellationToken cancellationToken
    );
    /// <summary>
    /// Sign in.
    /// </summary>
    protected abstract System.Threading.Tasks.Task<SignInActionResult> SignIn(
        SignInRequest signInRequest,
        System.Threading.CancellationToken cancellationToken
    );

    public readonly struct CallbackActionResult : Microsoft.AspNetCore.Mvc.IActionResult
    {
        private const int Default = -1;

        private readonly Microsoft.AspNetCore.Mvc.IActionResult? _raw;

        private CallbackActionResult(Microsoft.AspNetCore.Mvc.IActionResult? raw)
        {
            _raw = raw;
        }

        public async System.Threading.Tasks.Task ExecuteResultAsync(Microsoft.AspNetCore.Mvc.ActionContext context)
        {
            await (_raw ?? new Microsoft.AspNetCore.Mvc.NoContentResult()).ExecuteResultAsync(context);
        }

        public static CallbackActionResult Create200(
            Microsoft.AspNetCore.Mvc.ActionResult<AuthorizationCallbackParameters> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new CallbackActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 200
            });
        }

        public static CallbackActionResult Create200(
            AuthorizationCallbackParameters result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 200
            };
            return new CallbackActionResult(raw: actionResult);
        }

        public static CallbackActionResult Create400(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new CallbackActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 400
            });
        }

        public static CallbackActionResult Create400(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 400
            };
            return new CallbackActionResult(raw: actionResult);
        }

        public static CallbackActionResult CreateDefault(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new CallbackActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = Default
            });
        }

        public static CallbackActionResult CreateDefault(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = Default
            };
            return new CallbackActionResult(raw: actionResult);
        }

        public static CallbackActionResult CreateRaw(Microsoft.AspNetCore.Mvc.IActionResult result)
        {
            return new CallbackActionResult(raw: result);
        }
    }
    public readonly struct SignInActionResult : Microsoft.AspNetCore.Mvc.IActionResult
    {
        private const int Default = -1;

        private readonly Microsoft.AspNetCore.Mvc.IActionResult? _raw;

        private SignInActionResult(Microsoft.AspNetCore.Mvc.IActionResult? raw)
        {
            _raw = raw;
        }

        public async System.Threading.Tasks.Task ExecuteResultAsync(Microsoft.AspNetCore.Mvc.ActionContext context)
        {
            await (_raw ?? new Microsoft.AspNetCore.Mvc.NoContentResult()).ExecuteResultAsync(context);
        }

        public static SignInActionResult Create204(
            Microsoft.AspNetCore.Mvc.Infrastructure.IStatusCodeActionResult result
        )
        {
            if (result.StatusCode != 204 && 204 != Default)
            {
                throw new System.InvalidOperationException($"Expected status code 204, but got {result.StatusCode}.");
            }

            return new SignInActionResult(raw: result);
        }

        public static SignInActionResult Create204(
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.StatusCodeResult(204);
            return new SignInActionResult(raw: actionResult);
        }

        public static SignInActionResult Create400(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new SignInActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 400
            });
        }

        public static SignInActionResult Create400(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 400
            };
            return new SignInActionResult(raw: actionResult);
        }

        public static SignInActionResult Create401(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new SignInActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 401
            });
        }

        public static SignInActionResult Create401(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 401
            };
            return new SignInActionResult(raw: actionResult);
        }

        public static SignInActionResult CreateDefault(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new SignInActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = Default
            });
        }

        public static SignInActionResult CreateDefault(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = Default
            };
            return new SignInActionResult(raw: actionResult);
        }

        public static SignInActionResult CreateRaw(Microsoft.AspNetCore.Mvc.IActionResult result)
        {
            return new SignInActionResult(raw: result);
        }
    }
}

#nullable restore
