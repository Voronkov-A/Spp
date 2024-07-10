#nullable enable

namespace Spp.Authorization.WebApi.Rbac.V1;

[Microsoft.AspNetCore.Mvc.ApiController]
public abstract partial class BaseRbacController : Microsoft.AspNetCore.Mvc.ControllerBase
{
    /// <summary>
    /// Create or update permission group.
    /// </summary>
    [Microsoft.AspNetCore.Mvc.HttpPut("/v1/rbac/permission-groups/{id}")]
    public virtual async System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> CreateOrUpdatePermissionGroupEndpoint(
        [Microsoft.AspNetCore.Mvc.FromRoute(Name = "id")]
        [System.ComponentModel.DataAnnotations.Required]
        string id,
        [Microsoft.AspNetCore.Mvc.FromBody]
        [System.ComponentModel.DataAnnotations.Required]
        CreateOrUpdatePermissionGroupRequest createOrUpdatePermissionGroupRequest,
        System.Threading.CancellationToken cancellationToken
    )
    {
        return await CreateOrUpdatePermissionGroup(
            id,
            createOrUpdatePermissionGroupRequest,
            cancellationToken
        );
    }

    /// <summary>
    /// Create role.
    /// </summary>
    [Microsoft.AspNetCore.Mvc.HttpPost("/v1/rbac/roles")]
    public virtual async System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> CreateRoleEndpoint(
        [Microsoft.AspNetCore.Mvc.FromBody]
        [System.ComponentModel.DataAnnotations.Required]
        CreateRoleRequest createRoleRequest,
        System.Threading.CancellationToken cancellationToken
    )
    {
        return await CreateRole(
            createRoleRequest,
            cancellationToken
        );
    }

    /// <summary>
    /// Delete role.
    /// </summary>
    [Microsoft.AspNetCore.Mvc.HttpDelete("/v1/rbac/roles/{id}")]
    public virtual async System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> DeleteRoleEndpoint(
        [Microsoft.AspNetCore.Mvc.FromRoute(Name = "id")]
        [System.ComponentModel.DataAnnotations.Required]
        string id,
        System.Threading.CancellationToken cancellationToken
    )
    {
        return await DeleteRole(
            id,
            cancellationToken
        );
    }

    /// <summary>
    /// Create or update permission group.
    /// </summary>
    protected abstract System.Threading.Tasks.Task<CreateOrUpdatePermissionGroupActionResult> CreateOrUpdatePermissionGroup(
        string id,
        CreateOrUpdatePermissionGroupRequest createOrUpdatePermissionGroupRequest,
        System.Threading.CancellationToken cancellationToken
    );
    /// <summary>
    /// Create role.
    /// </summary>
    protected abstract System.Threading.Tasks.Task<CreateRoleActionResult> CreateRole(
        CreateRoleRequest createRoleRequest,
        System.Threading.CancellationToken cancellationToken
    );
    /// <summary>
    /// Delete role.
    /// </summary>
    protected abstract System.Threading.Tasks.Task<DeleteRoleActionResult> DeleteRole(
        string id,
        System.Threading.CancellationToken cancellationToken
    );

    public readonly struct CreateOrUpdatePermissionGroupActionResult : Microsoft.AspNetCore.Mvc.IActionResult
    {
        private const int Default = -1;

        private readonly Microsoft.AspNetCore.Mvc.IActionResult? _raw;

        private CreateOrUpdatePermissionGroupActionResult(Microsoft.AspNetCore.Mvc.IActionResult? raw)
        {
            _raw = raw;
        }

        public async System.Threading.Tasks.Task ExecuteResultAsync(Microsoft.AspNetCore.Mvc.ActionContext context)
        {
            await (_raw ?? new Microsoft.AspNetCore.Mvc.NoContentResult()).ExecuteResultAsync(context);
        }

        public static CreateOrUpdatePermissionGroupActionResult Create204(
            Microsoft.AspNetCore.Mvc.Infrastructure.IStatusCodeActionResult result
        )
        {
            if (result.StatusCode != 204 && 204 != Default)
            {
                throw new System.InvalidOperationException($"Expected status code 204, but got {result.StatusCode}.");
            }

            return new CreateOrUpdatePermissionGroupActionResult(raw: result);
        }

        public static CreateOrUpdatePermissionGroupActionResult Create204(
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.StatusCodeResult(204);
            return new CreateOrUpdatePermissionGroupActionResult(raw: actionResult);
        }

        public static CreateOrUpdatePermissionGroupActionResult Create400(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new CreateOrUpdatePermissionGroupActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 400
            });
        }

        public static CreateOrUpdatePermissionGroupActionResult Create400(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 400
            };
            return new CreateOrUpdatePermissionGroupActionResult(raw: actionResult);
        }

        public static CreateOrUpdatePermissionGroupActionResult Create401(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new CreateOrUpdatePermissionGroupActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 401
            });
        }

        public static CreateOrUpdatePermissionGroupActionResult Create401(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 401
            };
            return new CreateOrUpdatePermissionGroupActionResult(raw: actionResult);
        }

        public static CreateOrUpdatePermissionGroupActionResult Create403(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new CreateOrUpdatePermissionGroupActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 403
            });
        }

        public static CreateOrUpdatePermissionGroupActionResult Create403(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 403
            };
            return new CreateOrUpdatePermissionGroupActionResult(raw: actionResult);
        }

        public static CreateOrUpdatePermissionGroupActionResult CreateDefault(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new CreateOrUpdatePermissionGroupActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = Default
            });
        }

        public static CreateOrUpdatePermissionGroupActionResult CreateDefault(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = Default
            };
            return new CreateOrUpdatePermissionGroupActionResult(raw: actionResult);
        }

        public static CreateOrUpdatePermissionGroupActionResult CreateRaw(Microsoft.AspNetCore.Mvc.IActionResult result)
        {
            return new CreateOrUpdatePermissionGroupActionResult(raw: result);
        }
    }
    public readonly struct CreateRoleActionResult : Microsoft.AspNetCore.Mvc.IActionResult
    {
        private const int Default = -1;

        private readonly Microsoft.AspNetCore.Mvc.IActionResult? _raw;

        private CreateRoleActionResult(Microsoft.AspNetCore.Mvc.IActionResult? raw)
        {
            _raw = raw;
        }

        public async System.Threading.Tasks.Task ExecuteResultAsync(Microsoft.AspNetCore.Mvc.ActionContext context)
        {
            await (_raw ?? new Microsoft.AspNetCore.Mvc.NoContentResult()).ExecuteResultAsync(context);
        }

        public static CreateRoleActionResult Create201(
            Microsoft.AspNetCore.Mvc.ActionResult<CreateRoleResponse> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new CreateRoleActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 201
            });
        }

        public static CreateRoleActionResult Create201(
            CreateRoleResponse result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 201
            };
            return new CreateRoleActionResult(raw: actionResult);
        }

        public static CreateRoleActionResult Create400(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new CreateRoleActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 400
            });
        }

        public static CreateRoleActionResult Create400(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 400
            };
            return new CreateRoleActionResult(raw: actionResult);
        }

        public static CreateRoleActionResult Create401(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new CreateRoleActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 401
            });
        }

        public static CreateRoleActionResult Create401(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 401
            };
            return new CreateRoleActionResult(raw: actionResult);
        }

        public static CreateRoleActionResult Create403(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new CreateRoleActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 403
            });
        }

        public static CreateRoleActionResult Create403(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 403
            };
            return new CreateRoleActionResult(raw: actionResult);
        }

        public static CreateRoleActionResult CreateDefault(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new CreateRoleActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = Default
            });
        }

        public static CreateRoleActionResult CreateDefault(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = Default
            };
            return new CreateRoleActionResult(raw: actionResult);
        }

        public static CreateRoleActionResult CreateRaw(Microsoft.AspNetCore.Mvc.IActionResult result)
        {
            return new CreateRoleActionResult(raw: result);
        }
    }
    public readonly struct DeleteRoleActionResult : Microsoft.AspNetCore.Mvc.IActionResult
    {
        private const int Default = -1;

        private readonly Microsoft.AspNetCore.Mvc.IActionResult? _raw;

        private DeleteRoleActionResult(Microsoft.AspNetCore.Mvc.IActionResult? raw)
        {
            _raw = raw;
        }

        public async System.Threading.Tasks.Task ExecuteResultAsync(Microsoft.AspNetCore.Mvc.ActionContext context)
        {
            await (_raw ?? new Microsoft.AspNetCore.Mvc.NoContentResult()).ExecuteResultAsync(context);
        }

        public static DeleteRoleActionResult Create204(
            Microsoft.AspNetCore.Mvc.Infrastructure.IStatusCodeActionResult result
        )
        {
            if (result.StatusCode != 204 && 204 != Default)
            {
                throw new System.InvalidOperationException($"Expected status code 204, but got {result.StatusCode}.");
            }

            return new DeleteRoleActionResult(raw: result);
        }

        public static DeleteRoleActionResult Create204(
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.StatusCodeResult(204);
            return new DeleteRoleActionResult(raw: actionResult);
        }

        public static DeleteRoleActionResult Create400(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new DeleteRoleActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 400
            });
        }

        public static DeleteRoleActionResult Create400(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 400
            };
            return new DeleteRoleActionResult(raw: actionResult);
        }

        public static DeleteRoleActionResult Create401(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new DeleteRoleActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 401
            });
        }

        public static DeleteRoleActionResult Create401(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 401
            };
            return new DeleteRoleActionResult(raw: actionResult);
        }

        public static DeleteRoleActionResult Create403(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new DeleteRoleActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 403
            });
        }

        public static DeleteRoleActionResult Create403(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 403
            };
            return new DeleteRoleActionResult(raw: actionResult);
        }

        public static DeleteRoleActionResult Create404(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new DeleteRoleActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 404
            });
        }

        public static DeleteRoleActionResult Create404(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 404
            };
            return new DeleteRoleActionResult(raw: actionResult);
        }

        public static DeleteRoleActionResult CreateDefault(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new DeleteRoleActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = Default
            });
        }

        public static DeleteRoleActionResult CreateDefault(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = Default
            };
            return new DeleteRoleActionResult(raw: actionResult);
        }

        public static DeleteRoleActionResult CreateRaw(Microsoft.AspNetCore.Mvc.IActionResult result)
        {
            return new DeleteRoleActionResult(raw: result);
        }
    }
}

#nullable restore
