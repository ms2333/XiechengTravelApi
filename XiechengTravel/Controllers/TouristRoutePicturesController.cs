using AutoMapper;
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
    [ApiController]
    [Route("api/touristRoutes/{touristRouteId}/pictures")]
    public class TouristRoutePicturesController : ControllerBase
    {
        private ITouristRouteRepository _touristRouteRepository;
        private IMapper _mapper;
        public TouristRoutePicturesController(ITouristRouteRepository touristRouteRepository, IMapper mapper)
        {
            this._touristRouteRepository = touristRouteRepository ?? throw new Exception(nameof(touristRouteRepository));
            this._mapper = mapper ?? throw new Exception(nameof(mapper));
        }


        //http://localhost:5000/api/TouristRoutes/99ba5433-df5f-a898-c8e0-78b8ba55f251/pictures
        [HttpGet]
        public async Task<IActionResult> GetPictureListForTouristRouteAsync(Guid touristRouteId)
        {
            if (!_touristRouteRepository.TouristRouteExists(touristRouteId))
            {
                 return NotFound("旅游路线不存在");
            }
            var TouristRoutePictures = await _touristRouteRepository.GetPicturesByTouristRouteId(touristRouteId);//异步取回数据
             var TouristRoutePicturesDto = _mapper.Map<IEnumerable<TouristRoutePictureDto>>(TouristRoutePictures);
             return Ok(TouristRoutePicturesDto);
        }

        //http://localhost:5000/api/TouristRoutes/99ba5433-df5f-a898-c8e0-78b8ba55f251/pictures/4
        [HttpGet]
        [Route("{picId}")]
        public IActionResult GetPicture([FromRoute]Guid touristRouteId, int picId)//获取子数据时确认父数据存在否则无意义
        {
            if (!_touristRouteRepository.TouristRouteExists(touristRouteId))
            {
                return NotFound("旅游路线不存在");
            }
            var TouristPicture = _touristRouteRepository.GetPicture(picId);
            var TouristPictureDto = _mapper.Map<TouristRoutePictureDto>(TouristPicture);
            //如果查询的picId的路线图与输入的路线图id不匹配，拒绝返回数据
            if (TouristPicture.TouristRouteId == touristRouteId)
            {
                return Ok(TouristPictureDto);
            }
            return NotFound("您所查找的旅游路线图不存在，或者路由路线有误，请联系管理员");

        }

        [HttpPost]
        public async Task<IActionResult> CreateTouristRoutePictures(
            [FromRoute] Guid touristRouteId,
            [FromBody] TouristRouteForCreationPicDto touristRouteForCreationPicDto
            )
        {
            if (!_touristRouteRepository.TouristRouteExists(touristRouteId))
            {
                return NotFound(); 
            }

            var pictureModel = _mapper.Map<TouristRoutePicture>(touristRouteForCreationPicDto);
            _touristRouteRepository.AddTouristRoutePicture(touristRouteId, pictureModel);
            await _touristRouteRepository.Save();

            var pictureDto = _mapper.Map<TouristRoutePictureDto>(pictureModel);
            return CreatedAtAction(nameof(GetPicture), new { touristRouteId = touristRouteId, picId = pictureModel.Id },pictureDto);
        }
    }
}
