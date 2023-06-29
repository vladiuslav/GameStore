using AutoMapper;
using BLL.Interfaces;
using GameStore.WebAPI.Models.GameModels;
using GameStore.WebAPI.Models.GenreModels;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchFilterControler : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISearchFilterService _searchFilterService;
        public SearchFilterControler(ISearchFilterService searchFilterService, IMapper mapper)
        {
            _searchFilterService = searchFilterService;
            _mapper = mapper;
        }

        [HttpPost("Search/{gameName}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetGamesByNames(string gameName)
        {
            if (gameName.Length < 3)
            {
                return BadRequest();
            }
            IEnumerable<GameViewModel> games;

            games = _mapper.Map<IEnumerable<GameViewModel>>(await _searchFilterService.SearchGamesByNameAsync(gameName));

            if (games == null)
            {
                return NotFound();
            }
            return Ok(games);
        }

        [HttpPost("Filter")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetGamesByFilters(GenresIdsModel genresIds)
        {
            if(!genresIds.genresIds.Any())
            {
                return BadRequest();
            }

            var games = _mapper.Map<IEnumerable<GameViewModel>>(await _searchFilterService.FilterGameByGenresAsync(genresIds.genresIds));
            if (games == null)
            {
                return NotFound();
            }
            return Ok(games);
        }
    }
}
