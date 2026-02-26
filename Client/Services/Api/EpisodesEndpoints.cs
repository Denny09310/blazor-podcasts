using Client.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace Client.Services;

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
