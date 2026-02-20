namespace Client.Services;

internal interface IPersistentCache
{
    Task<CachedResponse?> GetAsync(string key);
    Task SetAsync(string key, CachedResponse value, TimeSpan duration);
}
