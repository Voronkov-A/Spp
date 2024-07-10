using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Reflection;
using Spp.Common.Configuration;
using Spp.Common.Logging;

namespace Spp.Common.Hosting;

public static class HostApplicationBuilderExtensions
{
    public static T WithConfiguration<T>(this T builder, Action<IConfigurationBuilder> configure)
        where T : IHostApplicationBuilder
    {
        configure(builder.Configuration);
        return builder;
    }

    public static T WithDefaultConfiguration<T>(this T builder) where T : IHostApplicationBuilder
    {
        return WithConfiguration(builder, configuration => configuration
            .AddJsonFile("appsettings.json")
            .AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true)
            .AddEnvironmentVariables());
    }

    public static T WithDefaultLogging<T>(this T builder) where T : IHostApplicationBuilder
    {
        var settings = builder.Configuration.GetSettings<LoggingSettings>("Logging");
        builder.Logging.ClearProviders();

        if (settings.Console?.Enabled ?? false)
        {
            builder.Logging.AddConsole();
        }

        if (settings.Debug?.Enabled ?? false)
        {
            builder.Logging.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton<ILoggerProvider, ScopedDebugLoggerProvider>());
            builder.Services.TryAddEnumerable(ServiceDescriptor
                .Singleton<IConfigureOptions<ScopedDebugLoggerOptions>, ScopedDebugLoggerConfigureOptions>());
            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton<IOptionsChangeTokenSource<ScopedDebugLoggerOptions>,
                LoggerProviderOptionsChangeTokenSource<ScopedDebugLoggerOptions, ScopedDebugLoggerProvider>>());
        }

        builder.Logging.Services.AddTransient<IExternalScopeProvider>(
            _ => new GlobalExternalScopeProvider(settings.Properties));
        return builder;
    }

    public static T WithServices<T>(this T builder, Action<IServiceCollection> configureServices)
        where T : IHostApplicationBuilder
    {
        configureServices(builder.Services);
        return builder;
    }

    public static T WithServices<T>(this T builder, Action<IServiceCollection, IConfiguration> configureServices)
        where T : IHostApplicationBuilder
    {
        configureServices(builder.Services, builder.Configuration);
        return builder;
    }
}
