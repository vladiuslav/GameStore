using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WebApi.Models;
using System.IO;
using DLL.Entities;

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

                // take file type
                string fileName = fileModel.UploadedFile.FileName;
                int indexOfLastDot = fileName.LastIndexOf('.');
                string filetype = fileName.Remove(0, indexOfLastDot);

                // path to file
                string path = "/img/" + game.Id + filetype;

                // delete previous image if exist
                if (game.ImageUrl != null)
                {
                    System.IO.File.Delete(_appEnvironment.WebRootPath + game.ImageUrl);
                }

                // save Files in cataloge wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await fileModel.UploadedFile.CopyToAsync(fileStream);
                    game.ImageUrl = game.Id + filetype;
                    await _gameService.UpdateAsync(game);
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
                game.ImageUrl = null;
                await _gameService.UpdateAsync(game);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
