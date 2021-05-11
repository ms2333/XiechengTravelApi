using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XiechengTravel.Dtos
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public ICollection<LineItemDto> OrderLineitems { get; set; }
        public string OrderState { get; set; }
        public DateTime CreateDateUTC { get; set; }//创建时间
        public string TransactionMetadata { get; set; }//交易数据
    }
}
