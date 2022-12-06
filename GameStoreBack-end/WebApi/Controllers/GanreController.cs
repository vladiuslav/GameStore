using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BLL.Services;
using BLL.Interfaces;
using BLL.Models;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : Controller
    {
        private IMapper _mapper;
        private IGenreService _genreService;
        public GenreController(IGenreService genreService, IMapper mapper)
        {
            _genreService = genreService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Get()
        {
            var genres = _mapper.Map<IEnumerable<GenreViewModel>>(await _genreService.GetAllAsync());
            return new JsonResult(genres);
        }
        [HttpGet("{id}", Name = nameof(GetGenre))]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetGenre(int id)
        {
            var genre = _mapper.Map<GenreViewModel>(await _genreService.GetByIdAsync(id));
            if (genre == null)
            {
                return NotFound();
            }
            return Ok(genre);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateGame(GenreViewModel game)
        {
            var genreModel = _mapper.Map<GenreModel>(game);
            await _genreService.AddAsync(genreModel);
            return Ok();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(string id, [FromBody] GenreViewModel game)
        {
            var genreModel = _mapper.Map<GenreModel>(game);
            await _genreService.UpdateAsync(genreModel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            await _genreService.DeleteByIdAsync(id);
            return NoContent();
        }
    }
}
