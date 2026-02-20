using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace Client.Services;

internal sealed class CachedResponse
{
    public HttpStatusCode StatusCode { get; init; }
    public byte[] Content { get; init; } = default!;
    public Dictionary<string, IEnumerable<string>> Headers { get; init; } = [];
    public Dictionary<string, IEnumerable<string>> ContentHeaders { get; init; } = [];
}