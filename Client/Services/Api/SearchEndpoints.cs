using Client.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace Client.Services;

internal sealed class SearchEndpoints(HttpClient http)
{
    public string BaseAddress => $"{http.BaseAddress}search";

    public async Task<SearchResponse> ByTermAsync(string q, CancellationToken ct = default)
        => await http.GetFromJsonAsync<SearchResponse>($"{BaseAddress}/byterm?q={Uri.EscapeDataString(q)}", Converter.Settings, ct)
            ?? throw new JsonException($"Can't deserialize response into {nameof(SearchResponse)}");
}