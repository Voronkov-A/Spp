using AutoFixture;
using Spp.Authorization.TestClient.Rbac.V1;
using System.Collections.Generic;
using System.Linq;

namespace Spp.Authorization.Tests.TestHelpers.Factories.TestClient;

internal class RbacFactory(IFixture fixture)
{
    public CreateRoleRequest CreateRoleRequest(IEnumerable<PermissionReference> references)
    {
        return new CreateRoleRequest(
            name: fixture.Create<LocalizedName>(),
            isDefault: fixture.Create<bool>(),
            permissions: references.ToList());
    }
}
