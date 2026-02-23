using System.Security.Cryptography;
using System.Text;

namespace Yarp.ReverseProxy.Transforms;

internal static class ReverseProxyExtensions
{
    internal static IReverseProxyBuilder AddPodcastIndexTransform(this IReverseProxyBuilder builder)
    {
        return builder.AddTransforms(context =>
        {
            context.AddRequestTransform(async transformer =>
            {
                var config = transformer.HttpContext
                    .RequestServices
                    .GetRequiredService<IConfiguration>();

                var section = config.GetSection("PodcastIndex");

                var key = section["Key"]!;
                var secret = section["Secret"]!;

                var window = DateTime.UtcNow - DateTime.UnixEpoch;
                int windowFrame = (int)window.TotalSeconds;

                var hash = SHA1.HashData(
                    Encoding.UTF8.GetBytes(key + secret + windowFrame));

                var hex = Convert.ToHexString(hash).ToLowerInvariant();

                var request = transformer.ProxyRequest;

                request.Headers.Remove("User-Agent");
                request.Headers.TryAddWithoutValidation("User-Agent", "SuperPodcastPlayer/1.3");
                request.Headers.TryAddWithoutValidation("X-Auth-Date", windowFrame.ToString());
                request.Headers.TryAddWithoutValidation("X-Auth-Key", key);
                request.Headers.TryAddWithoutValidation("Authorization", hex);
            });
        });
    }
}
