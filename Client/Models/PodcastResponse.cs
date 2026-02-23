using System.Text.Json.Serialization;

namespace Client.Models;

#pragma warning disable CS8618

public sealed class PodcastResponse : BaseResponse
{
    [JsonPropertyName("feed")]
    public Podcast Value { get; set; }
}

#pragma warning restore CS8618