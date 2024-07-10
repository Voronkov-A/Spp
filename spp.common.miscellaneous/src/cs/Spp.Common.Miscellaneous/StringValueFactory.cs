using System;
using System.Linq.Expressions;
using BindingFlags = System.Reflection.BindingFlags;

namespace Spp.Common.Miscellaneous;

public static class StringValueFactory<T>
{
    private static readonly Func<string, T> Lambda = CreateLambda();

    public static T Create(string value)
    {
        return Lambda(value);
    }

    private static Func<string, T> CreateLambda()
    {
        var constructor
            = typeof(T).GetConstructor(BindingFlags.Public | BindingFlags.Instance, new[] { typeof(string) })
              ?? throw new InvalidOperationException($"Could not find constructor {typeof(T)}({typeof(string)}).");
        var valueParameter = Expression.Parameter(typeof(string));
        var newExpression = Expression.New(constructor, new[] { valueParameter });
        var createExpression = Expression.Lambda<Func<string, T>>(newExpression, valueParameter);
        return createExpression.Compile();
    }
}

public static class StringValueFactory
{
    public static T Create<T>(string value)
    {
        return StringValueFactory<T>.Create(value);
    }
}
