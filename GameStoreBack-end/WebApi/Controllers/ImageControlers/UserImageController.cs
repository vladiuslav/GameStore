using AutoMapper;
using BLL.Interfaces;
using DLL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserImageController : ControllerBase
    {
        private IWebHostEnvironment _appEnvironment;
        private IUserService _userService;
        private IMapper _mapper;
        public UserImageController(IUserService userService, IMapper mapper, IWebHostEnvironment appEnvironment)
        {
            _userService = userService;
            _appEnvironment = appEnvironment;
            _mapper = mapper;
        }

        [HttpPut]
        [Authorize]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Image( [FromForm] FileUploadModel fileModel)
        {
            if (fileModel.UploadedFile != null)
            {
                var user = await _userService.GetUserByEmail(User.Identity.Name);

                // path to file
                string path = "/img/" + fileModel.UploadedFile.FileName;

                // delete previous image if exist
                if (user.AvatarImageUrl != "Logo.png" && user.AvatarImageUrl != "noneuser.jpg" && user.AvatarImageUrl != "nonegame.jpg")
                {
                    System.IO.File.Delete(_appEnvironment.WebRootPath + user.AvatarImageUrl);
                }

                // save Files in cataloge wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await fileModel.UploadedFile.CopyToAsync(fileStream);
                    user.AvatarImageUrl = fileModel.UploadedFile.FileName;
                    await _userService.UpdateAsync(user);
                }
            }
            return Ok();
        }
    }
}
