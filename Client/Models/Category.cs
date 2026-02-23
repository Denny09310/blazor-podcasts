using System.Text.Json.Serialization;

namespace Client.Models;

#pragma warning disable CS8618

public sealed class Category
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}

#pragma warning restore CS8618