using BackendCore.DTOs;
using BackendCore.Interfaces;
using System.Text.Json;
using System.Net.Http.Json;

namespace BackendCore.Services
{
    public class EpisodeService : IEpisodeService
    {
        private readonly HttpClient _httpClient;

        public EpisodeService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("RickAndMorty");
        }

        public async Task<PaginatedResponse<EpisodeDto>> GetEpisodesAsync(int page)
        {
            var response = await _httpClient.GetAsync($"episode?page={page}");

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Error al consultar la API: {response.StatusCode}");

            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var result = JsonSerializer.Deserialize<PaginatedResponse<EpisodeDto>>(content, options);

            return result ?? new PaginatedResponse<EpisodeDto>();
        }

        public async Task<EpisodeDto?> GetEpisodeByIdAsync(int id)
        {
          
            var response = await _httpClient.GetAsync($"episode/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

           
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<EpisodeDto>(content, options);
        }
    }
}