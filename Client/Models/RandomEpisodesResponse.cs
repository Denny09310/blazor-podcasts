using System.Text.Json.Serialization;

namespace Client.Models;

#pragma warning disable CS8618

internal sealed class RandomEpisodesResponse : BaseResponse
{
    [JsonPropertyName("episodes")]
    public List<RandomEpisodeDto> Episodes { get; set; }
}

#pragma warning restore CS8618