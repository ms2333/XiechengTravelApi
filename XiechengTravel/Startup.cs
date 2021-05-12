using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XiechengTravel.Database;
using XiechengTravel.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;//使用配置文件中的链接字符串
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using XiechengTravel.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql;
using Microsoft.OpenApi.Models;

namespace XiechengTravel
{
    public class Startup
    {
        //注入配置文件服务
        public IConfiguration _Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            this._Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //注入服务
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    var secretBytes = Encoding.UTF8.GetBytes(_Configuration["Authentication:SecretKey"]);
                    option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,//验证发布者
                        ValidIssuer = _Configuration["Authentication:Issuer"],

                        ValidateAudience = true,//验证Token持有者
                        ValidAudience = _Configuration["Authentication:Audience"],

                        ValidateLifetime = true,//验证是否过期
                        IssuerSigningKey = new SymmetricSecurityKey(secretBytes)//传入私钥
                    };
                });
            services.AddControllers(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;//这里配置了对不支持的数据格式的请求返回406 unacceptable
            }
            ).AddNewtonsoftJson(setupAction =>
            {
                setupAction.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
            })
            .AddXmlDataContractSerializerFormatters()
            .ConfigureApiBehaviorOptions(setupAction =>
            {
                setupAction.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetail = new ValidationProblemDetails(context.ModelState)
                    {
                        Type = "数据验证",
                        Title = "数据验证失败",
                        Status = StatusCodes.Status422UnprocessableEntity,
                        Instance = context.HttpContext.Request.Path
                    };
                    problemDetail.Extensions.Add("tranceId", context.HttpContext.TraceIdentifier);
                    return new UnprocessableEntityObjectResult(problemDetail)
                    {
                        ContentTypes = { "application/problem+json" }
                    };
                };
            });
            //AutoMapper会扫描所有包含映射关系的Profile文件
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddTransient<ITouristRouteRepository, TouristRouteRepository>();
            services.AddDbContext<AppDbContext>(option =>
            {
                // option.UseSqlServer(_Configuration["DbContext:ConnectionString"]);//配置使用SqlServer服务
                option.UseMySql(_Configuration["DbContext:MysqlConnectionString"],
            new MySqlServerVersion(new Version(8, 0, 25)));
            });//注入database服务，在参数中使用lambda表达式惊醒配置数据库
               //每次请求都会重新创建一次服务，
               //AddSingleton 只创建一次服务，之后系统的每次请求都是使用的这一个实例
               //AddScoped ----

            services.AddHttpClient();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();//IurlHelper
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Api接口文档",
                    Description = ".NET5 Web API",
                    //TermsOfService = new Uri(""),
                    Contact = new OpenApiContact
                    {
                        Name = "Login Get Token(AdminAccount:  Email:1527263338@qq.com  Password:Ms123456)",
                        Url = new Uri("http://www.flowerli.xyz/Auth/Login"),
                    },
                });

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }
            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "REST-full API V1");
                x.RoutePrefix = string.Empty;
            });
            //你在哪
            app.UseRouting();
            //你是谁
            app.UseAuthentication();
            //你可以干什么
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
