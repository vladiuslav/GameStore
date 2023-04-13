using AutoMapper;
using BLL.Interfaces;
using GameStrore.BusinessLogic.Interfaces;
using GameStrore.BusinessLogic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : Controller
    {
        public IUserService _userService { get; }
        public IMapper _mapper { get; }
        public IСartService _cartService { get; }
        public CartController(IUserService userService, IMapper mapper, IСartService cartService)
        {
            _userService = userService;
            _mapper = mapper;
            _cartService = cartService;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetUserCart()
        {
            var email = User.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
            var user = _mapper.Map<UserFullViewModel>(await _userService.GetUserByEmailAsync(email));
            
            var carts = _cartService.GetUserCart(user.Id);
            
            return Ok(carts);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> AddGameToUserCart(int gameId)
        {
            var email = User.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
            var user = _mapper.Map<UserFullViewModel>(await _userService.GetUserByEmailAsync(email));

            CartModel cart = new CartModel { GameId= gameId,UserId=user.Id,Quantity=1 };
            await _cartService.AddAsync(cart);

            return Ok(cart);
        }

        [HttpPut]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> ChangeUserCartItem(int cartId,int quantity)
        {
            if (quantity <= 0)
            {
                return BadRequest();
            }
            var email = User.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
            var user = _mapper.Map<UserFullViewModel>(await _userService.GetUserByEmailAsync(email));
            
            var cart = await _cartService.GetByIdAsync(cartId);
            if (cart == null)
            {
                return NotFound();
            }
            if(user.Id!= cart.Id)
            {
                return BadRequest();
            }

            cart.Quantity = quantity;
            await _cartService.UpdateAsync(cart);

            return Ok(cart);

        }

        [HttpDelete]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> RemoveFromUserCart(int cartId)
        {
            var email = User.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
            var user = _mapper.Map<UserFullViewModel>(await _userService.GetUserByEmailAsync(email));

            var cart = await _cartService.GetByIdAsync(cartId);
            if (cart == null)
            {
                return NotFound();
            }

            if (user.Id != cart.Id)
            {
                return BadRequest();
            }

            await _cartService.DeleteByIdAsync(cartId);

            return Ok();

        }
    }
}
