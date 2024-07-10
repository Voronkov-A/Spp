using System;
using System.Net.Http;
using System.Text.Json;
using Spp.Common.Miscellaneous;

namespace Spp.IdentityProvider.TestClient.Service.V1;

public partial class ServiceTestClient
{
    public ServiceTestClient(Uri baseAddress)
        : this(
            new HttpClient
            {
                BaseAddress = baseAddress
            })
    {
    }

    public ServiceTestClient(HttpClient httpClient)
        : this(httpClient, new JsonSerializerOptions(JsonSerializerDefaults.Web))
    {
    }

    partial void EnumToString<T>(T value, ref string? result) where T : struct, Enum
    {
        result = EnumSerializer.ToString(value);
    }
}
