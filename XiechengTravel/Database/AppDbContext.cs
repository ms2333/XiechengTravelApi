using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using XiechengTravel.Models;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace XiechengTravel.Database
{
    public class AppDbContext:IdentityDbContext<ApplicationUser> 
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        //指明哪些模型需要映射到数据库中
        public DbSet<TouristRoute> TouristRoutes { get; set; }//数据模型映射（每一张table都需要一个DbSet进行映射）
        public DbSet<TouristRoutePicture> TouristRoutePictures { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<LineItem> LineItems { get; set; }
        public DbSet<Order> Orders { get; set;  }

        //添加种子数据
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //从Json读取种子数据
          // var JsonData =  File.ReadAllText(@"C:\Users\Flower-Li\source\repos\XiechengTravel\XiechengTravel\bin\Debug\net5.0\Database\TouristRouteData.json");
           var JsonData =  File.ReadAllText(Path.GetDirectoryName(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"/Database/TouristRouteData.json"));
            IList<TouristRoute> touristRoutes = JsonConvert.DeserializeObject<IList<TouristRoute>>(JsonData);//反序列化，将string=>object

            var jsonPicData =  File.ReadAllText(Path.GetDirectoryName(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"/XiechengTravel/Database/TouristRoutePictureData.json"));
            IList<TouristRoutePicture> touristRoutePictures = JsonConvert.DeserializeObject<IList<TouristRoutePicture>>(jsonPicData);


            // 初始化用户与角色的种子数据
            // 1. 更新用户与角色的外键关系
            modelBuilder.Entity<ApplicationUser>(b => {
                b.HasMany(x => x.UserRoles)//有多个UserRoles
                .WithOne()//设置为一对多的关系
                .HasForeignKey(ur => ur.UserId)//UserId为外键
                .IsRequired();//不能为空
            });

            // 2. 添加角色
            var adminRoleId = "308660dc-ae51-480f-824d-7dca6714c3e2"; // guid 
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "Admin".ToUpper()
                }
            );

            // 3. 添加用户
            var adminUserId = "90184155-dee0-40c9-bb1e-b5ed07afc04e";
            ApplicationUser adminUser = new ApplicationUser
            {
                Id = adminUserId,
                UserName = "1527263338@qq.com",
                NormalizedUserName = "1527263338@qq.com".ToUpper(),
                Email = "1527263338@qq.com",
                NormalizedEmail = "1527263338@qq.com".ToUpper(),
                TwoFactorEnabled = false,
                EmailConfirmed = true,
                PhoneNumber = "123456789",
                PhoneNumberConfirmed = false
            };

            //加密密码
            PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>();
            adminUser.PasswordHash = ph.HashPassword(adminUser, "Ms666666+");
            modelBuilder.Entity<ApplicationUser>().HasData(adminUser);

            // 4. 给用户加入管理员权限
            // 通过使用 linking table：IdentityUserRole
            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasData(new IdentityUserRole<string>()
                {
                    RoleId = adminRoleId,
                    UserId = adminUserId
                });

            modelBuilder.Entity<TouristRoute>().HasData(touristRoutes);
            modelBuilder.Entity<TouristRoutePicture>().HasData(touristRoutePictures);
            base.OnModelCreating(modelBuilder);

        }
    }
}
