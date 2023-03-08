using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BLL.Services;
using BLL.Interfaces;
using BLL.Models;
using WebApi.Models;
using DLL.Entities;

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
                return NotFound();
            }
            return Ok(genre);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateGenre(GenreViewModel genre)
        {
            if (ModelState.IsValid)
            {
                var genreByName = await _genreService.GetByGenreNameAsync(genre.Name);
                if(genreByName != null)
                {
                    return BadRequest();
                }
                var genreModel = _mapper.Map<GenreModel>(genre);
                await _genreService.AddAsync(genreModel);
                return Ok(genreModel);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update([FromBody] GenreViewModel genre)
        {
            if (ModelState.IsValid)
            {
                if ((await _genreService.GetByIdAsync(genre.Id)) == null)
                {
                    return NotFound(genre.Name);
                }

                var genreByName = await _genreService.GetByGenreNameAsync(genre.Name);
                if(genreByName==null)
                {

                }
                else if (genre.Name != null && genreByName.Id != genre.Id)
                {
                    return BadRequest();
                }

                var genreModel = _mapper.Map<GenreModel>(genre);
                await _genreService.UpdateAsync(genreModel);
                return Ok(genre);
            }
            else
            {
                return BadRequest();
            }
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
                return NotFound();
            }
            else
            {
                await _genreService.DeleteByIdAsync(id);
                return Ok("Deleted");
            }

        }
    }
}
