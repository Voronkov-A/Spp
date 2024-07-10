using System;
using System.Collections.Generic;
using System.Linq;

namespace Spp.Common.Exceptions;

internal class ExceptionResolver(IEnumerable<IExceptionDescriptor> descriptors) : IExceptionResolver
{
    private readonly List<IExceptionDescriptor> _descriptors = descriptors.Distinct().ToList();

    public bool IsTransient(Exception exception)
    {
        return _descriptors.Any(x => x.IsTransient(exception));
    }
}
