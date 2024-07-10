using Microsoft.AspNetCore.Builder;
using System;

namespace Spp.Common.Hosting;

public static class ApplicationBuilderExtensions
{
    public static T WithMiddlewares<T>(this T app, Action<T> configure) where T : IApplicationBuilder
    {
        configure(app);
        return app;
    }
}
