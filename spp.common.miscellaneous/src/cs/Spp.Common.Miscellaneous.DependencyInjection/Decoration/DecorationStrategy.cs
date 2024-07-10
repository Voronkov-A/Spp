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

using Microsoft.Extensions.DependencyInjection;
using System;

namespace Spp.Common.Miscellaneous.DependencyInjection.Decoration;

// Source: https://github.com/khellang/Scrutor/blob/master/src/Scrutor/DecorationStrategy.cs
internal abstract class DecorationStrategy
{
    protected DecorationStrategy(Type serviceType)
    {
        ServiceType = serviceType;
    }

    public Type ServiceType { get; }

    public abstract bool CanDecorate(Type serviceType);

    public abstract Func<IServiceProvider, object> CreateDecorator(Type serviceType);

    internal static DecorationStrategy WithType(Type serviceType, Type decoratorType) =>
        Create(serviceType, decoratorType, decoratorFactory: null);

    internal static DecorationStrategy WithFactory(
        Type serviceType,
        Func<object, IServiceProvider, object> decoratorFactory) =>
        Create(serviceType, decoratorType: null, decoratorFactory);

    protected static Func<IServiceProvider, object> TypeDecorator(Type serviceType, Type decoratorType) =>
        serviceProvider =>
        {
            var instanceToDecorate = serviceProvider.GetRequiredService(serviceType);
            return ActivatorUtilities.CreateInstance(serviceProvider, decoratorType, instanceToDecorate);
        };

    protected static Func<IServiceProvider, object> FactoryDecorator(
        Type decorated,
        Func<object, IServiceProvider, object> decoratorFactory) =>
        serviceProvider =>
        {
            var instanceToDecorate = serviceProvider.GetRequiredService(decorated);
            return decoratorFactory(instanceToDecorate, serviceProvider);
        };

    private static DecorationStrategy Create(
        Type serviceType,
        Type? decoratorType,
        Func<object, IServiceProvider, object>? decoratorFactory)
    {
        if (serviceType.IsGenericTypeDefinition)
        {
            return new OpenGenericDecorationStrategy(serviceType, decoratorType, decoratorFactory);
        }

        return new ClosedTypeDecorationStrategy(serviceType, decoratorType, decoratorFactory);
    }
}
