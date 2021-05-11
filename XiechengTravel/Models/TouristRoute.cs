using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;//做数据库限定
using System.ComponentModel.DataAnnotations.Schema;
using XiechengTravel.Dtos;

namespace XiechengTravel.Models
{
    /// <summary>
    /// 路由路线
    /// </summary>
    public class TouristRoute
    {
        [Key]//主键
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } //路线的唯一Id
        [Required]//必须的
        [MaxLength(100)]//最大长度
        public string Tittle { get; set; }//路线的名字
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }//路线的描述
        [Column(TypeName = "decimal(18,2)")]//该属性映射到数据库指定的数据类型
        public decimal OriginalPrice { get; set; }//路线的原始价格
        [Range(0.0,1.0)]//值域
        public double? DiscountPresent { get; set; }//折扣（可空）
        public DateTime CreateTime { get; set; }//创建时间
        public DateTime? UpdateTime { get; set; }//更新时间（可控）
        public DateTime? DepartureTime { get; set; }//出发时间（可空）
        [MaxLength]//直接最大值
        public string Features { get; set; }//路线特色
        [MaxLength]
        public string Fees { get; set; }//费用
        [MaxLength]
        public String Notes { get; set; }//说明

        public ICollection<TouristRoutePicture> TouristRoutePictures { get; set; }//链接属性
        //= new List<TouristRoutePicture>();//与数据库无关，避免未知错误

        //补充一些缺少的数据库字段
        public double? Rating { get; set; }//评分
        public TravelDays? TravelDays { get; set; }//天数
        public TripType? TripType { get; set; }//旅游类型
        public DepartureCity? DepartureCity { get; set; }//出发地

    }
}
