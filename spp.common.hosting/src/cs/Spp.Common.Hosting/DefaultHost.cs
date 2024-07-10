using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace Spp.Common.Hosting;

public static class DefaultHost
{
    public static WebApplicationBuilder CreateBuilder()
    {
        var builder = WebApplication.CreateBuilder();
        builder.Host.UseDefaultServiceProvider(x =>
        {
            x.ValidateOnBuild = true;
            x.ValidateScopes = true;
        });
        return builder;
    }
}
