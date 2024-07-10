using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using System.Threading.Tasks;

namespace Spp.Common.Localization.AspNetCore;

internal class CookieCultureProvider : RequestCultureProvider
{
    public override Task<ProviderCultureResult?> DetermineProviderCultureResult(HttpContext httpContext)
    {
        var cookie = httpContext.Request.Cookies[CultureCookie.Name];
        var result = cookie == null ? null : new ProviderCultureResult(cookie, cookie);
        return Task.FromResult(result);
    }
}
