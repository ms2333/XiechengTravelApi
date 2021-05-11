using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using XiechengTravel.Dtos;
using XiechengTravel.Models;

namespace XiechengTravel.Profiles
{
    public class ShoppingCartDtoProfile:Profile
    {
        public ShoppingCartDtoProfile()
        {
            CreateMap<ShoppingCart, ShoppingCartDto>();
            CreateMap<AddShoppingCartItemDto, LineItem>();
        }
    }
}
