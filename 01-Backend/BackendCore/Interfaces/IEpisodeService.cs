using BackendCore.DTOs;

namespace BackendCore.Interfaces
{
    public interface IEpisodeService
    {
        Task<PaginatedResponse<EpisodeDto>> GetEpisodesAsync(int page);
    }
}