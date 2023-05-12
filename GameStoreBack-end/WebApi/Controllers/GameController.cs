using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using GameStore.WebAPI.Models;
using GameStrore.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IGameService _gameService;
        private readonly ICommentService _commentService;
        public GameController(IGameService gameService, IMapper mapper , ICommentService commentService)
        {
            _gameService = gameService;
            _mapper = mapper;
            _commentService = commentService;
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

        [HttpGet("{id}/comments")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetGameComments(int id)
        {

            var game = await _gameService.GetByIdAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            var commentsFiltered = await _commentService.GetCommentsByGameIdAsync(id);
            var comments = _mapper.Map<IEnumerable<CommentViewModel>>(commentsFiltered);
            return Ok(comments);

        }

        [HttpPost]
        [ProducesResponseType(200)]
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
            if (string.IsNullOrEmpty(game.Price))
            {
                game.Price = "0";
            }
            var gameModel = _mapper.Map<GameModel>(game);
            game.ImageUrl = null;
            await _gameService.AddAsync(gameModel);

            var lastGame = _gameService.GetAllAsync().Result.Last();
            return Ok(lastGame);
       
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update([FromBody] GameViewModel game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var gameById = await _gameService.GetByIdAsync(game.Id);
            if (gameById == null)
            {
                return NotFound(game.Name);
            }

            var gameByName = await _gameService.GetByGameNameAsync(game.Name);
            if (gameByName != null && gameByName.Id != game.Id)
            {
                return BadRequest();
            }   

            var gameModel = _mapper.Map<GameModel>(game);
            gameModel.CommentsIds = gameById.CommentsIds;
            gameModel.ImageUrl = gameById.ImageUrl;
            await _gameService.UpdateAsync(gameModel);
            return Ok(game);
            
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
