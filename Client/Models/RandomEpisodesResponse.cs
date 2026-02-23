using System.Text.Json.Serialization;

namespace Client.Models;

#pragma warning disable CS8618

public sealed class RandomEpisodesResponse : BaseResponse
{
    [JsonPropertyName("episodes")]
    public List<RandomEpisode> Episodes { get; set; }
}

#pragma warning restore CS8618