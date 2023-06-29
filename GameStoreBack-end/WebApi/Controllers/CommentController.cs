using AutoMapper;
using BLL.Interfaces;
using BLL.Services;
using GameStore.WebAPI.Models.CommentModels;
using GameStore.WebAPI.Models.UserModels;
using GameStrore.BusinessLogic.Interfaces;
using GameStrore.BusinessLogic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameStore.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICommentService _commentService;
        private readonly IGameService _gameService;
        private readonly IUserService _userService;
        public CommentController(IMapper mapper,ICommentService commentService, IGameService gameService, IUserService userService)
        {
            _mapper = mapper;
            _commentService = commentService;
            _gameService = gameService;
            _userService = userService;
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCommentsById(int id)
        {

            var comment = await _commentService.GetByIdAsync(id);
            if (comment == null)
            {
                var problem = new ProblemDetails
                {
                    Title = "Comment not found",
                    Detail = $"The comment with ID {id} does not exist.",
                    Status = 404,
                };
                return NotFound(problem);
            }

            return Ok(comment);

        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(201)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> AddComment([FromBody] CommentCreateModel comment)
        {
            
            var game = await _gameService.GetByIdAsync(comment.GameId);
            if (game == null)
            {
                var problem = new ProblemDetails
                {
                    Title = "Game for comment not found",
                    Detail = $"The game with ID {comment.GameId} does not exist.",
                    Status = 404,
                };
                return NotFound(problem);
            }

            var email = User.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
            var user = _mapper.Map<UserFullViewModel>(await _userService.GetUserByEmailAsync(email));

            var commentModel = _mapper.Map<CommentModel>(comment);
            commentModel.UserId = user.Id;
            commentModel.GameId = comment.GameId;
            commentModel.Created = DateTime.UtcNow;

            if (comment.RepliedCommentId.GetValueOrDefault() != 0)
            {
                var Repliedcomment = await _commentService.GetByIdAsync(commentModel.RepliedCommentId.GetValueOrDefault());
                if (Repliedcomment == null)
                {
                    var problem = new ProblemDetails
                    {
                        Title = "Replied comment not found",
                        Detail = $"The comment with ID {comment.RepliedCommentId} does not exist.",
                        Status = 404,
                    };
                    return NotFound(problem);
                }
            }
            else
            {
                commentModel.RepliedCommentId = null;
            }

            await _commentService.AddAsync(commentModel);

            return Ok(commentModel);

        }
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _commentService.GetByIdAsync(id);
            if (comment == null)
            {
                var problem = new ProblemDetails
                {
                    Title = "Comment not found",
                    Detail = $"The comment with ID {id} does not exist.",
                    Status = 404,
                };
                return NotFound(problem);
            }

            var email = User.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
            var user = _mapper.Map<UserFullViewModel>(await _userService.GetUserByEmailAsync(email));
            if (comment.UserId != user.Id)
            {
                var problem = new ProblemDetails
                {
                    Title = "Comment not created by this user",
                    Detail = $"The comment with ID {comment.Id} not created by current user {user.Id}",
                    Status = 400,
                };
                return BadRequest(problem);
            }

            await _commentService.DeleteByIdAsync(id);
            return NoContent();
        }

        [HttpPut]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> UpdateComment(CommentUpdateModel comment)
        {

            var commentModel = await _commentService.GetByIdAsync(comment.Id);
            if (commentModel == null)
            {
                var problem = new ProblemDetails
                {
                    Title = "Comment not found",
                    Detail = $"The comment with ID {comment.Id} does not exist.",
                    Status = 404,
                };
                return NotFound(problem);
            }

            var email = User.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
            var user = _mapper.Map<UserFullViewModel>(await _userService.GetUserByEmailAsync(email));
            if (commentModel.UserId != user.Id)
            {
                var problem = new ProblemDetails
                {
                    Title = "Comment not created by this user",
                    Detail = $"The comment with ID {comment.Id} not created by current user {user.Id}",
                    Status = 400,
                };
                return BadRequest(problem);
            }

            commentModel.Text = comment.Text;
            await _commentService.UpdateAsync(commentModel);

            return Ok(commentModel);
        }
    }
}
