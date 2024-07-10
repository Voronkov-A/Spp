/*
The MIT License (MIT)

Copyright (c) 2015 Kristian Hellang

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;

namespace Spp.Common.Miscellaneous.DependencyInjection.Decoration;

// Source: https://github.com/khellang/Scrutor/blob/master/src/Scrutor/OpenGenericDecorationStrategy.cs
internal class OpenGenericDecorationStrategy : DecorationStrategy
{
    public OpenGenericDecorationStrategy(
        Type serviceType,
        Type? decoratorType,
        Func<object, IServiceProvider, object>? decoratorFactory)
        : base(serviceType)
    {
        DecoratorType = decoratorType;
        DecoratorFactory = decoratorFactory;
    }

    private Type? DecoratorType { get; }

    private Func<object, IServiceProvider, object>? DecoratorFactory { get; }

    public override bool CanDecorate(Type serviceType) =>
        serviceType.IsGenericType
            && !serviceType.IsGenericTypeDefinition
            && serviceType.GetGenericTypeDefinition() == ServiceType.GetGenericTypeDefinition()
            && (DecoratorType is null || HasCompatibleGenericArguments(serviceType, DecoratorType));

    public override Func<IServiceProvider, object> CreateDecorator(Type serviceType)
    {
        if (DecoratorType is not null)
        {
            var genericArguments = serviceType.GetGenericArguments();
            var closedDecorator = DecoratorType.MakeGenericType(genericArguments);

            return TypeDecorator(serviceType, closedDecorator);
        }

        if (DecoratorFactory is not null)
        {
            return FactoryDecorator(serviceType, DecoratorFactory);
        }

        throw new InvalidOperationException($"Both serviceType and decoratorFactory can not be null.");
    }

    private static bool HasCompatibleGenericArguments(Type type, Type genericTypeDefinition)
    {
        var genericArguments = type.GetGenericArguments();
        try
        {
            _ = genericTypeDefinition.MakeGenericType(genericArguments);
            return true;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }
}
