using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using OrchardCore.Localization;
using OrchardCore.Localization.PortableObject;
using Spp.Common.Miscellaneous.DependencyInjection;
using System.Linq;

namespace Spp.Common.Localization.AspNetCore;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDefaultLocalization(
        this IServiceCollection services,
        string contentDirectoryRelativePath = "Content")
    {
        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new SupportedCultureCollection();
            options.DefaultRequestCulture = new RequestCulture(
                SupportedCultureCollection.DefaultCulture,
                SupportedCultureCollection.DefaultCulture);
            options.SupportedCultures = supportedCultures.ToList();
            options.SupportedUICultures = supportedCultures.ToList();
            options.RequestCultureProviders = new[]
            {
                new CookieCultureProvider()
            };
            options.FallBackToParentCultures = false;
            options.FallBackToParentUICultures = false;
        });

        return services
            .AddSingleton<IPluralRuleProvider, DefaultPluralRuleProvider>()
            .AddSingleton<ITranslationProvider, PoFilesTranslationsProvider>()
            .AddSingleton<ILocalizationFileLocationProvider>(_ => new PoFileLocationProvider(
                contentDirectoryRelativePath))
            .AddSingleton<ILocalizationManager, LocalizationManager>()
            .AddSingleton<IStringLocalizerFactory, PortableObjectStringLocalizerFactory>()
            .AddDecorator<IStringLocalizerFactory>((sp, inner) => new FallbackStringLocalizerFactory(
                inner,
                sp.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value.DefaultRequestCulture.UICulture))
            .AddTransient(typeof(IStringLocalizer<>), typeof(StringLocalizer<>));
    }
}
