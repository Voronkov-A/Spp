using System;

namespace Spp.Common.Miscellaneous;

public readonly struct TryResult<TValue, TError>
{
    private readonly TValue _value;
    private readonly TError _error;

    public TryResult(TValue value)
    {
        _value = value;
        _error = default!;
        IsSucceeded = true;
    }

    public TryResult(TError error)
    {
        _value = default!;
        _error = error;
        IsSucceeded = false;
    }

    public TValue Value =>
        IsSucceeded ? _value : throw new InvalidOperationException($"Result is not succeeded. Error: {_error}.");

    public TError Error =>
        IsSucceeded ? throw new InvalidOperationException("Result is succeeded.") : _error;

    public bool IsSucceeded { get; }

    public static TryResult<TValue, TError> Succeed(TValue value)
    {
        return new TryResult<TValue, TError>(value);
    }

    public static TryResult<TValue, TError> Fail(TError error)
    {
        return new TryResult<TValue, TError>(error);
    }

    public static implicit operator TryResult<TValue, TError>(TValue value)
    {
        return Succeed(value);
    }

    public static implicit operator TryResult<TValue, TError>(TError error)
    {
        return Fail(error);
    }
}
