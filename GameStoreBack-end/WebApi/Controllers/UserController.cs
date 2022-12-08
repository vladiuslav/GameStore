using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IMapper _mapper;
        private IUserService _userService;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetUsers()
        {
            var users = _mapper.Map<IEnumerable<UserViewModel>>(await _userService.GetAllAsync());
            return new JsonResult(users);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetUser(int id)
        {

            var user = _mapper.Map<UserViewModel>(await _userService.GetByIdAsync(id));
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);

        }
        [HttpGet("current")]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCurrentUser()
        {

            var user = _mapper.Map<UserFullViewModel>(await _userService.GetUserByEmail(User.Identity.Name));
            return Ok(user);

        }

        [HttpPost("/login")]
        [ProducesResponseType(201)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> LoginUser(LoginData loginData)
        {
            IEnumerable<UserModel> users = await _userService.GetAllAsync();
            UserModel? user = users.FirstOrDefault(u => u.Email == loginData.Login && u.Password == loginData.Password);

            var response = new JsonResult(user);
            if (user is null)
            {
                response.StatusCode = 401;
                return response;
            }
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Email) };
            var jwt = new JwtSecurityToken(

            issuer: AuthOptions.ISSUER, audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(
                (loginData.RememberMe) ? TimeSpan.FromDays(30) : TimeSpan.FromMinutes(120)),
            signingCredentials: new SigningCredentials(
                AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);


            response = new JsonResult(new
            {
                access_token = encodedJwt,
                email = user.Email
            });
            response.StatusCode = 201;
            return response;
        }


        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateUser(UserFullViewModel user)
        {
            var userModel = _mapper.Map<UserModel>(user);
            await _userService.AddAsync(userModel);
            var response = new JsonResult(user);
            response.StatusCode = 201;
            return response;
        }

        [HttpPut()]
        [Authorize]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update([FromBody] UserViewModel user)
        {
            var userModel = _mapper.Map<UserModel>(user);
            userModel.Id = (await _userService.GetUserByEmail(User.Identity.Name)).Id;
            await _userService.UpdateAsync(userModel);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.DeleteByIdAsync(id);
            var result = new JsonResult("");
            result.StatusCode = 204;
            result.ContentType = "application/json";
            return result;
        }
    }
}
