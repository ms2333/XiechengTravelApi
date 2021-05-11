using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using XiechengTravel.Dtos;

namespace XiechengTravel.ValidationAttributes
{
    /// <summary>
    /// 类级别的数据验证
    /// </summary>
    public class TouristRouteTitleMustBeDifferentFromDescriptionAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var touristRouteDto =(TouristRouteForCreationDto) validationContext.ObjectInstance;
            if (touristRouteDto.Tittle == touristRouteDto.Description)
            {
                return new ValidationResult("路线名称必须与路线描述不同", new[] { "TouristRouteForCreationDto" });
            }
            return ValidationResult.Success;
        }
    }
}
