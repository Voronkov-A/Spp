using Spp.Authorization.Tests.TestHelpers.Fixtures;
using Xunit;

namespace Spp.Authorization.Tests.TestHelpers.Collections;

[CollectionDefinition(nameof(AuthorizationServiceCollection))]
public class AuthorizationServiceCollection : ICollectionFixture<AuthorizationServiceFixture>;
