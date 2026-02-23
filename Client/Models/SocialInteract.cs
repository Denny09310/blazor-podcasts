using System.Text.Json.Serialization;

namespace Client.Models;

#pragma warning disable CS8618

public sealed class SocialInteract
{
    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("protocol")]
    public string Protocol { get; set; }

    [JsonPropertyName("accountId")]
    public string AccountId { get; set; }

    [JsonPropertyName("accountUrl")]
    public string AccountUrl { get; set; }

    [JsonPropertyName("priority")]
    public long Priority { get; set; }
}

#pragma warning restore CS8618