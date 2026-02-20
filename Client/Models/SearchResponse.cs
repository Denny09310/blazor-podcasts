using System.Text.Json.Serialization;

namespace Client.Models;

#pragma warning disable CS8618

internal sealed class SearchResponse : BaseResponse
{
    [JsonPropertyName("feeds")]
    public List<PodcastSummary> Searches { get; set; }
}

#pragma warning restore CS8618