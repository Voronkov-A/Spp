using AutoFixture;
using Spp.Authorization.TestClient.Rbac.V1;

namespace Spp.Authorization.Tests.TestHelpers.Customizations;

internal class TranslationCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<Translation>(c => c.FromFactory(() => new Translation(
            language: "en-US",
            value: fixture.Create<string>())));
    }
}
