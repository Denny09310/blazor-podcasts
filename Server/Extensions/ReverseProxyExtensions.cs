using Microsoft.AspNetCore.OutputCaching;
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
                var config = httpContext.RequestServices.GetRequiredService<IConfiguration>();

                var (key, secret) = GetPodcastIndexCredentials(config);

                var unixTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                var authorization = CreateAuthorization(key, secret, unixTime);

                var lang = GetRequestLanguage(httpContext);

                AddLanguageQuery(transformContext, lang);
                AddPodcastIndexHeaders(transformContext.ProxyRequest, key, unixTime, authorization);

                return ValueTask.CompletedTask;
            });
        });

        return builder;
    }

    private static (string Key, string Secret) GetPodcastIndexCredentials(IConfiguration config)
    {
        var section = config.GetSection("PodcastIndex");

        var key = section["Key"]!;
        var secret = section["Secret"]!;

        return (key, secret);
    }

    private static string CreateAuthorization(string key, string secret, long unixTime)
    {
        var hash = SHA1.HashData(
            Encoding.UTF8.GetBytes(key + secret + unixTime));

        return Convert.ToHexStringLower(hash);
    }

    private static string GetRequestLanguage(HttpContext context)
    {
        var acceptLanguage = context.Request.Headers.AcceptLanguage.ToString();

        return acceptLanguage
            .Split(',', StringSplitOptions.RemoveEmptyEntries)
            .FirstOrDefault()?
            .Split('-')[0]
            ?? "en";
    }

    private static void AddLanguageQuery(RequestTransformContext context, string lang)
    {
        if (!context.Query.Collection.ContainsKey("lang"))
        {
            context.Query.Collection.Add("lang", lang);
        }
    }

    private static void AddPodcastIndexHeaders(
        HttpRequestMessage request,
        string key,
        long unixTime,
        string authorization)
    {
        request.Headers.Add("User-Agent", "BlazorPoscasts/1.0");
        request.Headers.Add("X-Auth-Date", unixTime.ToString());
        request.Headers.Add("X-Auth-Key", key);
        request.Headers.Add("Authorization", authorization);
    }
}