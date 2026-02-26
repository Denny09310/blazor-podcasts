namespace Client.Services;

internal sealed class PodcastIndexClient(HttpClient http)
{
    public CategoriesEndpoints Categories { get; set; } = new(http);
    public EpisodesEndpoints Episodes { get; set; } = new(http);
    public PodcastsEndpoints Podcasts { get; set; } = new(http);
    public SearchEndpoints Search { get; set; } = new(http);
}