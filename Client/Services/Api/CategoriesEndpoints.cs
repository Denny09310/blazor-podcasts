using Client.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace Client.Services;

internal sealed class CategoriesEndpoints(HttpClient http)
{
    public string BaseAddress => $"{http.BaseAddress}categories";

    public async Task<CategoriesResponse> ListAsync(CancellationToken ct = default)
        => await http.GetFromJsonAsync<CategoriesResponse>($"{BaseAddress}/list", Converter.Settings, ct)
            ?? throw new JsonException($"Can't deserialize response into {nameof(CategoriesResponse)}");
}