using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Stores;
using Microsoft.EntityFrameworkCore;
using Spp.IdentityProvider.Domain.Users;
using System.Linq;
using System.Threading.Tasks;

namespace Spp.IdentityProvider.Persistence.Authorization;

public class ClientStore(AuthorizationDbContext dbContext) : IClientStore
{
    public async Task<Client?> FindClientByIdAsync(string clientId)
    {
        var application = await dbContext.Applications.SingleOrDefaultAsync(x => x.ClientId == clientId);

        if (application == null)
        {
            return null;
        }

        return new Client
        {
            ClientId = application.ClientId,
            ClientSecrets = new[]
            {
                new Secret(application.ClientSecretHash)
            },
            AllowedGrantTypes = new[]
            {
                GrantType.ClientCredentials,
                GrantType.AuthorizationCode
            },
            AllowedScopes = Scopes.Enumerate().ToList(),
            AllowOfflineAccess = true,
            RedirectUris = application.RedirectUris
                .SelectMany(x => new[] { x.ToString(), x.ToString().TrimEnd('/') })
                .ToList(),
            ProtocolType = IdentityServerConstants.ProtocolTypes.OpenIdConnect
        };
    }
}
