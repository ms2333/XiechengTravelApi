using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using XiechengTravel.Profiles;
using XiechengTravel.Services;
using XiechengTravel.Dtos;
using Microsoft.AspNetCore.Identity;
using XiechengTravel.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using XiechengTravel.Helper;

namespace XiechengTravel.Controllers
{
    [Route("api/[controller]")]
    public class ShoppingCartController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITouristRouteRepository _touristRouteRepository;
        private readonly IMapper _mapper;
        private UserManager<ApplicationUser> _userManager;
        public ShoppingCartController(
            IHttpContextAccessor httpContextAccessor,
            ITouristRouteRepository touristRouteRepository,
            IMapper mapper,
            UserManager<ApplicationUser> userManager
            )
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _touristRouteRepository = touristRouteRepository;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetShoppingCart()
        {
            //获得当前用户 
            var user = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (user == null)
            {
                return NotFound("用户未登录");
            }
            var shoppingCart = await _touristRouteRepository.GetShoppingCartsByUserId(user);
            var shoppingCartDto = _mapper.Map<ShoppingCartDto>(shoppingCart);
            return Ok(shoppingCartDto);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AddShoppingCartItem([FromBody] AddShoppingCartItemDto addShoppingCartItemDto)
        {
            if (addShoppingCartItemDto == null)
            {
                return NotFound("TouristRouteId 不能为空");
            }
            var TouristRoute = _touristRouteRepository.GetTouristRoute(addShoppingCartItemDto.TouristRouteId);
            if (TouristRoute == null)
            {
                return NotFound("该旅游路线（商品）不存在");
            }
            var user = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (user == null)
            {
                return NotFound("用户未登录");
            }
            var userShoppingCart = await _touristRouteRepository.GetShoppingCartsByUserId(user);
            var lineItem = _mapper.Map<LineItem>(addShoppingCartItemDto);
            if (userShoppingCart == null)
            {
                await _touristRouteRepository.CreateShoppingCart(
                    new Models.ShoppingCart
                    {
                        userId = user,
                        user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User),
                        ShopingCartItems = { lineItem }
                    });
                await _touristRouteRepository.Save();
            }
            else
            {
                userShoppingCart.ShopingCartItems.Add(lineItem);
                await _touristRouteRepository.Save();
            }
            return Ok("添加成功");
        }

        [HttpPost("deleteitem/{TouristRouteId}")]
        [Authorize(AuthenticationSchemes ="Bearer")]
        public async Task<IActionResult> DeleteLineItem([FromRoute]Guid TouristRouteId)
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user ==null)
            {
                return Forbid("未登录？ 请登录");
            }
            var shoppingCart = await _touristRouteRepository.GetShoppingCartsByUserId(user.Id);
            if(shoppingCart==null)
            {
                return Ok("您的购物车为空");
            }
            var lineItem = shoppingCart.ShopingCartItems.FirstOrDefault(x => x.TouristRouteId == TouristRouteId);
            if(lineItem==null)
            {
                return NotFound("当前购物车不存在该商品");
            }
            _touristRouteRepository.DeleteShoppingCartItem(lineItem);
            var state =  await _touristRouteRepository.Save();
            if(state)
            {
                return Ok();
            }
            return NotFound("在删除数据的时候发生了意料之外的错误");
        }

        [HttpPost("deleteitem")]
        [Authorize(AuthenticationSchemes ="Bearer")]
        public async Task<IActionResult> deleteLineItems([ModelBinder(BinderType = typeof(ArrayModelBinder))][FromRoute]IEnumerable<Guid> TouristRoutes)
        {
            List<LineItem> lineItems = new List<LineItem>();
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user == null)
            {
                return Forbid("未登录？ 请登录");
            }
            var shoppingCart = await _touristRouteRepository.GetShoppingCartsByUserId(user.Id);
            if (shoppingCart == null)
            {
                return Ok("您的购物车为空");
            }
            foreach (var Tid in TouristRoutes)
            {
                var lineItem = shoppingCart.ShopingCartItems.FirstOrDefault(x => x.TouristRouteId == Tid);
                if (lineItem != null)
                {
                    lineItems.Add(lineItem);
                }
            }
            _touristRouteRepository.DeleteShoppingCartItems(lineItems);
            var state = await _touristRouteRepository.Save();
            if (state)
            {
                return Ok();
            }
            return NotFound("在删除数据的时候发生了意料之外的错误");

        }

        [HttpPost("checkout")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Checkout()
        {
            //根据UserId拿到购物车
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var shoppingCart =await _touristRouteRepository.GetShoppingCartsByUserId(user.Id);

            //创建订单
            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                OrderState = OrderStateEnum.Pending, //订单已生成
                OrderLineitems = shoppingCart.ShopingCartItems,
                CreateDateUTC = DateTime.UtcNow
            };
            await _touristRouteRepository.AddOrderAsync(order);
            shoppingCart.ShopingCartItems = null;//提交订单后清空购物车
            await _touristRouteRepository.Save();
            return Ok(_mapper.Map<OrderDto>(order));//返回订单
        }
    }
}
