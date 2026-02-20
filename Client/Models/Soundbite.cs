using System.Text.Json.Serialization;

namespace Client.Models;

#pragma warning disable CS8618

internal sealed class Soundbite
{
    [JsonPropertyName("startTime")]
    public long StartTime { get; set; }

    [JsonPropertyName("duration")]
    public long Duration { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }
}

#pragma warning restore CS8618