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

namespace Spp.Common.Miscellaneous.DependencyInjection;

// Source: https://github.com/khellang/Scrutor/blob/master/src/Scrutor/ServiceDescriptorExtensions.cs
internal static class ServiceDescriptorExtensions
{
    public static ServiceDescriptor WithImplementationFactory(
        this ServiceDescriptor descriptor,
        Func<IServiceProvider, object> implementationFactory)
    {
        return new(descriptor.ServiceType, implementationFactory, descriptor.Lifetime);
    }

    public static ServiceDescriptor WithServiceType(this ServiceDescriptor descriptor, Type serviceType)
    {
        return descriptor switch
        {
            { ImplementationType: not null } =>
                new ServiceDescriptor(serviceType, descriptor.ImplementationType, descriptor.Lifetime),
            { ImplementationFactory: not null } =>
                new ServiceDescriptor(serviceType, descriptor.ImplementationFactory, descriptor.Lifetime),
            { ImplementationInstance: not null } =>
                new ServiceDescriptor(serviceType, descriptor.ImplementationInstance),
            _ => throw new ArgumentException(
                $"No implementation factory or instance or type found for {descriptor.ServiceType}.",
                nameof(descriptor))
        };
    }
}
