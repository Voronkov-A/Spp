using IntegrationMocks.Core;
using IntegrationMocks.Core.Networking;
using IntegrationMocks.Modules.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Net.Http.Headers;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Spp.Common.Localization.AspNetCore;
using Spp.Common.Localization.Tests.Localization;
using Xunit;

namespace Spp.Common.Localization.Tests.AspNetCore;

public class RequestLocalizationTests(RequestLocalizationTests.TestServiceFixture testServiceFixture) :
    IClassFixture<RequestLocalizationTests.TestServiceFixture>
{
    private const string HelloWorldPath = "/hello-world";

    private readonly IInfrastructureService<TestContract> _testService = testServiceFixture.TestService;

    [Theory]
    [InlineData("en-US", "Hello, world!")]
    [InlineData("ru-RU", "Привет, мир!")]
    public async Task HelloWorld_returns_localized_text_based_on_culture_cookie(
        string culture,
        string expectedText)
    {
        using var httpClient = new TestClient($"http://localhost:{_testService.Contract.WebApiPort}", culture);

        var actualText = await httpClient.HelloWorld();

        Assert.Equal(expectedText, actualText);
    }

    [Fact]
    public async Task HelloWorld_fallbacks_to_default_culture_when_requested_culture_is_not_found()
    {
        using var httpClient = new TestClient($"http://localhost:{_testService.Contract.WebApiPort}", "es-ES");

        var actualText = await httpClient.HelloWorld();

        Assert.Equal("Hello, world!", actualText);
    }

    public class TestServiceFixture : IAsyncLifetime
    {
        public TestService TestService { get; } = new TestService(PortManager.Default);

        public async Task DisposeAsync()
        {
            await TestService.DisposeAsync();
        }

        public async Task InitializeAsync()
        {
            await TestService.InitializeAsync();
        }
    }

    public class TestContract(int webApiPort)
    {
        public int WebApiPort { get; } = webApiPort;
    }

    public class TestService : MockWebApplicationService<TestContract>
    {
        public TestService(IPortManager portManager) : base(portManager)
        {
            Contract = new TestContract(WebApiPort.Number);
            AddController(typeof(TestController));
        }

        public override TestContract Contract { get; }

        protected override WebApplicationBuilder CreateWebApplicationBuilder()
        {
            var builder = base.CreateWebApplicationBuilder();

            builder.Services.AddDefaultLocalization();
            builder.Services.AddMemoryCache();
            return builder;
        }

        protected override void Configure(WebApplication app)
        {
            base.Configure(app);
            app.UseRequestLocalization();
        }
    }

    private class TestController(IStringLocalizer<Label> localizer) : Controller
    {
        [HttpGet(HelloWorldPath)]
        public Task<string> HelloWorld()
        {
            return Task.FromResult(localizer.Get(Label.HelloWorld));
        }
    }

    private class TestClient : IDisposable
    {
        private readonly HttpClient _httpClient;

        public TestClient(string baseAddress, string cultureCookie)
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(
                HeaderNames.Cookie,
                $"{CultureCookie.Name}={cultureCookie}");
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        public async Task<string> HelloWorld()
        {
            using var response = await _httpClient.GetAsync(HelloWorldPath);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
