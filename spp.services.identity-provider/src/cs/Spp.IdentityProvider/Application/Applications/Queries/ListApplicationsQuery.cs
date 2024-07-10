using Spp.Common.Mediator;
using Spp.IdentityProvider.WebApi.Applications.V1;

namespace Spp.IdentityProvider.Application.Applications.Queries;

public readonly struct ListApplicationsQuery(string clientId) : IRequest<ApplicationViewList>
{
    public string ClientId { get; } = clientId;
}
