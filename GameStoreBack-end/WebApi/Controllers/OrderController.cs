using AutoMapper;
using BLL.Interfaces;
using GameStrore.BusinessLogic.Interfaces;
using GameStrore.BusinessLogic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.Models;

namespace GameStore.WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        public IUserService _userService { get; }
        public IMapper _mapper { get; }
        public IOrderService _orderService { get; }
        public OrderController(IUserService userService, IMapper mapper, IOrderService orderService)
        {
            _userService = userService;
            _mapper = mapper;
            _orderService = orderService;
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> createOrder(OrderModel orderModel)
        {
            if (!ModelState.IsValid||
                (orderModel.PaymentType != "card" || orderModel.PaymentType != "cash"))
            {
                BadRequest();
            }
            var email = User.Claims.First(claim => claim.Type == ClaimTypes.Email).Value;
            var user = await _userService.GetUserByEmailAsync(email);
            if (user.CommentsIds==null) {
                return BadRequest();
            }

            orderModel.CartItemIds = user.CommentsIds;
            await _orderService.AddAsync(orderModel);

            return Ok(orderModel);
        }
    }
}
