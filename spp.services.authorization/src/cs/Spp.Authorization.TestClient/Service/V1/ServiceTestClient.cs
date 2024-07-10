using System;
using System.Net.Http;
using System.Text.Json;

namespace Spp.Authorization.TestClient.Service.V1;

public partial class ServiceTestClient
{
    public ServiceTestClient(Uri baseAddress)
        : this(
            new HttpClient
            {
                BaseAddress = baseAddress
            },
            new JsonSerializerOptions(JsonSerializerDefaults.Web))
    {
    }
}
