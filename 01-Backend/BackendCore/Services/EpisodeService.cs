using BackendCore.DTOs;
using BackendCore.Interfaces;
using System.Text.Json;

namespace BackendCore.Services
{
    public class EpisodeService : IEpisodeService
    {
        private readonly HttpClient _httpClient;

        public EpisodeService(IHttpClientFactory httpClientFactory)
        {
            // Usamos el cliente nombrado que configuraste en Program.cs
            _httpClient = httpClientFactory.CreateClient("RickAndMorty");
        }

        public async Task<PaginatedResponse<EpisodeDto>> GetEpisodesAsync(int page)
        {
            // Consumimos la API externa usando el parámetro de página
            var response = await _httpClient.GetAsync($"episode?page={page}");

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Error al consultar la API de Rick & Morty: {response.StatusCode}");

            var content = await response.Content.ReadAsStringAsync();

            // Deserializamos con CaseInsensitive porque la API usa minúsculas
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var result = JsonSerializer.Deserialize<PaginatedResponse<EpisodeDto>>(content, options);

            return result ?? new PaginatedResponse<EpisodeDto>();
        }
    }
}