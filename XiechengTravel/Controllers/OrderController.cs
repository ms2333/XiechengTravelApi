using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XiechengTravel.Dtos;
using XiechengTravel.Models;
using XiechengTravel.Services;

namespace XiechengTravel.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ITouristRouteRepository _touristRouteRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public OrderController(ITouristRouteRepository touristRouteRepository,UserManager<ApplicationUser> userManager,IMapper mapper)
        {
            _touristRouteRepository = touristRouteRepository;
            _userManager = userManager;
            _mapper = mapper;
        }
        /// <summary>
        /// get all of the order by userId
        /// </summary>
        /// <returns></returns>
        [HttpGet("getorders/{userId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetOrdersByUserId([FromRoute]string userId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return Forbid("用户未登录");
            }
            var orders =await _touristRouteRepository.GetOrdersByUserId(userId);
            return Ok(_mapper.Map<IEnumerable<OrderDto>>(orders));
        }

        /// <summary>
        /// 获得详细的订单信息
        /// </summary>
        /// <param name="OrderId"></param>
        /// <returns></returns>
        [HttpGet("getorderInfo/{OrderId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> GetOrderInfo([FromRoute]Guid OrderId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return Forbid("用户未登录");
            }
            var order = await _touristRouteRepository.GetOrderInfo(OrderId);
            if (order.UserId == user.Id)
            {
                return Ok(order);
            }
            return NoContent();
        }

        [HttpDelete("delete/{OrderId}")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeleteOrderByOrderId([FromRoute]Guid OrderId)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return Forbid("用户未登录");
            }
            var order = await _touristRouteRepository.GetOrdersByUserId(user.Id);
            if (order.FirstOrDefault(x => x.Id == OrderId).UserId == user.Id)
            {
                await _touristRouteRepository.deleteOrder(OrderId);
                await _touristRouteRepository.Save();
                return Ok();
            }
            return NotFound();
        }

        //**************************************

        //  ------------->不好使~

        //**************************************
        [HttpDelete("deleteall")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeleteAllOfTheOrders()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            _touristRouteRepository.deleteAllOrders(user.Id);
            await _touristRouteRepository.Save();
            return Ok();
        }
    }
}
