using System.Text.Json.Serialization;

namespace Client.Models;

#pragma warning disable CS8618

internal sealed class Value
{
    [JsonPropertyName("model")]
    public Model Model { get; set; }

    [JsonPropertyName("destinations")]
    public List<Destination> Destinations { get; set; }
}

#pragma warning restore CS8618