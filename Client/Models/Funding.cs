using System.Text.Json.Serialization;

namespace Client.Models;

#pragma warning disable CS8618

public sealed class Funding
{
    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }
}

#pragma warning restore CS8618