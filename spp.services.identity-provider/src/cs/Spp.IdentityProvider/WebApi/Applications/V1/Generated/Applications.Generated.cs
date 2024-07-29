#nullable enable

namespace Spp.IdentityProvider.WebApi.Applications.V1;

[Microsoft.AspNetCore.Mvc.ApiController]
public abstract partial class BaseApplicationsController : Microsoft.AspNetCore.Mvc.ControllerBase
{
    /// <summary>
    /// Create application.
    /// </summary>
    [Microsoft.AspNetCore.Mvc.HttpPost("/identity-provider/v1/applications")]
    public virtual async System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> CreateEndpoint(
        [Microsoft.AspNetCore.Mvc.FromBody]
        [System.ComponentModel.DataAnnotations.Required]
        CreateApplicationRequest createApplicationRequest,
        System.Threading.CancellationToken cancellationToken
    )
    {
        return await Create(
            createApplicationRequest,
            cancellationToken
        );
    }

    /// <summary>
    /// List applications.
    /// </summary>
    [Microsoft.AspNetCore.Mvc.HttpGet("/identity-provider/v1/applications")]
    public virtual async System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> ListEndpoint(
        [Microsoft.AspNetCore.Mvc.FromQuery(Name = "client_id")]
        [System.ComponentModel.DataAnnotations.Required]
        string clientId,
        System.Threading.CancellationToken cancellationToken
    )
    {
        return await List(
            clientId,
            cancellationToken
        );
    }

    /// <summary>
    /// Update application.
    /// </summary>
    [Microsoft.AspNetCore.Mvc.HttpPut("/v1/applications/{id}")]
    public virtual async System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> UpdateEndpoint(
        [Microsoft.AspNetCore.Mvc.FromRoute(Name = "id")]
        [System.ComponentModel.DataAnnotations.Required]
        string id,
        [Microsoft.AspNetCore.Mvc.FromBody]
        [System.ComponentModel.DataAnnotations.Required]
        UpdateApplicationRequest updateApplicationRequest,
        System.Threading.CancellationToken cancellationToken
    )
    {
        return await Update(
            id,
            updateApplicationRequest,
            cancellationToken
        );
    }

    /// <summary>
    /// Create application.
    /// </summary>
    protected abstract System.Threading.Tasks.Task<CreateActionResult> Create(
        CreateApplicationRequest createApplicationRequest,
        System.Threading.CancellationToken cancellationToken
    );
    /// <summary>
    /// List applications.
    /// </summary>
    protected abstract System.Threading.Tasks.Task<ListActionResult> List(
        string clientId,
        System.Threading.CancellationToken cancellationToken
    );
    /// <summary>
    /// Update application.
    /// </summary>
    protected abstract System.Threading.Tasks.Task<UpdateActionResult> Update(
        string id,
        UpdateApplicationRequest updateApplicationRequest,
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
            Microsoft.AspNetCore.Mvc.ActionResult<CreateApplicationResponse> result
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
            CreateApplicationResponse result
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
    public readonly struct ListActionResult : Microsoft.AspNetCore.Mvc.IActionResult
    {
        private const int Default = -1;

        private readonly Microsoft.AspNetCore.Mvc.IActionResult? _raw;

        private ListActionResult(Microsoft.AspNetCore.Mvc.IActionResult? raw)
        {
            _raw = raw;
        }

        public async System.Threading.Tasks.Task ExecuteResultAsync(Microsoft.AspNetCore.Mvc.ActionContext context)
        {
            await (_raw ?? new Microsoft.AspNetCore.Mvc.NoContentResult()).ExecuteResultAsync(context);
        }

        public static ListActionResult Create200(
            Microsoft.AspNetCore.Mvc.ActionResult<ApplicationViewList> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new ListActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 200
            });
        }

        public static ListActionResult Create200(
            ApplicationViewList result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 200
            };
            return new ListActionResult(raw: actionResult);
        }

        public static ListActionResult Create400(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new ListActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 400
            });
        }

        public static ListActionResult Create400(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 400
            };
            return new ListActionResult(raw: actionResult);
        }

        public static ListActionResult Create401(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new ListActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 401
            });
        }

        public static ListActionResult Create401(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 401
            };
            return new ListActionResult(raw: actionResult);
        }

        public static ListActionResult Create403(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new ListActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 403
            });
        }

        public static ListActionResult Create403(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 403
            };
            return new ListActionResult(raw: actionResult);
        }

        public static ListActionResult CreateDefault(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new ListActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = Default
            });
        }

        public static ListActionResult CreateDefault(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = Default
            };
            return new ListActionResult(raw: actionResult);
        }

        public static ListActionResult CreateRaw(Microsoft.AspNetCore.Mvc.IActionResult result)
        {
            return new ListActionResult(raw: result);
        }
    }
    public readonly struct UpdateActionResult : Microsoft.AspNetCore.Mvc.IActionResult
    {
        private const int Default = -1;

        private readonly Microsoft.AspNetCore.Mvc.IActionResult? _raw;

        private UpdateActionResult(Microsoft.AspNetCore.Mvc.IActionResult? raw)
        {
            _raw = raw;
        }

        public async System.Threading.Tasks.Task ExecuteResultAsync(Microsoft.AspNetCore.Mvc.ActionContext context)
        {
            await (_raw ?? new Microsoft.AspNetCore.Mvc.NoContentResult()).ExecuteResultAsync(context);
        }

        public static UpdateActionResult Create204(
            Microsoft.AspNetCore.Mvc.Infrastructure.IStatusCodeActionResult result
        )
        {
            if (result.StatusCode != 204 && 204 != Default)
            {
                throw new System.InvalidOperationException($"Expected status code 204, but got {result.StatusCode}.");
            }

            return new UpdateActionResult(raw: result);
        }

        public static UpdateActionResult Create204(
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.StatusCodeResult(204);
            return new UpdateActionResult(raw: actionResult);
        }

        public static UpdateActionResult Create400(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new UpdateActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 400
            });
        }

        public static UpdateActionResult Create400(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 400
            };
            return new UpdateActionResult(raw: actionResult);
        }

        public static UpdateActionResult Create401(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new UpdateActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 401
            });
        }

        public static UpdateActionResult Create401(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 401
            };
            return new UpdateActionResult(raw: actionResult);
        }

        public static UpdateActionResult Create403(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new UpdateActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 403
            });
        }

        public static UpdateActionResult Create403(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 403
            };
            return new UpdateActionResult(raw: actionResult);
        }

        public static UpdateActionResult Create404(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new UpdateActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = 404
            });
        }

        public static UpdateActionResult Create404(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = 404
            };
            return new UpdateActionResult(raw: actionResult);
        }

        public static UpdateActionResult CreateDefault(
            Microsoft.AspNetCore.Mvc.ActionResult<Microsoft.AspNetCore.Mvc.ProblemDetails> result
        )
        {
            if (result.Value == null)
            {
                throw new System.InvalidOperationException("Result value must not be null.");
            }

            return new UpdateActionResult(raw: new Microsoft.AspNetCore.Mvc.ObjectResult(result.Value)
            {
                StatusCode = Default
            });
        }

        public static UpdateActionResult CreateDefault(
            Microsoft.AspNetCore.Mvc.ProblemDetails result
        )
        {
            var actionResult = new Microsoft.AspNetCore.Mvc.ObjectResult(result)
            {
                StatusCode = Default
            };
            return new UpdateActionResult(raw: actionResult);
        }

        public static UpdateActionResult CreateRaw(Microsoft.AspNetCore.Mvc.IActionResult result)
        {
            return new UpdateActionResult(raw: result);
        }
    }
}

#nullable restore
