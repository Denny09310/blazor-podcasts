using Client.Models;
using System.Text.Json.Serialization;

namespace Models;

#pragma warning disable CS8618

internal abstract partial class EpisodeBase
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("link")]
    public Uri Link { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("guid")]
    public string Guid { get; set; }

    [JsonPropertyName("datePublished")]
    public long DatePublishedUnix { get; set; }

    [JsonPropertyName("dateCrawled")]
    public long DateCrawledUnix { get; set; }

    [JsonPropertyName("enclosureUrl")]
    public Uri EnclosureUrl { get; set; }

    [JsonPropertyName("enclosureType")]
    public string EnclosureType { get; set; }

    [JsonPropertyName("enclosureLength")]
    public long EnclosureLength { get; set; }

    [JsonPropertyName("explicit")]
    [JsonConverter(typeof(BooleanConverter))]
    public bool Explicit { get; set; }

    [JsonPropertyName("episode")]
    public long? Episode { get; set; }

    [JsonPropertyName("episodeType")]
    public EpisodeType EpisodeType { get; set; }

    [JsonPropertyName("season")]
    public long Season { get; set; }

    [JsonPropertyName("image")]
    public Uri Image { get; set; }

    [JsonPropertyName("feedItunesId")]
    public long? FeedItunesId { get; set; }

    [JsonPropertyName("feedImage")]
    public Uri FeedImage { get; set; }

    [JsonPropertyName("feedId")]
    public long FeedId { get; set; }

    [JsonPropertyName("feedLanguage")]
    public string FeedLanguage { get; set; }

    [JsonPropertyName("feedDead")]
    public long FeedDead { get; set; }
}

internal abstract partial class EpisodeBase
{
    [JsonIgnore]
    public DateTimeOffset DatePublished => DateTimeOffset.FromUnixTimeSeconds(DatePublishedUnix);

    [JsonIgnore]
    public DateTimeOffset DateCrawled => DateTimeOffset.FromUnixTimeSeconds(DateCrawledUnix);
}

[JsonConverter(typeof(JsonStringEnumConverter<EpisodeType>))]
public enum EpisodeType
{
    Full,
    Trailer,
    Bonus
}

#pragma warning restore CS8618