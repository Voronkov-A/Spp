using Xunit;

namespace Spp.IdentityProvider.Tests.TestHelpers.Collections;

[CollectionDefinition(nameof(IdentityProviderServiceCollection))]
public class IdentityProviderServiceCollection : ICollectionFixture<IdentityProviderServiceFixture>;
