using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using XiechengTravel.ValidationAttributes;

namespace XiechengTravel.Dtos
{
    public class TouristRouteForUpdateDto
    {
        [Required(ErrorMessage = "Tittle不可以为空值")]
        public string Tittle { get; set; }//路线的名字
        public string Description { get; set; }//
                                               //面向消费者的价格：原价*折扣
        public decimal OriginalPrice { get; set; }//路线的原始价格
        public double? DiscountPresent { get; set; }//内部折扣（可空） 消费者不应该看到的
        public DateTime CreateTime { get; set; }//创建时间
        public DateTime? UpdateTime { get; set; }//更新时间（可控）
        public DateTime? DepartureTime { get; set; }//出发时间（可空）
        public string Features { get; set; }//路线特色
        public string Fees { get; set; }//费用
        public String Notes { get; set; }//说明
                                         //补充一些缺少的数据库字段
        public double? Rating { get; set; }//评分
        public String TravelDays { get; set; }//天数
        public string TripType { get; set; }//旅游类型
        public String DepartureCity { get; set; }//出发地

        //这里建立的集合里包含数据的关系，就是来自efcore的include方法
        //这里的属性名称一定要和模型中的一摸一样
        public ICollection<TouristRouteForCreationPicDto> TouristRoutePictures { get; set; }//链接属性
    }
}
