using System;
using System.Threading;

namespace Spp.Common.Authentication.Abstractions;

public class AuthenticationContext : IAuthenticationContext
{
    private static readonly AsyncLocal<bool> IsActiveState = new();

    public bool IsActive => IsActiveState.Value;

    public IDisposable Activate()
    {
        return new Activator();
    }

    private class Activator : IDisposable
    {
        private readonly bool _wasActive;

        public Activator()
        {
            _wasActive = IsActiveState.Value;
            IsActiveState.Value = true;
        }

        public void Dispose()
        {
            IsActiveState.Value = _wasActive;
        }
    }
}
