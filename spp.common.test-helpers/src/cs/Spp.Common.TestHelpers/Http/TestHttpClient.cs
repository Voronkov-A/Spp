using System.Net.Http;

namespace Spp.Common.TestHelpers.Http;

public class TestHttpClient : HttpClient
{
    public TestHttpClient(HttpClientHandler handler) : base(handler)
    {
        Handler = handler;
    }

    public TestHttpClient(HttpClientHandler handler, bool disposeHandler) : base(handler, disposeHandler)
    {
        Handler = handler;
    }

    public HttpClientHandler Handler { get; }
}
