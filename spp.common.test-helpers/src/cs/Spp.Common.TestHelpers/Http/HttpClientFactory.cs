using System;
using System.Net.Http;
using Microsoft.Net.Http.Headers;

namespace Spp.Common.TestHelpers.Http;

public static class HttpClientFactory
{
    public static TestHttpClient Create(Uri baseAddress)
    {
        return new TestHttpClient(
            new HttpClientHandler
            {
                AllowAutoRedirect = false
            })
        {
            BaseAddress = baseAddress
        };
    }

    public static TestHttpClient Create(Uri baseAddress, string accessToken)
    {
        var httpClient = Create(baseAddress);
        httpClient.DefaultRequestHeaders.TryAddWithoutValidation(HeaderNames.Authorization, $"Bearer {accessToken}");
        return httpClient;
    }
}
