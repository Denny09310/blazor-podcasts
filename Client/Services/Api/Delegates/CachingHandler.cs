using Microsoft.Extensions.Caching.Memory;

namespace Client.Services;

internal sealed class CachingHandler(
    IMemoryCache memory,
    IPersistentCache persistent) : DelegatingHandler
{
    private static readonly TimeSpan DefaultDuration = TimeSpan.FromMinutes(5);

    protected override async Task<HttpResponseMessage> SendAsync(
    HttpRequestMessage request,
    CancellationToken cancellationToken)
    {
        if (request.Method != HttpMethod.Get)
            return await base.SendAsync(request, cancellationToken);

        var key = request.RequestUri!.ToString();

        if (memory.TryGetValue<CachedResponse>(key, out var cached))
            return CreateResponse(cached!);

        cached = await persistent.GetAsync(key);
        if (cached is not null)
        {
            memory.Set(key, cached, DefaultDuration);
            return CreateResponse(cached);
        }

        var response = await base.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
            return response;

        var content = await response.Content.ReadAsByteArrayAsync(cancellationToken);

        cached = new CachedResponse
        {
            StatusCode = response.StatusCode,
            Content = content,
            Headers = response.Headers.ToDictionary(h => h.Key, h => h.Value),
            ContentHeaders = response.Content.Headers.ToDictionary(h => h.Key, h => h.Value)
        };

        memory.Set(key, cached, DefaultDuration);
        await persistent.SetAsync(key, cached, DefaultDuration);

        return CreateResponse(cached);
    }

    private static HttpResponseMessage CreateResponse(CachedResponse cached)
    {
        var response = new HttpResponseMessage(cached.StatusCode)
        {
            Content = new ByteArrayContent(cached.Content)
        };

        foreach (var header in cached.Headers)
            response.Headers.TryAddWithoutValidation(header.Key, header.Value);

        foreach (var header in cached.ContentHeaders)
            response.Content.Headers.TryAddWithoutValidation(header.Key, header.Value);

        return response;
    }
}