using Microsoft.JSInterop;
using System.Text.Json;

namespace Client.Services;

internal sealed class LocalStorageCache(IJSRuntime js) : IPersistentCache
{
    private const string Prefix = "http-cache:";

    public async Task<CachedResponse?> GetAsync(string key)
    {
        var json = await js.InvokeAsync<string?>("localStorage.getItem", Prefix + key);

        if (string.IsNullOrWhiteSpace(json))
            return null;

        var wrapper = JsonSerializer.Deserialize<CacheWrapper>(json);

        if (wrapper!.ExpiresAt < DateTime.UtcNow)
        {
            await js.InvokeVoidAsync("localStorage.removeItem", Prefix + key);
            return null;
        }

        return wrapper.Response;
    }

    public async Task SetAsync(string key, CachedResponse value, TimeSpan duration)
    {
        var wrapper = new CacheWrapper
        {
            ExpiresAt = DateTime.UtcNow.Add(duration),
            Response = value
        };

        var json = JsonSerializer.Serialize(wrapper);

        await js.InvokeVoidAsync("localStorage.setItem", Prefix + key, json);
    }

    private sealed class CacheWrapper
    {
        public DateTime ExpiresAt { get; init; }
        public CachedResponse Response { get; init; } = default!;
    }
}
