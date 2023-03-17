﻿using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DLL.Entities;
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
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly TokenValidationParameters _tokenValidationParameters;
        public UserController(IUserService userService, IMapper mapper, IRefreshTokenService refreshTokenService, TokenValidationParameters tokenValidationParameters)
        {
            _userService = userService;
            _refreshTokenService = refreshTokenService;
            _mapper = mapper;
            _tokenValidationParameters = tokenValidationParameters;
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

            var email = User.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
            var user = _mapper.Map<UserFullViewModel>(await _userService.GetUserByEmailAsync(email));
            return Ok(user);

        }

        [HttpPost("/refreshToken")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> refreshToken(AuthResultViewModel tokenRequestVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await VerifyAndGenerateTokenAsync(tokenRequestVM);
            if (result == null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        private async Task<AuthResultViewModel> VerifyAndGenerateTokenAsync(AuthResultViewModel tokenRequestVM)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var storedToken = await _refreshTokenService.GetTokenByToken(tokenRequestVM.RefreshToken);
            if (storedToken == null)
            {
                return null;
            }
            

            var dbUser = await _userService.GetByIdAsync(storedToken.UserId);

            try
            {
                jwtTokenHandler.ValidateToken(tokenRequestVM.Token, _tokenValidationParameters, out var validatedToken);

                return await GenerateJWTTokenAsync(dbUser, storedToken);
            }
            catch (SecurityTokenExpiredException)
            {
                if (storedToken.DateExpire >= DateTime.UtcNow)
                {
                    return await GenerateJWTTokenAsync(dbUser, storedToken);
                }
                else
                {
                    return await GenerateJWTTokenAsync(dbUser, null);
                }
            }
        }

        [HttpPost("/login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Login(LoginData loginVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please, provide all required fields");
            }

            var user = await _userService.GetUserByEmailAsync(loginVM.Email);
            if (user != null && loginVM.Password == user.Password)
            {
                var tokenValue = await GenerateJWTTokenAsync(user, null);
                return Ok(tokenValue);
            }
            return Unauthorized();
        }

        private async Task<AuthResultViewModel> GenerateJWTTokenAsync(UserModel user, RefreshToken rToken)
        {
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                expires: DateTime.UtcNow.AddMinutes(5),
                claims: authClaims,
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            if (rToken != null)
            {
                var rTokenResponse = new AuthResultViewModel()
                {
                    Token = jwtToken,
                    RefreshToken = rToken.Token,
                    ExpiresAt = token.ValidTo
                };
                return rTokenResponse;
            }

            var refreshToken = new RefreshToken()
            {
                JwtId = token.Id,
                IsRevoked = false,
                UserId = user.Id,
                DateAdded = DateTime.UtcNow,
                DateExpire = DateTime.UtcNow.AddMonths(6),
                Token = Guid.NewGuid().ToString() + "-" + Guid.NewGuid().ToString()
            };

            await _refreshTokenService.AddAsync(refreshToken);

            var response = new AuthResultViewModel()
            {
                Token = jwtToken,
                RefreshToken = refreshToken.Token,
                ExpiresAt = token.ValidTo
            };

            return response;

        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateUser(UserFullViewModel user)
        {
            if (ModelState.IsValid)
            {
                var userByEmail = await _userService.GetUserByEmailAsync(user.Email);
                if (userByEmail != null)
                {
                    return BadRequest();
                }

                var userByName = await _userService.GetUserByUserNameAsync(user.UserName);
                if (userByName != null)
                {
                    return BadRequest();
                }

                var userModel = _mapper.Map<UserModel>(user);
                userModel.AvatarImageUrl = null;
                await _userService.AddAsync(userModel);
                var response = new JsonResult(user);
                response.StatusCode = 200;
                return response;
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut()]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update([FromBody] UserFullViewModel user)
        {
            if (ModelState.IsValid)
            {
                var email = User.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
                var userByIdentity = await _userService.GetUserByEmailAsync(email);

                var userByEmail = await _userService.GetUserByEmailAsync(user.Email);
                if(userByEmail!= null&& userByEmail.Id != userByIdentity.Id)
                {
                    return BadRequest();
                }

                var userByName = await _userService.GetUserByUserNameAsync(user.UserName);
                if (userByName != null && userByName.Id != userByIdentity.Id)
                {
                    return BadRequest();
                }

                var userModel = _mapper.Map<UserModel>(user);
                userModel.Id = userByIdentity.Id;
                userModel.AvatarImageUrl = userByIdentity.AvatarImageUrl;
                await _userService.UpdateAsync(userModel);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
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
