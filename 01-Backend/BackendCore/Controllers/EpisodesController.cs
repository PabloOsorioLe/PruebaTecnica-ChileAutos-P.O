using BackendCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackendCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EpisodesController : ControllerBase
    {
        private readonly IEpisodeService _episodeService;

        public EpisodesController(IEpisodeService episodeService)
        {
            _episodeService = episodeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page = 1)
        {
            var data = await _episodeService.GetEpisodesAsync(page);
            return Ok(data);
        }
    }
}