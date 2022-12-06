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
        public GameController(IGameService gameService,IMapper mapper)
        {
            _gameService = gameService;
            _mapper = mapper;
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
            var response = new JsonResult(game);
            response.StatusCode = 201;
            return response;
        }


        //genres ids 
        [HttpPut("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update([FromRoute]int id, [FromBody] GameViewModel game)
        {
            var gameModel = _mapper.Map<GameModel>(game);
            gameModel.Id = id;
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
            var result = new JsonResult("");
            result.StatusCode=204;
            result.ContentType="application/json";
            return result;
        }
    }
}
