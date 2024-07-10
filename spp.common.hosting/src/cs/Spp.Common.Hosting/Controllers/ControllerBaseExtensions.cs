using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Spp.Common.Miscellaneous;
using System;
using System.Threading.Tasks;

namespace Spp.Common.Hosting.Controllers;

public static class ControllerBaseExtensions
{
    public static void AddCreatedResourceLocation(this ControllerBase self, string resourceId)
    {
        var uri = new Uri(new Uri(self.Request.GetEncodedUrl().EnsureEndsWith('/')), resourceId).ToString();
        self.Response.OnStarting(() =>
        {
            self.Response.Headers.Append(HeaderNames.Location, uri);
            return Task.CompletedTask;
        });
    }
}
