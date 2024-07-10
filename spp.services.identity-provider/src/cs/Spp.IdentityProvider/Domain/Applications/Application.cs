using Duende.IdentityServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spp.IdentityProvider.Domain.Applications;

public class Application
{
    private readonly List<Uri> _redirectUris;

    public Application(
        ApplicationId id,
        string clientId,
        string clientSecret,
        IEnumerable<Uri> redirectUris)
    {
        Id = id;
        ClientId = clientId;
        ClientSecretHash = clientSecret.Sha256();
        _redirectUris = new List<Uri>(redirectUris);
    }

    private Application()
    {
        _redirectUris = null!;
        ClientId = null!;
        ClientSecretHash = null!;
    }

    public ApplicationId Id { get; }

    public string ClientId { get; }

    public string ClientSecretHash { get; private set; }

    public IReadOnlyCollection<Uri> RedirectUris => _redirectUris;

    public void Update(string clientSecret, IEnumerable<Uri> redirectUris)
    {
        ClientSecretHash = clientSecret.Sha256();

        if (!redirectUris.SequenceEqual(_redirectUris))
        {
            _redirectUris.Clear();
            _redirectUris.AddRange(redirectUris);
        }
    }
}
