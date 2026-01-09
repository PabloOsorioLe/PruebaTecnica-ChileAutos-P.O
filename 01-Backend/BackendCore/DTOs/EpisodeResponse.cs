using System.Text.Json.Serialization;

namespace BackendCore.DTOs
{
    public class ApiResponseInfo
    {
        public int Count { get; set; }
        public int Pages { get; set; }
        public string? Next { get; set; }
        public string? Prev { get; set; }
    }
    public class EpisodeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("air_date")]
        public string AirDate { get; set; } = string.Empty;
        public string Episode { get; set; } = string.Empty;

        [JsonPropertyName("characters")]
        public List<string> Characters { get; set; } = new();
    }

    public class PaginatedResponse<T>
    {
        public ApiResponseInfo Info { get; set; } = new();
        public List<T> Results { get; set; } = new();
    }
}
