using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;

namespace Spp.Common.Logging;

[ProviderAlias("Debug")]
public class ScopedDebugLoggerProvider : ILoggerProvider, ISupportExternalScope
{
    private readonly ConcurrentDictionary<string, ScopedDebugLogger> _loggers;
    private readonly IOptionsMonitor<ScopedDebugLoggerOptions> _options;
    private IExternalScopeProvider? _scopeProvider;

    public ScopedDebugLoggerProvider(IOptionsMonitor<ScopedDebugLoggerOptions> options)
    {
        _loggers = new ConcurrentDictionary<string, ScopedDebugLogger>();
        _options = options;
        _options.OnChange(opt => UpdateLoggers(x => x.IncludeScopes = opt.IncludeScopes));
    }

    public ILogger CreateLogger(string categoryName)
    {
        return _loggers.TryGetValue(categoryName, out var logger)
            ? logger
            : _loggers.GetOrAdd(
                categoryName,
                new ScopedDebugLogger(categoryName)
                {
                    ScopeProvider = _scopeProvider,
                    IncludeScopes = _options.CurrentValue.IncludeScopes
                });
    }

    public void Dispose()
    {
        //
    }

    public void SetScopeProvider(IExternalScopeProvider scopeProvider)
    {
        _scopeProvider = scopeProvider;
        UpdateLoggers(x => x.ScopeProvider = _scopeProvider);
    }

    private void UpdateLoggers(Action<ScopedDebugLogger> update)
    {
        foreach (var logger in _loggers)
        {
            update(logger.Value);
        }
    }
}
