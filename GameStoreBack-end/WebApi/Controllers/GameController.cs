using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : Controller
    {
        private IMapper _mapper;
        private IGameService _gameService;
        public GameController(IGameService gameService, IMapper mapper)
        {
            _gameService = gameService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetGames()
        {
            var games = _mapper.Map<IEnumerable<GameViewModel>>(await _gameService.GetAllAsync());
            return new JsonResult(games);
        }

        [HttpGet("{id}")]
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
            if (!ModelState.IsValid)
            {

                return BadRequest();

            }
            
            if((await _gameService.GetByGameNameAsync(game.Name)) != null)
            {
                return BadRequest();
            }

            var gameModel = _mapper.Map<GameModel>(game);
            game.ImageUrl = null;
            await _gameService.AddAsync(gameModel);

            return Created(game.Name, gameModel);
       
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update([FromBody] GameViewModel game)
        {
            if (ModelState.IsValid)
            {

                var gameById = await _gameService.GetByIdAsync(game.Id);
                if (gameById == null)
                {
                    return NotFound(game.Name);
                }

                var gameByName = await _gameService.GetByGameNameAsync(game.Name);
                if (gameByName == null)
                {

                }
                else if (game.Name != null && gameByName.Id != game.Id)
                {
                    return BadRequest();
                }

                var gameModel = _mapper.Map<GameModel>(game);
                gameModel.ImageUrl = gameById.ImageUrl;
                await _gameService.UpdateAsync(gameModel);
                return Ok(game);
            }
            else {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            var game = await _gameService.GetByIdAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            else { 
                await _gameService.DeleteByIdAsync(id);
                return Ok();
            }
        }
    }
}
