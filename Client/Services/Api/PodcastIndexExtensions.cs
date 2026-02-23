using Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Microsoft.Extensions.DependencyInjection;

internal static class PodcastIndexExtensions
{
    internal static IServiceCollection AddPodcastIndex(this IServiceCollection services)
    {
        services.AddHttpClient<PodcastIndexClient>((sp, client) =>
        {
            var environment = sp.GetRequiredService<IWebAssemblyHostEnvironment>();
            client.BaseAddress = new Uri(environment.BaseAddress + "podcastindex/");
        })
        .AddHttpMessageHandler<CachingHandler>()
        .AddHttpMessageHandler<LanguageHandler>();

        services.AddTransient<CachingHandler>();
        services.AddTransient<LanguageHandler>();

        return services;
    }
}
