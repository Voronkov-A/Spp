using System;

namespace Spp.IdentityProvider.Tests.TestHelpers;

public class SampleClientApplicationContract(Uri webApiUrl)
{
    public Uri WebApiUrl { get; } = webApiUrl;
}
