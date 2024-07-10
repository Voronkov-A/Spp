using System;

namespace Spp.Common.Authentication.TokenAuthenticationScheme;

public readonly struct TokenParseResult
{
    private readonly Token? _value;
    private readonly string? _error;

    private TokenParseResult(Token? value, string? error)
    {
        _value = value;
        _error = error;
    }

    public bool IsSucceeded => _value != null;

    public Token Value => _value ?? throw new InvalidOperationException($"The result is not succeded: {_error}.");

    public string Error => IsSucceeded
        ? throw new InvalidOperationException("The result is succeded.")
        : _error ?? "";

    public static TokenParseResult Success(Token value)
    {
        return new(value, null);
    }

    public static TokenParseResult Fail(string error)
    {
        return new(null, error);
    }
}
