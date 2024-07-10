#nullable enable

namespace Spp.Authorization.WebApi.Users.V1;

[Microsoft.AspNetCore.Mvc.ApiController]
public abstract partial class BaseUsersController : Microsoft.AspNetCore.Mvc.ControllerBase
{
    /// <summary>
    /// Assign role to user.
    /// </summary>
    [Microsoft.AspNetCore.Mvc.HttpPut("/v1/users/{id}/roles/{roleId}")]
    public virtual async System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> AssignRoleEndpoint(
        [Microsoft.AspNetCore.Mvc.FromRoute(Name = "id")]
        [System.ComponentModel.DataAnnotations.Required]
        string id,
        [Microsoft.AspNetCore.Mvc.FromRoute(Name = "roleId")]
        [System.ComponentModel.DataAnnotations.Required]
        string roleId,
        System.Threading.CancellationToken cancellationToken
    )
    {
        return await AssignRole(
            id,
            roleId,
            cancellationToken
        );
    }

    /// <summary>
    /// Unassign role from user.
    /// </summary>
    [Microsoft.AspNetCore.Mvc.HttpDelete("/v1/users/{id}/roles/{roleId}")]
    public virtual async System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> UnassignRoleEndpoint(
        [Microsoft.AspNetCore.Mvc.FromRoute(Name = "id")]
        [System.ComponentModel.DataAnnotations.Required]
        string id,
        [Microsoft.AspNetCore.Mvc.FromRoute(Name = "roleId")]
        [System.ComponentModel.DataAnnotations.Required]
        string roleId,
        System.Threading.CancellationToken cancellationToken
    )
    {
        return await UnassignRole(
            id,
            roleId,
            cancellationToken
        );
    }

    /// <summary>
    /// Assign role to user.
    /// </summary>
    protected abstract System.Threading.Tasks.Task<AssignRoleActionResult> AssignRole(
        string id,
        string roleId,
        System.Threading.CancellationToken cancellationToken
    );
    /// <summary>
    /// Unassign role from user.
    /// </summary>
    protected abstract System.Threading.Tasks.Task<UnassignRoleActionResult> UnassignRole(
        string id,
        string roleId,
        System.Threading.CancellationToken cancellationToken
    );

    public readonly struct AssignRoleActionResult : Microsoft.AspNetCore.Mvc.IActionResult
    {
        private const int Default = -1;

        private readonly Microsoft.AspNetCore.Mvc.IActionResult? _raw;

        private AssignRoleActionResult(Microsoft.AspNetCore.Mvc.IActionResult? raw)
        {
            _raw = raw;
        }

        public async System.Threading.Tasks.Task ExecuteResultAsync(Microsoft.AspNetCore.Mvc.ActionContext context)
        {
            await (_raw ?? new Microsoft.AspNetCore.Mvc.NoContentResult()).ExecuteResultAsync(context);
        }

        public static AssignRoleActionResult Create204(
            Microsoft.AspNetCore.Mvc.Infrastructure.IStatusCodeActionResult result
        )
        {
            if (result.StatusCode != 204 && 204 != Default)
            {
                throw new System.InvalidOperationException($"Expected status code 204, but got {result.StatusCode}.");
            }

            return new AssignRoleActionResult(raw: result);
        }

        public static AssignRoleActionResult Create204(
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.StatusCodeResult(204);
            return new AssignRoleActionResult(raw: actionResult);
        }

        public static AssignRoleActionResult Create400(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new AssignRoleActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 400
            });
        }

        public static AssignRoleActionResult Create400(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 400
            };
            return new AssignRoleActionResult(raw: actionResult);
        }

        public static AssignRoleActionResult Create401(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new AssignRoleActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 401
            });
        }

        public static AssignRoleActionResult Create401(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 401
            };
            return new AssignRoleActionResult(raw: actionResult);
        }

        public static AssignRoleActionResult Create403(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new AssignRoleActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 403
            });
        }

        public static AssignRoleActionResult Create403(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 403
            };
            return new AssignRoleActionResult(raw: actionResult);
        }

        public static AssignRoleActionResult Create404(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new AssignRoleActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 404
            });
        }

        public static AssignRoleActionResult Create404(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 404
            };
            return new AssignRoleActionResult(raw: actionResult);
        }

        public static AssignRoleActionResult CreateDefault(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new AssignRoleActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = Default
            });
        }

        public static AssignRoleActionResult CreateDefault(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = Default
            };
            return new AssignRoleActionResult(raw: actionResult);
        }

        public static AssignRoleActionResult CreateRaw(Microsoft.AspNetCore.Mvc.IActionResult result)
        {
            return new AssignRoleActionResult(raw: result);
        }
    }
    public readonly struct UnassignRoleActionResult : Microsoft.AspNetCore.Mvc.IActionResult
    {
        private const int Default = -1;

        private readonly Microsoft.AspNetCore.Mvc.IActionResult? _raw;

        private UnassignRoleActionResult(Microsoft.AspNetCore.Mvc.IActionResult? raw)
        {
            _raw = raw;
        }

        public async System.Threading.Tasks.Task ExecuteResultAsync(Microsoft.AspNetCore.Mvc.ActionContext context)
        {
            await (_raw ?? new Microsoft.AspNetCore.Mvc.NoContentResult()).ExecuteResultAsync(context);
        }

        public static UnassignRoleActionResult Create204(
            Microsoft.AspNetCore.Mvc.Infrastructure.IStatusCodeActionResult result
        )
        {
            if (result.StatusCode != 204 && 204 != Default)
            {
                throw new System.InvalidOperationException($"Expected status code 204, but got {result.StatusCode}.");
            }

            return new UnassignRoleActionResult(raw: result);
        }

        public static UnassignRoleActionResult Create204(
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.StatusCodeResult(204);
            return new UnassignRoleActionResult(raw: actionResult);
        }

        public static UnassignRoleActionResult Create400(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new UnassignRoleActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 400
            });
        }

        public static UnassignRoleActionResult Create400(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 400
            };
            return new UnassignRoleActionResult(raw: actionResult);
        }

        public static UnassignRoleActionResult Create401(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new UnassignRoleActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 401
            });
        }

        public static UnassignRoleActionResult Create401(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 401
            };
            return new UnassignRoleActionResult(raw: actionResult);
        }

        public static UnassignRoleActionResult Create403(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new UnassignRoleActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 403
            });
        }

        public static UnassignRoleActionResult Create403(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 403
            };
            return new UnassignRoleActionResult(raw: actionResult);
        }

        public static UnassignRoleActionResult Create404(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new UnassignRoleActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 404
            });
        }

        public static UnassignRoleActionResult Create404(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 404
            };
            return new UnassignRoleActionResult(raw: actionResult);
        }

        public static UnassignRoleActionResult CreateDefault(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new UnassignRoleActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = Default
            });
        }

        public static UnassignRoleActionResult CreateDefault(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = Default
            };
            return new UnassignRoleActionResult(raw: actionResult);
        }

        public static UnassignRoleActionResult CreateRaw(Microsoft.AspNetCore.Mvc.IActionResult result)
        {
            return new UnassignRoleActionResult(raw: result);
        }
    }
}

#nullable restore
