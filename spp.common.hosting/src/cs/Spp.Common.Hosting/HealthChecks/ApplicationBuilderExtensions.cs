using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Spp.Common.Hosting.HealthChecks;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseDefaultHealthChecks(
        this IApplicationBuilder app,
        PathString path)
    {
        return app.UseHealthChecks(path, new HealthCheckOptions
        {
            ResponseWriter = WriteHealthCheckResponse
        });
    }

    private static async Task WriteHealthCheckResponse(HttpContext context, HealthReport report)
    {
        context.Response.ContentType = MediaTypeNames.Application.Json;
        await context.Response.WriteAsync($"\"{report.Status.ToString().ToLowerInvariant()}\"", context.RequestAborted);
    }
}
