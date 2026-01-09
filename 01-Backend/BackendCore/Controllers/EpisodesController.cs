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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _episodeService.GetEpisodeByIdAsync(id);

            if (data == null)
            {
                return NotFound(new { message = $"No se encontro el episodio con ID {id}" });
            }

            return Ok(data);
        }

    }
}