using Models;
using System.Text.Json.Serialization;

namespace Client.Models;

#pragma warning disable CS8618

public sealed class RandomEpisode : EpisodeBase
{
    [JsonPropertyName("feedTitle")]
    public string FeedTitle { get; set; }
}

#pragma warning restore CS8618