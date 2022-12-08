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
        private IWebHostEnvironment _appEnvironment;
        private IGameService _gameService;
        private IMapper _mapper;
        public GameImageController(IGameService gameService,IMapper mapper, IWebHostEnvironment appEnvironment)
        {
            _gameService = gameService;
            _appEnvironment = appEnvironment;
            _mapper = mapper;
        }

        [HttpPut]
        [Consumes("multipart/form-data")]
        [Route("{gameId}")]
        public async Task<IActionResult> Image(int gameId,[FromForm]FileUploadModel fileModel)
        {
            if (fileModel.UploadedFile != null)
            {
                var game = await _gameService.GetByIdAsync(gameId);

                // path to file
                string path = "/img/" + fileModel.UploadedFile.FileName;

                // delete previous image if exist
                if (game.ImageUrl != "Logo.png"&& game.ImageUrl != "nonegame.jpg" && game.ImageUrl != "noneuser.png")
                {
                    System.IO.File.Delete(_appEnvironment.WebRootPath + game.ImageUrl);
                }

                // save Files in cataloge wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await fileModel.UploadedFile.CopyToAsync(fileStream);
                    game.ImageUrl= fileModel.UploadedFile.FileName;
                    await _gameService.UpdateAsync(game);
                }
            }
            return Ok();
        }
    }
}
