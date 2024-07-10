using Spp.Common.Authentication.Abstractions;
using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace Spp.Common.Authentication.Http;

public class AccessTokenCache(TimeProvider timeProvider)
{
    private readonly ConcurrentDictionary<Type, AccessToken> _items = new();

    public bool TryGet(Type consumerType, [NotNullWhen(true)] out AccessToken? token)
    {
        if (!_items.TryGetValue(consumerType, out token))
        {
            return false;
        }

        if (token.ExpirationTime >= timeProvider.GetUtcNow())
        {
            token = null;
            return false;
        }

        return true;
    }

    public void Set(Type consumerType, AccessToken token)
    {
        _items[consumerType] = token;
    }
}
