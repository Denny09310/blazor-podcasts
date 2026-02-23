using System.Text.Json.Serialization;

namespace Client.Models;

#pragma warning disable CS8618

public sealed class Model
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("method")]
    public string Method { get; set; }

    [JsonPropertyName("suggested")]
    public string Suggested { get; set; }
}

#pragma warning restore CS8618