using System.Text.Json.Serialization;

namespace Client.Models;

#pragma warning disable CS8618

public sealed class EpisodesResponse : BaseResponse
{
    [JsonPropertyName("items")]
    public List<Episode> Items { get; set; }
}

#pragma warning restore CS8618