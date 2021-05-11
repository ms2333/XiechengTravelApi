using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XiechengTravel.Models;

namespace XiechengTravel.Dtos
{
    public class ShoppingCartDto
    {
        public Guid Id { get; set; }
        public string userId { get; set; }
        public ICollection<LineItem> ShopingCartItems { get; set; }
    }
}
