using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XiechengTravel.Services;
using AutoMapper;
using XiechengTravel.Dtos;
using System.Text.RegularExpressions;
using XiechengTravel.ResourceParameters;
using XiechengTravel.Models;
using Microsoft.AspNetCore.JsonPatch;
using XiechengTravel.Helper;
using Microsoft.Scripting.Runtime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace XiechengTravel.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TouristRoutesController : ControllerBase //因为项目中仅仅使用api而不需使用view等页面所以没有使用（Controller）
    {
        //注入服务
        private ITouristRouteRepository _touristRouteRepository;
        private IMapper _mapper;
        private IUrlHelper _urlHelper;

        public TouristRoutesController(
            ITouristRouteRepository touristRouteRepository,
            IMapper mapper,
            IActionContextAccessor actionContextAccessor,
            IUrlHelperFactory urlHelperFactory
            )
        {
            this._touristRouteRepository = touristRouteRepository;
            this._mapper = mapper;
            this._urlHelper = urlHelperFactory.GetUrlHelper(actionContextAccessor.ActionContext);
        }

        private string GenerateTouristRouteResourceURL(
            TouristRouteResourceParamaters paramaters,
            PaginationResourceParamaters paramaters2,
            ResourceUriType type
        )
        {
            //return type switch
            //{
            //    ResourceUriType.PreviousPage => _urlHelper.Link("GetTouristRoutes",
            //        new
            //        {
            //            keyword = paramaters.Keyword,
            //            rating = paramaters.Rating,
            //            pageNumber = paramaters2.PageNumber - 1,
            //            pageSize = paramaters2.PageSize
            //        }),
            //    ResourceUriType.NextPage => _urlHelper.Link("GetTouristRoutes",
            //        new
            //        {
            //            keyword = paramaters.Keyword,
            //            rating = paramaters.Rating,
            //            pageNumber = paramaters2.PageNumber + 1,
            //            pageSize = paramaters2.PageSize
            //        }),
            //    _ => _urlHelper.Link("GetTouristRoutes",
            //        new
            //        {
            //            keyword = paramaters.Keyword,
            //            rating = paramaters.Rating,
            //            pageNumber = paramaters2.PageNumber,
            //            pageSize = paramaters2.PageSize
            //        })
            //};

            var result = _urlHelper.Link("GetTouristRoutes",
                    new
                    {
                        keyword = paramaters.Keyword,
                        rating = paramaters.Rating,
                        pageNumber = paramaters2.PageNumber + 1,
                        pageSize = paramaters2.PageSize
                    });

            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return _urlHelper.Link("GetRoutistRoutes", new
                    {
                        keyword = paramaters.Keyword,
                        rating = paramaters.Rating,
                        pageNumber = paramaters2.PageNumber - 1,
                        pageSize = paramaters2.PageSize
                    });
                case ResourceUriType.NextPage:
                    return _urlHelper.Link("GetTouristRoutes",
                    new
                    {
                        keyword = paramaters.Keyword,
                        rating = paramaters.Rating,
                        pageNumber = paramaters2.PageNumber + 1,
                        pageSize = paramaters2.PageSize
                    });
                default:
                   return _urlHelper.Link("GetTouristRoutes",
                   new
                   {
                       keyword = paramaters.Keyword,
                       rating = paramaters.Rating,
                       pageNumber = paramaters2.PageNumber,
                       pageSize = paramaters2.PageSize
                   });
            }
        }

        [HttpGet("GetTouristRoutes")]//http://localhost:5000/api/TouristRoutes?keyword=埃及&ratingValue=largerThan4
        [HttpHead]
        [Authorize(AuthenticationSchemes ="Bearer")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetTouristRoutes(
            [FromQuery]TouristRouteResourceParamaters paramaters,//lessThan,largerThan,equalTo ====>lessThan3,largerThan4,equalTo1
            [FromQuery] PaginationResourceParamaters paramaters2
            )
        {
            var touristRoutesFromRepo = await _touristRouteRepository.GetTouristRoutes(
                paramaters.Keyword, paramaters.RatingOperatorType, paramaters.RatingValue,
                paramaters2.PageNumber,paramaters2.PageSize);
            if (touristRoutesFromRepo == null || touristRoutesFromRepo.Count() <= 0)
            {
                return NotFound("您所查找的旅游路线图不存在，如有问题请拨打客服电话：110");
            }
            var touristRoutesDto = _mapper.Map<IEnumerable<TouristRouteDto>>(touristRoutesFromRepo);

            //header添加上下页导航信息
            var previousPageLink = touristRoutesFromRepo.HasPrevious
              ? GenerateTouristRouteResourceURL(
                  paramaters, paramaters2, ResourceUriType.PreviousPage)
              : null;

            //var nextPageLink = touristRoutesFromRepo.HasNext
            //    ? GenerateTouristRouteResourceURL(
            //        paramaters, paramaters2, ResourceUriType.NextPage)
            //    : null;

            var nextPageLink = GenerateTouristRouteResourceURL(paramaters, paramaters2, ResourceUriType.NextPage);

            // x-pagination
            var paginationMetadata = new
            {
                previousPageLink,
                nextPageLink,
                totalCount = touristRoutesFromRepo.TotalCount,
                pageSize = touristRoutesFromRepo.PageSize,
                currentPage = touristRoutesFromRepo.CurrentPage,
                totalPages = touristRoutesFromRepo.TotalPages
            };
            //添加到头部响应
            Response.Headers.Add("x-pagination",
                Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));
            return  Ok(touristRoutesDto);
        }

        [HttpGet]//http://localhost:5000/api/TouristRoutes/99BA5433-DF5F-A898-C8E0-78B8BA55F251
        [Route("{touristRouteId:Guid}",Name = "GetTouristRouteById")]//(:Guid确保传进来的数据为Guid)
        public IActionResult GetTouristRouteById(Guid touristRouteId)
        {
            var touristRouteFromRepo = _touristRouteRepository.GetTouristRoute(touristRouteId);
            if (touristRouteFromRepo == null)
            {
                return NotFound($"没有查询到该路由路线图，请检查该Id:{touristRouteId}的路线图是否存在，或者联系管理员");
            }
            var touristRouteDto = _mapper.Map<TouristRouteDto>(touristRouteFromRepo);
            return Ok(touristRouteDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTouristRoute([FromBody] TouristRouteForCreationDto touristRouteForCreationgDto)
        {
            var touristRoute = _mapper.Map<TouristRoute>(touristRouteForCreationgDto);
            if (touristRoute != null)
            {
                _touristRouteRepository.AddTouristRoute(touristRoute);
            }
            if(await _touristRouteRepository.Save())
            {
                var touristRouteReture = _mapper.Map<TouristRouteDto>(touristRoute);
                //信息展示页面
                return CreatedAtRoute("GetTouristRouteById", new { touristRouteId = touristRouteReture.Id },touristRouteReture);
            }
            return NotFound();
        }

        [HttpPut]
        [Route("{touristRouteId:Guid}")]
        public async Task<IActionResult> UpdateTouristRouet(
            [FromRoute] Guid TouristRouteId,
            [FromBody] TouristRouteForUpdateDto touristRouteForUpdateDto)
        {
            if(!_touristRouteRepository.TouristRouteExists(TouristRouteId))
            {
                return NotFound("没有找到旅游路线");
            }
            var touristRoute = _touristRouteRepository.GetTouristRoute(TouristRouteId);
            //在这里Map（）会直接将传进来的对象映射到TouristRoute的Entity instance上，我们只需要.save（）提交一下即可；
            var touristRoutePut = _mapper.Map(touristRouteForUpdateDto, touristRoute);
            await _touristRouteRepository.Save();
            return NoContent();
        }

        [HttpPatch]
        [Route("{touristRouteId:Guid}")]
        public async Task<IActionResult> PartialUpdateTouristRoute(
            [FromRoute] Guid TouristRouteId,
            [FromBody] JsonPatchDocument<TouristRouteForUpdateDto> jsonPatchDocument
            )
        {
            if(!_touristRouteRepository.TouristRouteExists(TouristRouteId))
            {
                return NotFound("要修改的数据不存在");
            }
            var touristRoute = _touristRouteRepository.GetTouristRoute(TouristRouteId);
            var touristRoutePatch = _mapper.Map<TouristRouteForUpdateDto>(touristRoute);
            jsonPatchDocument.ApplyTo(touristRoutePatch,ModelState);
            if (TryValidateModel(touristRoutePatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(touristRoutePatch, touristRoute);
            await _touristRouteRepository.Save();
            return NoContent();
        }

        [HttpDelete]
        [Route("{touristRouteId:Guid}")]
        public async Task<IActionResult> DeleteTouristRoute(
            [FromRoute]Guid TouristRouteId)
        {
            if(!_touristRouteRepository.TouristRouteExists(TouristRouteId))
            {
                return NotFound("要删除的数据不存在");
            }

            _touristRouteRepository.DeleteTouristRoute(TouristRouteId);
            await _touristRouteRepository.Save();
            return Ok("删除成功");
        }
        /// <summary>
        /// 这里需要自定义Model绑定------------------------未完成
        /// </summary>
        /// <param name="touristRouteIds"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{touristRouteIds}")]
        public async Task<IActionResult> DeleteTouristRoutes(
            [ModelBinder(BinderType=typeof(ArrayModelBinder))]
        [FromRoute]IEnumerable<Guid> touristRouteIds
            ) 
        {
            int ErrCount = 0;
            if(touristRouteIds==null)
            {
                return BadRequest("请求参数不能为空");
            }
            
            foreach (var item in touristRouteIds)
            {
                if (_touristRouteRepository.TouristRouteExists(item))
                {
                    ErrCount++;
                }
            }
           var DeleteList = await _touristRouteRepository.GetTouristRoutesByList(touristRouteIds.ToList());
            _touristRouteRepository.DeleteTouristRoutes(DeleteList);
            await _touristRouteRepository.Save();
            return Ok($"成功删除{ErrCount}条数据，失败{touristRouteIds.Count()-ErrCount}条数据");
        }
    }
}
