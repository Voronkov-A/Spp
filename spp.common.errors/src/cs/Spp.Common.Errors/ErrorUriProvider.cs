using Microsoft.Extensions.Options;
using Spp.Common.Miscellaneous;
using System;

namespace Spp.Common.Errors;

internal class ErrorUriProvider(IOptions<ErrorUriSettings> settings) : IErrorUriProvider
{
    public Uri GetTypeUri<TErrorCode>(TErrorCode code) where TErrorCode : struct, Enum
    {
        var urn = new Uri(EnumSerializer.ToString(code), UriKind.Relative);
        return new Uri(settings.Value.Url, urn);
    }
}
