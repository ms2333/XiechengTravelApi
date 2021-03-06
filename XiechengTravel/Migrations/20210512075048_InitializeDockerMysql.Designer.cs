// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using XiechengTravel.Database;

namespace XiechengTravel.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210512075048_InitializeDockerMysql")]
    partial class InitializeDockerMysql
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = "308660dc-ae51-480f-824d-7dca6714c3e2",
                            ConcurrencyStamp = "feef72bb-67b4-487f-9359-abc76a5ee793",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .HasColumnType("longtext");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RoleId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");

                    b.HasData(
                        new
                        {
                            UserId = "90184155-dee0-40c9-bb1e-b5ed07afc04e",
                            RoleId = "308660dc-ae51-480f-824d-7dca6714c3e2"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Value")
                        .HasColumnType("longtext");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("XiechengTravel.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("longtext");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("longtext");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers");

                    b.HasData(
                        new
                        {
                            Id = "90184155-dee0-40c9-bb1e-b5ed07afc04e",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "c817d7ef-5b7c-46a7-a1ad-34437f2306ff",
                            Email = "1527263338@qq.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "1527263338@QQ.COM",
                            NormalizedUserName = "1527263338@QQ.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEHctEVB55xHE/2boyl6cOvR3kRQKtfLLE9BpcDeMGq5ZrRbZ1HINKed2c4TPpsirRQ==",
                            PhoneNumber = "123456789",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "506a2a46-f090-42c7-80c0-5aebc61fa79c",
                            TwoFactorEnabled = false,
                            UserName = "1527263338@qq.com"
                        });
                });

            modelBuilder.Entity("XiechengTravel.Models.LineItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double?>("DiscountPresent")
                        .HasColumnType("double");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("char(36)");

                    b.Property<decimal>("OriginalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("ShoppingCartId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TouristRouteId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ShoppingCartId");

                    b.HasIndex("TouristRouteId");

                    b.ToTable("LineItems");
                });

            modelBuilder.Entity("XiechengTravel.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreateDateUTC")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("OrderState")
                        .HasColumnType("int");

                    b.Property<string>("TransactionMetadata")
                        .HasColumnType("longtext");

                    b.Property<string>("UserId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("XiechengTravel.Models.ShoppingCart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("userId")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("userId");

                    b.ToTable("ShoppingCarts");
                });

            modelBuilder.Entity("XiechengTravel.Models.TouristRoute", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("DepartureCity")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DepartureTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("varchar(1000)");

                    b.Property<double?>("DiscountPresent")
                        .HasColumnType("double");

                    b.Property<string>("Features")
                        .HasColumnType("longtext");

                    b.Property<string>("Fees")
                        .HasColumnType("longtext");

                    b.Property<string>("Notes")
                        .HasColumnType("longtext");

                    b.Property<decimal>("OriginalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<double?>("Rating")
                        .HasColumnType("double");

                    b.Property<string>("Tittle")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int?>("TravelDays")
                        .HasColumnType("int");

                    b.Property<int?>("TripType")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateTime")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("TouristRoutes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("99ba5433-df5f-a898-c8e0-78b8ba55f251"),
                            CreateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartureCity = 3,
                            Description = "官方旗舰店亏本销售，全程免费喝可乐",
                            Features = "<p>features没什么</p>",
                            Fees = "<p>fees没什么</p>",
                            Notes = "<p>notes没什么</p>",
                            OriginalPrice = 1119.8m,
                            Rating = 4.0999999999999996,
                            Tittle = "埃及法老王3日游",
                            TravelDays = 3,
                            TripType = 3
                        },
                        new
                        {
                            Id = new Guid("a164b5f9-5d54-4647-928a-e8a87b2cfed9"),
                            CreateTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DepartureCity = 4,
                            Description = "官方旗舰店亏本销售，全程免费喝可乐",
                            Features = "<p>features没什么</p>",
                            Fees = "<p>fees没什么</p>",
                            Notes = "<p>notes没什么</p>",
                            OriginalPrice = 2998.8m,
                            Rating = 7.0,
                            Tittle = "埃菲尔铁塔3日游",
                            TravelDays = 6,
                            TripType = 1
                        });
                });

            modelBuilder.Entity("XiechengTravel.Models.TouristRoutePicture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<Guid>("TouristRouteId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Url")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("TouristRouteId");

                    b.ToTable("TouristRoutePictures");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            TouristRouteId = new Guid("a164b5f9-5d54-4647-928a-e8a87b2cfed9"),
                            Url = "1win/win/win"
                        },
                        new
                        {
                            Id = 2,
                            TouristRouteId = new Guid("99ba5433-df5f-a898-c8e0-78b8ba55f251"),
                            Url = "2win/win/win"
                        },
                        new
                        {
                            Id = 3,
                            TouristRouteId = new Guid("a164b5f9-5d54-4647-928a-e8a87b2cfed9"),
                            Url = "ewin/win/win"
                        },
                        new
                        {
                            Id = 4,
                            TouristRouteId = new Guid("99ba5433-df5f-a898-c8e0-78b8ba55f251"),
                            Url = "4win/win/win"
                        },
                        new
                        {
                            Id = 5,
                            TouristRouteId = new Guid("a164b5f9-5d54-4647-928a-e8a87b2cfed9"),
                            Url = "5win/win/win"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("XiechengTravel.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("XiechengTravel.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("XiechengTravel.Models.ApplicationUser", null)
                        .WithMany("UserRoles")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("XiechengTravel.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("XiechengTravel.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("XiechengTravel.Models.LineItem", b =>
                {
                    b.HasOne("XiechengTravel.Models.Order", null)
                        .WithMany("OrderLineitems")
                        .HasForeignKey("OrderId");

                    b.HasOne("XiechengTravel.Models.ShoppingCart", null)
                        .WithMany("ShopingCartItems")
                        .HasForeignKey("ShoppingCartId");

                    b.HasOne("XiechengTravel.Models.TouristRoute", "TouristRoute")
                        .WithMany()
                        .HasForeignKey("TouristRouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TouristRoute");
                });

            modelBuilder.Entity("XiechengTravel.Models.Order", b =>
                {
                    b.HasOne("XiechengTravel.Models.ApplicationUser", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("XiechengTravel.Models.ShoppingCart", b =>
                {
                    b.HasOne("XiechengTravel.Models.ApplicationUser", "user")
                        .WithMany()
                        .HasForeignKey("userId");

                    b.Navigation("user");
                });

            modelBuilder.Entity("XiechengTravel.Models.TouristRoutePicture", b =>
                {
                    b.HasOne("XiechengTravel.Models.TouristRoute", "TouristRoute")
                        .WithMany("TouristRoutePictures")
                        .HasForeignKey("TouristRouteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TouristRoute");
                });

            modelBuilder.Entity("XiechengTravel.Models.ApplicationUser", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("XiechengTravel.Models.Order", b =>
                {
                    b.Navigation("OrderLineitems");
                });

            modelBuilder.Entity("XiechengTravel.Models.ShoppingCart", b =>
                {
                    b.Navigation("ShopingCartItems");
                });

            modelBuilder.Entity("XiechengTravel.Models.TouristRoute", b =>
                {
                    b.Navigation("TouristRoutePictures");
                });
#pragma warning restore 612, 618
        }
    }
}
