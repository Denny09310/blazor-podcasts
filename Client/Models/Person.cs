using System.Text.Json.Serialization;

namespace Client.Models;

#pragma warning disable CS8618

public sealed class Person
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("role")]
    public string Role { get; set; }

    [JsonPropertyName("group")]
    public string Group { get; set; }

    [JsonPropertyName("href")]
    public string Href { get; set; }

    [JsonPropertyName("img")]
    public string Img { get; set; }
}

#pragma warning restore CS8618