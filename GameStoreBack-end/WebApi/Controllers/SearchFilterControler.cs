using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BLL.Services;
using BLL.Interfaces;
using BLL.Models;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchFilterControler : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISearchFilterService _searchFilterService ;
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
            var games = _mapper.Map<IEnumerable<GameViewModel>>(await _searchFilterService.FilterGameByGenresAsync(genresIds.genresIds));
            if (games == null)
            {
                return NotFound();
            }
            return Ok(games);
        }
    }
}
