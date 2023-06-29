using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using GameStore.WebAPI.Models.CommentModels;
using GameStore.WebAPI.Models.GameModels;
using GameStrore.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
                var problem = new ProblemDetails
                {
                    Title = "Game not found",
                    Detail = $"The game with ID {id} does not exist.",
                    Status = 404,
                };
                return NotFound(problem);
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
                var problem = new ProblemDetails
                {
                    Title = "Game not found",
                    Detail = $"The game with ID {id} does not exist.",
                    Status = 404,
                };
                return NotFound(problem);
            }

            var commentsFiltered = await _commentService.GetCommentsByGameIdAsync(id);
            return Ok(commentsFiltered);

        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateGame(GameCreateModel game)
        {
            if((await _gameService.GetByGameNameAsync(game.Name)) != null)
            {
                var problem = new ProblemDetails
                {
                    Title = "Game already exist",
                    Detail = $"The game with name {game.Name} already exist.",
                    Status = 400,
                };
                return BadRequest(problem);
            }
            if (string.IsNullOrEmpty(game.Price))
            {
                game.Price = "0";
            }
            var gameModel = _mapper.Map<GameModel>(game);
            gameModel.ImageUrl = null;
            await _gameService.AddAsync(gameModel);

            var lastGame = _gameService.GetAllAsync().Result.Last();
            return Ok(lastGame);
            
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(GameUpdateModel game)
        {
            var gameById = await _gameService.GetByIdAsync(game.Id);
            if (gameById == null)
            {
                var problem = new ProblemDetails
                {
                    Title = "Game not found",
                    Detail = $"The game with ID {game.Id} does not exist.",
                    Status = 404,
                };
                return NotFound(problem);
            }

            var gameByName = await _gameService.GetByGameNameAsync(game.Name);
            if (gameByName != null && gameByName.Id != game.Id)
            {
                var problem = new ProblemDetails
                {
                    Title = "Game already exist",
                    Detail = $"The game with name {game.Name} already exist.",
                    Status = 400,
                };
                return BadRequest(problem);
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
                var problem = new ProblemDetails
                {
                    Title = "Game not found",
                    Detail = $"The game with ID {id} does not exist.",
                    Status = 404,
                };
                return NotFound(problem);
            }
            else { 
                await _gameService.DeleteByIdAsync(id);
                return NoContent();
            }
        }
    }
}
