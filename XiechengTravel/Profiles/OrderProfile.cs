using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XiechengTravel.Dtos;
using XiechengTravel.Models;

namespace XiechengTravel.Profiles
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>().ForMember(
                destinationMember: dest => dest.OrderState,
                memberOptions: opt => {
                    opt.MapFrom(src => src.OrderState.ToString());
                }
                );
            CreateMap<LineItem, LineItemDto>();
        }
    }
}
