using System.Text.Json.Serialization;

namespace Client.Models;

#pragma warning disable CS8618

internal class BaseResponse
{
    [JsonPropertyName("status")]
    [JsonConverter(typeof(BooleanConverter))]
    public bool Status { get; set; }

    [JsonPropertyName("count")]
    public long Count { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("query")]
    [JsonConverter(typeof(QueryConverter))]
    public Query Query { get; set; }
}

#pragma warning restore CS8618