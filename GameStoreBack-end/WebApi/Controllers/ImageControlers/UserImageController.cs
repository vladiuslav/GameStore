using AutoMapper;
using BLL.Interfaces;
using BLL.Services;
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
                var user = await _userService.GetUserByEmailAsync(User.Identity.Name);

                // take file type
                string fileName = fileModel.UploadedFile.FileName;
                int indexOfLastDot = fileName.LastIndexOf('.');
                string filetype = fileName.Remove(0, indexOfLastDot);
                // path to file
                string path = "/img/" + user.Id + filetype;

                // delete previous image if exist
                if (user.AvatarImageUrl != null)
                {
                    System.IO.File.Delete(_appEnvironment.WebRootPath + user.AvatarImageUrl);
                }

                // save Files in cataloge wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await fileModel.UploadedFile.CopyToAsync(fileStream);
                    user.AvatarImageUrl = user.Id + filetype;
                    await _userService.UpdateAsync(user);
                }
            }
            return Ok();
        }
        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> deleteImage()
        {
            try
            {
                var user = await _userService.GetUserByEmailAsync(User.Identity.Name);
                user.AvatarImageUrl = null;
                await _userService.UpdateAsync(user);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
