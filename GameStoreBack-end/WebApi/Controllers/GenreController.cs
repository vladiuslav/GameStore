using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DLL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IGenreService _genreService;
        public GenreController(IGenreService genreService, IMapper mapper)
        {
            _genreService = genreService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetGenres()
        {
            var genres = _mapper.Map<IEnumerable<GenreViewModel>>(await _genreService.GetAllAsync());
            return new JsonResult(genres);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetGenre(int id)
        {
            var genre = _mapper.Map<GenreViewModel>(await _genreService.GetByIdAsync(id));
            if (genre == null)
            {
                var problem = new ProblemDetails
                {
                    Title = "Genre not found",
                    Detail = $"The genre with ID {id} does not exist.",
                    Status = 404,
                };
                return NotFound(problem);
            }
            return Ok(genre);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateGenre(GenreViewModel genre)
        {
            var genreByName = await _genreService.GetByGenreNameAsync(genre.Name);
            if (genreByName != null)
            {
                var problem = new ProblemDetails
                {
                    Title = "Genre already exist",
                    Detail = $"The genre with name {genre.Name} already exist.",
                    Status = 400,
                };
                return BadRequest(problem);
            }
            var genreModel = _mapper.Map<GenreModel>(genre);
            await _genreService.AddAsync(genreModel);
            return Ok(genreModel);
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update([FromBody] GenreViewModel genre)
        {
            if ((await _genreService.GetByIdAsync(genre.Id)) == null)
            {
                var problem = new ProblemDetails
                {
                    Title = "Genre not found",
                    Detail = $"The genre with ID {genre.Id} does not exist.",
                    Status = 404,
                };
                return NotFound(problem);
            }

            var genreByName = await _genreService.GetByGenreNameAsync(genre.Name);

            if (genreByName != null &&(genre.Name != null && genreByName.Id != genre.Id))
            {
                var problem = new ProblemDetails
                {
                    Title = "Genre already exist",
                    Detail = $"The genre with name {genre.Name} already exist.",
                    Status = 400,
                };
                return BadRequest(problem);
            }

            var genreModel = _mapper.Map<GenreModel>(genre);
            await _genreService.UpdateAsync(genreModel);
            return Ok(genre);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var genre = await _genreService.GetByIdAsync(id);
            if (genre == null)
            {
                var problem = new ProblemDetails
                {
                    Title = "Genre not found",
                    Detail = $"The genre with ID {id} does not exist.",
                    Status = 404,
                };
                return NotFound(problem);
            }
            else
            {
                await _genreService.DeleteByIdAsync(id);
                return NoContent();
            }

        }
    }
}
