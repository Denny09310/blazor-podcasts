using System.Text.Json.Serialization;

namespace Client.Models;

#pragma warning disable CS8618

public sealed class Destination
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("address")]
    public string Address { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("split")]
    public long Split { get; set; }

    [JsonPropertyName("fee")]
    public bool Fee { get; set; }

    [JsonPropertyName("customKey")]
    [JsonConverter(typeof(LongStringConverter))]
    public long CustomKey { get; set; }

    [JsonPropertyName("customValue")]
    public string CustomValue { get; set; }
}

#pragma warning restore CS8618