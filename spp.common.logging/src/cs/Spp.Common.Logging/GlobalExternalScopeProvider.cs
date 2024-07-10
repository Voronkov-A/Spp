using Microsoft.Extensions.Logging;
using System;
using System.Threading;

namespace Spp.Common.Logging;

public class GlobalExternalScopeProvider : IExternalScopeProvider, IDisposable
{
    private readonly Scope _globalScope;
    private readonly AsyncLocal<Scope?> _currentScope;

    public GlobalExternalScopeProvider(LoggingProperties properties)
    {
        var globalState = new DictionaryView<string, string>
        {
            [nameof(properties.ApplicationName)] = properties.ApplicationName
        };
        _globalScope = new Scope(this, globalState, null);
        _currentScope = new AsyncLocal<Scope?>();
    }

    public void Dispose()
    {
        _globalScope.Dispose();
    }

    public void ForEachScope<TState>(Action<object?, TState> callback, TState state)
    {
        void Report(Scope? current)
        {
            if (current == null)
            {
                return;
            }

            Report(current.Parent);
            callback(current.State, state);
        }

        Report(_currentScope.Value);
    }

    public IDisposable Push(object? state)
    {
        var parent = _currentScope.Value ?? _globalScope;
        var newScope = new Scope(this, state, parent);
        _currentScope.Value = newScope;
        return newScope;
    }

    private sealed class Scope : IDisposable
    {
        private readonly GlobalExternalScopeProvider _provider;
        private bool _isDisposed;

        internal Scope(GlobalExternalScopeProvider provider, object? state, Scope? parent)
        {
            _provider = provider;
            State = state;
            Parent = parent;
        }

        public Scope? Parent { get; }

        public object? State { get; }

        public override string? ToString()
        {
            return State?.ToString();
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _provider._currentScope.Value = Parent;
                _isDisposed = true;
            }
        }
    }
}
