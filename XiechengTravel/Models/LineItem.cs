using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace XiechengTravel.Models
{
    /// <summary>
    /// 价格模块
    /// </summary>
    public class LineItem
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("TouristRouteId")]
        public Guid TouristRouteId { get; set; }
        public TouristRoute TouristRoute { get; set; }//仅仅作为TouristRouteId的引用关系
        public Guid? ShoppingCartId { get; set; }
        public Guid? OrderId { get; set; }
        [Column(TypeName = "decimal(18,2)")]//该属性映射到数据库指定的数据类型
        public decimal OriginalPrice { get; set; }//路线的原始价格
        [Range(0.0, 1.0)]//值域
        public double? DiscountPresent { get; set; }//折扣（可空）
    }
}
