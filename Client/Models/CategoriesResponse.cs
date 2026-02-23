using System.Text.Json.Serialization;

namespace Client.Models;

#pragma warning disable CS8618

public sealed class CategoriesResponse : BaseResponse
{
    [JsonPropertyName("feeds")]
    public List<CategoryDto> Categories { get; set; }
}

#pragma warning restore CS8618