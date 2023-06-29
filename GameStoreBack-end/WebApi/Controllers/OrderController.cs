using AutoMapper;
using BLL.Interfaces;
using DLL.Entities;
using GameStore.WebAPI.Models;
using GameStrore.BusinessLogic.Interfaces;
using GameStrore.BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        public IUserService _userService { get; }
        public IMapper _mapper { get; }
        public IOrderService _orderService { get; }
        public IGameService _gameService { get; }

        public IСartService _cartService { get; }
        public OrderController(IUserService userService, IMapper mapper, IOrderService orderService, IСartService cartService, IGameService gameService)
        {
            _userService = userService;
            _mapper = mapper;
            _orderService = orderService;
            _cartService = cartService;
            _gameService = gameService;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> createOrder(CreateOrderModel createOrderModel)
        {
            if (!(createOrderModel.PaymentType == "card" || createOrderModel.PaymentType == "cash"))
            {
                var problem = new ProblemDetails
                {
                    Title = "Wrong payment type",
                    Detail = $"The apyment type must be card or cash.",
                    Status = 400,
                };
                return BadRequest(problem);
            }

            var games = await _gameService.GetAllAsync();
            foreach (var cart in createOrderModel.CartModelsIds)
            {
                if (games.FirstOrDefault(g => g.Id == cart.GameId) == null)
                {
                    return BadRequest();
                }
            }
            var orderModel = _mapper.Map<OrderModel>(createOrderModel);
            await _orderService.AddAsync(orderModel);
            orderModel.Id = _orderService.GetAllAsync().Result.Last().Id;
            foreach (var cart in createOrderModel.CartModelsIds)
            {
                cart.OrderId = orderModel.Id;
                await _cartService.AddAsync(cart);
            }

            return Ok(orderModel);
        }
    }
}
