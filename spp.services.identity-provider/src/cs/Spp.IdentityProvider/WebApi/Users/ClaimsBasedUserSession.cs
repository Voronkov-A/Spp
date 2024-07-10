using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Spp.IdentityProvider.WebApi.Users;

public class ClaimsBasedUserSession(IHttpContextAccessor httpContextAccessor) : IUserSession
{
    public Task AddClientIdAsync(string clientId)
    {
        return Task.CompletedTask;
    }

    public Task<string> CreateSessionIdAsync(ClaimsPrincipal principal, AuthenticationProperties properties)
    {
        return Task.FromResult(Guid.NewGuid().ToString());
    }

    public Task EnsureSessionIdCookieAsync()
    {
        return Task.CompletedTask;
    }

    public Task<IEnumerable<string>> GetClientListAsync()
    {
        var clientIds
            = httpContextAccessor.HttpContext?.User.Claims.Where(x => x.Type == "client_id").Select(x => x.Value)
              ?? Enumerable.Empty<string>();
        return Task.FromResult(clientIds);
    }

    public Task<string?> GetSessionIdAsync()
    {
        return Task.FromResult<string?>(null);
        //return Task.FromResult<string?>(Guid.NewGuid().ToString());
    }

    public Task<ClaimsPrincipal?> GetUserAsync()
    {
        return Task.FromResult(httpContextAccessor.HttpContext?.User);
    }

    public Task RemoveSessionIdCookieAsync()
    {
        return Task.CompletedTask;
    }
}
