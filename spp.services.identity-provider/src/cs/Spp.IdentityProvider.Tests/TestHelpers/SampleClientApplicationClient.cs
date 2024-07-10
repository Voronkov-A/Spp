using Microsoft.Net.Http.Headers;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Spp.IdentityProvider.Tests.TestHelpers;

public sealed class SampleClientApplicationClient(HttpClient httpClient) : IDisposable
{
    public void Dispose()
    {
        httpClient.Dispose();
    }

    public async Task<HttpStatusCode> Resource()
    {
        using var response = await httpClient.GetAsync(SampleClientApplicationService.ResourcePath);
        return response.StatusCode;
    }

    public async Task<string> GetSignInRedirectUri(Uri uiRedirectUri)
    {
        using var response = await httpClient.GetAsync(
            $"{SampleClientApplicationService.SignInPath}?redirect_uri={uiRedirectUri}");

        if (response.StatusCode != HttpStatusCode.Redirect)
        {
            throw new InvalidOperationException(
                $"Status code is {response.StatusCode}, but {HttpStatusCode.Redirect} is expected.");
        }

        AddCookies(httpClient, response);
        return response.Headers.GetValues(HeaderNames.Location).Single();
    }

    public async Task<string> GetCallbackRedirectUri(Uri uri)
    {
        using var response = await httpClient.GetAsync(uri);

        if (response.StatusCode != HttpStatusCode.Found)
        {
            throw new InvalidOperationException(
                $"Status code is {response.StatusCode}, but {HttpStatusCode.OK} is expected.");
        }

        var cookies = new CookieContainer();

        AddCookies(httpClient, response);
        return response.Headers.GetValues(HeaderNames.Location).Single();
    }

    private static void AddCookies(HttpClient httpClient, HttpResponseMessage response)
    {
        var cookies = new CookieContainer();

        foreach (var header in response.Headers.Where(x => x.Key == HeaderNames.SetCookie).SelectMany(x => x.Value))
        {
            cookies.SetCookies(new Uri("https://localhost"), header);
        }

        foreach (var cookie in cookies.GetAllCookies().Cast<Cookie>())
        {
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation(
                HeaderNames.Cookie,
                $"{cookie.Name}={cookie.Value}");
        }
    }
}
