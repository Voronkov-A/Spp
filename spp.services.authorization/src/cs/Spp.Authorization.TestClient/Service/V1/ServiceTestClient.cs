using Spp.Common.Miscellaneous.Serialization;
using System;
using System.Net.Http;

namespace Spp.Authorization.TestClient.Service.V1;

public partial class ServiceTestClient
{
    public ServiceTestClient(Uri baseAddress)
        : this(
            new HttpClient
            {
                BaseAddress = baseAddress
            },
            DefaultJsonSerializer.Options)
    {
    }
}
