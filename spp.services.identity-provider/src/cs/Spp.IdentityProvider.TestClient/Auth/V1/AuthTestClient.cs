using System;
using System.Net.Http;
using System.Text.Json;
using Spp.Common.Miscellaneous;
using Spp.Common.Miscellaneous.Serialization;

namespace Spp.IdentityProvider.TestClient.Auth.V1;

public partial class AuthTestClient
{
    public AuthTestClient(Uri baseAddress)
        : this(
            new HttpClient
            {
                BaseAddress = baseAddress
            })
    {
    }

    public AuthTestClient(HttpClient httpClient)
        : this(httpClient, DefaultJsonSerializer.Options)
    {
    }

    partial void EnumToString<T>(T value, ref string? result) where T : struct, Enum
    {
        result = EnumSerializer.ToString(value);
    }
}
