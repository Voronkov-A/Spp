using Spp.Common.Exceptions;
using System;
using System.Net.Http;
using System.Net.Sockets;

namespace Spp.Common.Http;

public sealed class ExceptionDescriptor : IExceptionDescriptor, IEquatable<ExceptionDescriptor>
{
    public bool IsTransient(Exception exception)
    {
        return exception is HttpRequestException && exception.InnerException is SocketException;
    }

    public bool Equals(ExceptionDescriptor? other)
    {
        return other is not null;
    }

    public override bool Equals(object? obj)
    {
        return obj is ExceptionDescriptor other && Equals(other);
    }

    public override int GetHashCode()
    {
        return typeof(ExceptionDescriptor).GetHashCode();
    }

    public static bool operator ==(ExceptionDescriptor left, ExceptionDescriptor right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(ExceptionDescriptor left, ExceptionDescriptor right)
    {
        return !left.Equals(right);
    }
}
