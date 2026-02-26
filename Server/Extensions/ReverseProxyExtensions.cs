using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.OutputCaching;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Yarp.ReverseProxy.Transforms;

internal static class ReverseProxyExtensions
{
    internal static void AddPodcastIndexPolicy(this OutputCacheOptions options)
    {
        options.AddPolicy("podcastindexPolicy", builder => builder.Expire(TimeSpan.FromMinutes(5)));
    }

    internal static IReverseProxyBuilder AddPodcastIndexTransform(this IReverseProxyBuilder builder)
    {
        builder.AddTransforms(builderContext =>
        {
            if (!string.Equals(builderContext.Route.RouteId, "podcastindex", StringComparison.OrdinalIgnoreCase))
                return;

            builderContext.AddRequestTransform(transformContext =>
            {
                var httpContext = transformContext.HttpContext;

                var config = httpContext
                    .RequestServices
                    .GetRequiredService<IConfiguration>();

                var section = config.GetSection("PodcastIndex");

                var key = section["Key"];
                var secret = section["Secret"];

                var unixTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

                var hash = SHA1.HashData(
                    Encoding.UTF8.GetBytes(key + secret + unixTime));

                var hex = Convert.ToHexStringLower(hash);

                var culture = httpContext.Features
                    .Get<IRequestCultureFeature>()
                    ?.RequestCulture.Culture
                    ?? CultureInfo.CurrentCulture;

                var lang = culture.TwoLetterISOLanguageName;

                if (!transformContext.Query.Collection.ContainsKey("lang"))
                {
                    transformContext.Query.Collection.Add("lang", lang);
                }

                var request = transformContext.ProxyRequest;

                request.Headers.Add("User-Agent", "BlazorPoscasts/1.0");
                request.Headers.Add("X-Auth-Date", unixTime.ToString());
                request.Headers.Add("X-Auth-Key", key);
                request.Headers.Add("Authorization", hex);

                return ValueTask.CompletedTask;
            });
        });

        return builder;
    }
}