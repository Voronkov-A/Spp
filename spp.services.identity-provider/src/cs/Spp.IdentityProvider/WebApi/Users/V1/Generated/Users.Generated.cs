#nullable enable

namespace Spp.IdentityProvider.WebApi.Users.V1;

[Microsoft.AspNetCore.Mvc.ApiController]
public abstract partial class BaseUsersController : Microsoft.AspNetCore.Mvc.ControllerBase
{
    /// <summary>
    /// Create user.
    /// </summary>
    [Microsoft.AspNetCore.Mvc.HttpPost("/identity-provider/v1/users")]
    public virtual async System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> CreateEndpoint(
        [Microsoft.AspNetCore.Mvc.FromBody]
        [System.ComponentModel.DataAnnotations.Required]
        CreateUserRequest createUserRequest,
        System.Threading.CancellationToken cancellationToken
    )
    {
        return await Create(
            createUserRequest,
            cancellationToken
        );
    }

    /// <summary>
    /// Create user.
    /// </summary>
    protected abstract System.Threading.Tasks.Task<CreateActionResult> Create(
        CreateUserRequest createUserRequest,
        System.Threading.CancellationToken cancellationToken
    );

    public readonly struct CreateActionResult : Microsoft.AspNetCore.Mvc.IActionResult
    {
        private const int Default = -1;

        private readonly Microsoft.AspNetCore.Mvc.IActionResult? _raw;

        private CreateActionResult(Microsoft.AspNetCore.Mvc.IActionResult? raw)
        {
            _raw = raw;
        }

        public async System.Threading.Tasks.Task ExecuteResultAsync(Microsoft.AspNetCore.Mvc.ActionContext context)
        {
            await (_raw ?? new Microsoft.AspNetCore.Mvc.NoContentResult()).ExecuteResultAsync(context);
        }

        public static CreateActionResult Create201(
            Microsoft.AspNetCore.Mvc.ActionResult<CreateUserResponse> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new CreateActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 201
            });
        }

        public static CreateActionResult Create201(
            CreateUserResponse result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 201
            };
            return new CreateActionResult(raw: actionResult);
        }

        public static CreateActionResult Create400(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new CreateActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 400
            });
        }

        public static CreateActionResult Create400(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 400
            };
            return new CreateActionResult(raw: actionResult);
        }

        public static CreateActionResult Create401(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new CreateActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 401
            });
        }

        public static CreateActionResult Create401(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 401
            };
            return new CreateActionResult(raw: actionResult);
        }

        public static CreateActionResult Create403(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new CreateActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 403
            });
        }

        public static CreateActionResult Create403(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 403
            };
            return new CreateActionResult(raw: actionResult);
        }

        public static CreateActionResult CreateDefault(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new CreateActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = Default
            });
        }

        public static CreateActionResult CreateDefault(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = Default
            };
            return new CreateActionResult(raw: actionResult);
        }

        public static CreateActionResult CreateRaw(Microsoft.AspNetCore.Mvc.IActionResult result)
        {
            return new CreateActionResult(raw: result);
        }
    }
}

#nullable restore
