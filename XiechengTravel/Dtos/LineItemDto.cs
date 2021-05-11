using System;
using XiechengTravel.Models;

namespace XiechengTravel.Dtos
{
    public class LineItemDto
    {
        public int Id { get; set; }
        public Guid TouristRouteId { get; set; }
        public TouristRoute TouristRoute { get; set; }//仅仅作为TouristRouteId的引用关系
        public Guid? ShoppingCartId { get; set; }
        public Guid? OrderId { get; set; }
        public decimal OriginalPrice { get; set; }//路线的原始价格
        public double? DiscountPresent { get; set; }//折扣（可空）
    }
}