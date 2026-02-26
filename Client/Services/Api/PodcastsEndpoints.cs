using Client.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace Client.Services;

internal sealed class PodcastsEndpoints(HttpClient http)
{
    public string BaseAddress => $"{http.BaseAddress}podcasts";

    public async Task<PodcastResponse> ByFeedIdAsync(int id, CancellationToken ct = default)
        => await http.GetFromJsonAsync<PodcastResponse>($"{BaseAddress}/byfeedid?id={id}", Converter.Settings, ct)
            ?? throw new JsonException($"Can't deserialize response into {nameof(PodcastResponse)}");

    public async Task<SearchResponse> TrendingAsync(TrendingRequest? request = null, CancellationToken ct = default)
        => await http.GetFromJsonAsync<SearchResponse>($"{BaseAddress}/trending?{request?.ToQuery()}", Converter.Settings, ct)
            ?? throw new JsonException($"Can't deserialize response into {nameof(SearchResponse)}");
}
