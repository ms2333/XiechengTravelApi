using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace XiechengTravel.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<LineItem> OrderLineitems { get; set; }
        public OrderStateEnum OrderState { get; set; }
        public DateTime CreateDateUTC { get; set; }//创建时间
        public string TransactionMetadata { get; set; }//交易数据
    }
}
