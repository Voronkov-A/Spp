using System;

namespace Spp.Common.Authentication.Abstractions;

public interface IAuthenticationContext
{
    bool IsActive { get; }

    IDisposable Activate();
}

public interface IAuthenticationContext<T> : IAuthenticationContext;
