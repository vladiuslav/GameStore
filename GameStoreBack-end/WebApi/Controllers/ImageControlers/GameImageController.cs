using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameImageController : ControllerBase
    {
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IGameService _gameService;
        public GameImageController(IGameService gameService, IWebHostEnvironment appEnvironment)
        {
            _gameService = gameService;
            _appEnvironment = appEnvironment;
        }

        [HttpPut]
        [Consumes("multipart/form-data")]
        [Route("{gameId}")]
        public async Task<IActionResult> setImage(int gameId, [FromForm] FileUploadModel fileModel)
        {
            if (fileModel.UploadedFile != null)
            {
                var game = await _gameService.GetByIdAsync(gameId);

                if (game == null)
                {
                    var problem = new ProblemDetails
                    {
                        Title = "Game not found",
                        Detail = $"The game with ID {gameId} does not exist.",
                        Status = 404,
                    };
                    return NotFound(problem);
                }
                // take file type
                string fileName = fileModel.UploadedFile.FileName;
                int indexOfLastDot = fileName.LastIndexOf('.');
                string filetype = fileName.Remove(0, indexOfLastDot);

                // path to file
                string path = "/img/" + game.Id + filetype;

                // delete previous image if exist
                if (game.ImageUrl != null)
                {
                    try
                    {
                        System.IO.File.Delete(_appEnvironment.WebRootPath + game.ImageUrl);
                    }
                    catch
                    {
                        var problem = new ProblemDetails
                        {
                            Title = "Error deleting image",
                            Detail = $"Image replacing error.",
                            Status = 400,
                        };
                        return BadRequest(problem);
                    }
                }

                try
                {
                    // save Files in cataloge wwwroot
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await fileModel.UploadedFile.CopyToAsync(fileStream);
                        game.ImageUrl = game.Id + filetype;
                        await _gameService.UpdateAsync(game);
                    }
                }
                catch
                {
                    var problem = new ProblemDetails
                    {
                        Title = "Error deleting image",
                        Detail = $"Image creating error.",
                        Status = 400,
                    };
                    return BadRequest(problem);
                }
            }
            return Ok();
        }
        [HttpDelete]
        [Route("{gameId}")]
        public async Task<IActionResult> deleteImage(int gameId)
        {
            try
            {
                var game = await _gameService.GetByIdAsync(gameId);
                if (game == null)
                {
                    var problem = new ProblemDetails
                    {
                        Title = "Game not found",
                        Detail = $"The game with ID {gameId} does not exist.",
                        Status = 404,
                    };
                    return NotFound(problem);
                }

                game.ImageUrl = null;
                await _gameService.UpdateAsync(game);
                return NoContent();
            }
            catch (Exception ex)
            {
                var problem = new ProblemDetails
                {
                    Title = "Error deleting image",
                    Detail = $"Image error: {ex.Message}",
                    Status = 400,
                };
                return BadRequest(problem);
            }
        }
    }
}
