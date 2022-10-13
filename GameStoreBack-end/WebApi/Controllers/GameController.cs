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
    public class GameController : Controller
    {
        private IMapper _mapper;
        private IGameService _gameService;
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            }
            );
            _mapper = new Mapper(config);
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Get()
        {
            var games = _mapper.Map<IEnumerable<GameViewModel>>(await _gameService.GetAllAsync());
            return new JsonResult(games);
        }
        [HttpGet("{id}", Name = nameof(GetGame))]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetGame(int id)
        {
            var game = _mapper.Map<GameViewModel>(await _gameService.GetByIdAsync(id));
            if (game == null)
            {
                return NotFound();
            }
            return Ok(game);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateGame(GameViewModel game)
        {
            var gameModel = _mapper.Map<GameModel>(game);
            await _gameService.AddAsync(gameModel);
            return Ok();
        }


        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(string id, [FromBody] GameViewModel game)
        {
            var gameModel = _mapper.Map<GameModel>(game);
            await _gameService.UpdateAsync(gameModel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            await _gameService.DeleteByIdAsync(id);
            return NoContent();
        }
    }
}
