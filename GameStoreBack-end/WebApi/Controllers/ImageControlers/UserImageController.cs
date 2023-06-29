using BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserImageController : ControllerBase
    {
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IUserService _userService;
        public UserImageController(IUserService userService, IWebHostEnvironment appEnvironment)
        {
            _userService = userService;
            _appEnvironment = appEnvironment;
        }

        [HttpPut]
        [Authorize]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Image([FromForm] FileUploadModel fileModel)
        {
            if (fileModel.UploadedFile != null)
            {
                var email = User.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
                var user = await _userService.GetUserByEmailAsync(email);

                // take file type
                string fileName = fileModel.UploadedFile.FileName;
                int indexOfLastDot = fileName.LastIndexOf('.');
                string filetype = fileName.Remove(0, indexOfLastDot);
                // path to file
                string path = "/img/" + user.Id + filetype;

                // delete previous image if exist
                if (user.AvatarImageUrl != null)
                {
                    try
                    {
                        System.IO.File.Delete(_appEnvironment.WebRootPath + user.AvatarImageUrl);
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
                        user.AvatarImageUrl = user.Id + filetype;
                        await _userService.UpdateAsync(user);
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
        [Authorize]
        public async Task<IActionResult> deleteImage()
        {
            try
            {
                var email = User.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
                var user = await _userService.GetUserByEmailAsync(email);
                user.AvatarImageUrl = null;
                await _userService.UpdateAsync(user);
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
