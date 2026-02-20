using Client.Services;
using System.Security.Cryptography;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection;

internal static class PodcastIndexExtensions
{
    internal static IServiceCollection AddPodcastIndex(this IServiceCollection services)
    {
        services.AddHttpClient<PodcastIndexClient>((sp, client) =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();
            var section = configuration.GetSection("PodcastIndex");
            
            client.BaseAddress = new Uri(section["BaseAddress"]!);

            var key = section["Key"];
            var secret = section["Secret"];

            var window = DateTime.UtcNow - DateTime.UnixEpoch;
            int windowFrame = (int)window.TotalSeconds;

            var hash = SHA1.HashData(Encoding.UTF8.GetBytes(key + secret + windowFrame));
            var hex = Convert.ToHexStringLower(hash);

            client.DefaultRequestHeaders.Add("User-Agent", "SuperPodcastPlayer/1.3");
            client.DefaultRequestHeaders.Add("X-Auth-Date", windowFrame.ToString());
            client.DefaultRequestHeaders.Add("X-Auth-Key", key);
            client.DefaultRequestHeaders.Add("Authorization", hex);
        })
        .AddHttpMessageHandler<CachingHandler>()
        .AddHttpMessageHandler<LanguageHandler>();

        services.AddTransient<CachingHandler>();
        services.AddTransient<LanguageHandler>();

        return services;
    }
}
