using System;
using System.Net.Http;
using System.Text.Json;
using Spp.Common.Miscellaneous;
using Spp.Common.Miscellaneous.Serialization;

namespace Spp.IdentityProvider.TestClient.Connect.V1;

public partial class ConnectTestClient
{
    public ConnectTestClient(Uri baseAddress)
        : this(
            new HttpClient
            {
                BaseAddress = baseAddress
            })
    {
    }

    public ConnectTestClient(HttpClient httpClient)
        : this(httpClient, DefaultJsonSerializer.Options)
    {
    }

    partial void EnumToString<T>(T value, ref string? result) where T : struct, Enum
    {
        result = EnumSerializer.ToString(value);
    }
}
