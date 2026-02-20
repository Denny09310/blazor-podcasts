using Client.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace Client.Services;

internal sealed class PodcastIndexClient(HttpClient http)
{
    public CategoriesEndpoints Categories { get; set; } = new(http);
    public EpisodesEndpoints Episodes { get; set; } = new(http);
    public PodcastsEndpoints Podcasts { get; set; } = new(http);
    public SearchEndpoints Search { get; set; } = new(http);
}

internal sealed class CategoriesEndpoints(HttpClient http)
{
    public string BaseAddress => $"{http.BaseAddress}categories";

    public async Task<CategoriesResponse> ListAsync(CancellationToken ct = default)
        => await http.GetFromJsonAsync<CategoriesResponse>($"{BaseAddress}/list", Converter.Settings, ct)
            ?? throw new JsonException($"Can't deserialize response into {nameof(CategoriesResponse)}");
}

internal sealed class EpisodesEndpoints(HttpClient http)
{
    public string BaseAddress => $"{http.BaseAddress}episodes";

    public async Task<EpisodesResponse> ByFeedIdAsync(int id, CancellationToken ct = default)
        => await http.GetFromJsonAsync<EpisodesResponse>($"{BaseAddress}/byfeedid?id={id}", Converter.Settings, ct)
            ?? throw new JsonException($"Can't deserialize response into {nameof(EpisodesResponse)}");

    public async Task<RandomEpisodesResponse> RandomAsync(RandomEpisodesRequest? request = null, CancellationToken ct = default)
        => await http.GetFromJsonAsync<RandomEpisodesResponse>($"{BaseAddress}/random?{request?.ToQuery()}", Converter.Settings, ct)
            ?? throw new JsonException($"Can't deserialize response into {nameof(RandomEpisodesResponse)}");
}

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

internal sealed class SearchEndpoints(HttpClient http)
{
    public string BaseAddress => $"{http.BaseAddress}search";

    public async Task<SearchResponse> ByTermAsync(string q, CancellationToken ct = default)
        => await http.GetFromJsonAsync<SearchResponse>($"{BaseAddress}/byterm?q={Uri.EscapeDataString(q)}", Converter.Settings, ct)
            ?? throw new JsonException($"Can't deserialize response into {nameof(SearchResponse)}");
}