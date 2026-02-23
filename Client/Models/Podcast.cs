using System.Text.Json.Serialization;

namespace Client.Models;

#pragma warning disable CS8618

public sealed class Podcast : Feed
{
    [JsonPropertyName("itunesType")]
    public string ItunesType { get; set; }

    [JsonPropertyName("value")]
    public Value Value { get; set; }

    [JsonPropertyName("funding")]
    public Funding Funding { get; set; }
}

#pragma warning restore CS8618