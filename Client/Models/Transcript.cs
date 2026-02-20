using System.Text.Json.Serialization;

namespace Client.Models;

#pragma warning disable CS8618

internal sealed class Transcript
{
    [JsonPropertyName("url")]
    public Uri Url { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }
}

#pragma warning restore CS8618