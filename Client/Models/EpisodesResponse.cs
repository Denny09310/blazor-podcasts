using System.Text.Json.Serialization;

namespace Client.Models;

#pragma warning disable CS8618

internal sealed class EpisodesResponse : BaseResponse
{
    [JsonPropertyName("items")]
    public List<EpisodeDto> Items { get; set; }
}

#pragma warning restore CS8618