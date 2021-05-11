using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XiechengTravel.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Address { get; set; }
        //list table ：重建关系
        public ICollection<Order> Orders { get; set; }//一个用户包含多个订单
        public virtual ICollection<IdentityUserRole<string>> UserRoles { get; set; }
       // public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
       // public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
       // public virtual ICollection<IdentityUserToken<string>> Tokens{ get; set; }
    }
}
