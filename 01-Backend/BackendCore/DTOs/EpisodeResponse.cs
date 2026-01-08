namespace BackendCore.DTOs
{
    // Clase para la información de paginación que envía la API original
    public class ApiResponseInfo
    {
        public int Count { get; set; }
        public int Pages { get; set; }
        public string? Next { get; set; }
        public string? Prev { get; set; }
    }

    // Clase que representa un episodio
    public class EpisodeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string AirDate { get; set; } = string.Empty;
        public string Episode { get; set; } = string.Empty;
    }

    // Clase envoltorio para la respuesta paginada
    public class PaginatedResponse<T>
    {
        public ApiResponseInfo Info { get; set; } = new();
        public List<T> Results { get; set; } = new();
    }
}
