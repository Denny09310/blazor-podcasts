using Models;
using System.Text.Json.Serialization;

namespace Client.Models;

#pragma warning disable CS8618

public sealed class Episode : EpisodeBase
{
    [JsonPropertyName("duration")]
    public long Duration { get; set; }
    
    [JsonPropertyName("feedUrl")]
    public string FeedUrl { get; set; }

    [JsonPropertyName("podcastGuid")]
    public Guid PodcastGuid { get; set; }

    [JsonPropertyName("feedDuplicateOf")]
    public long? FeedDuplicateOf { get; set; }

    [JsonPropertyName("chaptersUrl")]
    public string ChaptersUrl { get; set; }

    [JsonPropertyName("transcriptUrl")]
    public string TranscriptUrl { get; set; }

    [JsonPropertyName("transcripts")]
    public List<Transcript> Transcripts { get; set; }

    [JsonPropertyName("soundbite")]
    public Soundbite Soundbite { get; set; }

    [JsonPropertyName("soundbites")]
    public List<Soundbite> Soundbites { get; set; }

    [JsonPropertyName("persons")]
    public List<Person> Persons { get; set; }

    [JsonPropertyName("socialInteract")]
    public List<SocialInteract> SocialInteract { get; set; }

    [JsonPropertyName("value")]
    public Value Value { get; set; }
}

#pragma warning restore CS8618