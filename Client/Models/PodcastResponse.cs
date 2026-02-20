using System.Text.Json.Serialization;

namespace Client.Models;

#pragma warning disable CS8618

internal sealed class PodcastResponse : BaseResponse
{
    [JsonPropertyName("feed")]
    public PodcastDto Value { get; set; }
}

#pragma warning restore CS8618