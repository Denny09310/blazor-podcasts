using System.Text.Json.Serialization;

namespace Client.Models;

#pragma warning disable CS8618

public class Feed
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("podcastGuid")]
    public Guid PodcastGuid { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("originalUrl")]
    public string OriginalUrl { get; set; }

    [JsonPropertyName("link")]
    public string Link { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("author")]
    public string Author { get; set; }

    [JsonPropertyName("ownerName")]
    public string OwnerName { get; set; }

    [JsonPropertyName("image")]
    public string Image { get; set; }

    [JsonPropertyName("artwork")]
    public string Artwork { get; set; }

    [JsonPropertyName("lastUpdateTime")]
    public long LastUpdateTime { get; set; }

    [JsonPropertyName("lastCrawlTime")]
    public long LastCrawlTime { get; set; }

    [JsonPropertyName("lastParseTime")]
    public long LastParseTime { get; set; }

    [JsonPropertyName("lastGoodHttpStatusTime")]
    public long LastGoodHttpStatusTime { get; set; }

    [JsonPropertyName("lastHttpStatus")]
    public long LastHttpStatus { get; set; }

    [JsonPropertyName("contentType")]
    public string ContentType { get; set; }

    [JsonPropertyName("itunesId")]
    public long? ItunesId { get; set; }

    [JsonPropertyName("generator")]
    public string Generator { get; set; }

    [JsonPropertyName("language")]
    public string Language { get; set; }

    [JsonPropertyName("explicit")]
    public bool Explicit { get; set; }

    [JsonPropertyName("type")]
    public long Type { get; set; }

    [JsonPropertyName("medium")]
    public string Medium { get; set; }

    [JsonPropertyName("dead")]
    [JsonConverter(typeof(BooleanConverter))]
    public bool Dead { get; set; }

    [JsonPropertyName("episodeCount")]
    public long EpisodeCount { get; set; }

    [JsonPropertyName("crawlErrors")]
    public long CrawlErrors { get; set; }

    [JsonPropertyName("parseErrors")]
    public long ParseErrors { get; set; }

    [JsonPropertyName("categories")]
    public Dictionary<string, string> Categories { get; set; }

    [JsonPropertyName("locked")]
    [JsonConverter(typeof(BooleanConverter))]
    public bool Locked { get; set; }

    [JsonPropertyName("imageUrlHash")]
    public long ImageUrlHash { get; set; }

    [JsonPropertyName("newestItemPubdate")]
    public long NewestItemPubdate { get; set; }
}

#pragma warning restore CS8618