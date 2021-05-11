using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XiechengTravel.Dtos;
using XiechengTravel.Models;

namespace XiechengTravel.Profiles
{
    public class TouristRoutePicProfile:Profile
    {
        public TouristRoutePicProfile()
        {
            CreateMap<TouristRoutePicture, TouristRoutePictureDto>();
            CreateMap<TouristRouteForCreationPicDto, TouristRoutePicture>();
            CreateMap<TouristRoutePicture, TouristRouteForCreationPicDto>();
        }
    }
}
