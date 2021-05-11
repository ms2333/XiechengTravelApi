using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace XiechengTravel.Models
{   
    /// <summary>
    /// 购物车模块
    /// </summary>
    public class ShoppingCart
    {
        [Key]
        public Guid Id { get; set; }
        public string userId { get; set; }
        public ApplicationUser user { get; set; } = new ApplicationUser();//对应引用关系，而不是创建数据库
        public ICollection<LineItem> ShopingCartItems { get; set; } = new List<LineItem>();
    }
}
