using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using XiechengTravel.Dtos;
using XiechengTravel.Models;

namespace XiechengTravel.Controllers
{
    [Route("Auth")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthenticateController(
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
            )
        {
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [AllowAnonymous]
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            //1.验证用户名密码
            //第三四个参数：表示多次登录是否锁定账户
            var loginResult = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.Password, false, false);
            if (!loginResult.Succeeded)
            {
                return BadRequest("您所登录的账户不存在");
            }
            var user = await _userManager.FindByNameAsync(loginDto.Email);
            var signingAlgorithm = SecurityAlgorithms.HmacSha256;

            //2.创建jwt
            var claims = new List<Claim>
            {
                //new Claim(ClaimTypes.Role,"Admin"),//添加角色
                new Claim(JwtRegisteredClaimNames.Sub,user.Id)
            };
            var roleNames = await _userManager.GetRolesAsync(user);//从数据库中拿到User的角色
            foreach (var roleName in roleNames)//将该对象的所有Claim遍历存储到claims，在后面添加到token中一同返回
            {
                var roleClaim = new Claim(ClaimTypes.Role,roleName);
                claims.Add(roleClaim);
            }
            var secretByte = Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]);
            var signingKey = new SymmetricSecurityKey(secretByte);//非对称算法进行加密
            var signingCredentials = new SigningCredentials(signingKey, signingAlgorithm);
            var token = new JwtSecurityToken(
                issuer:_configuration["Authentication:Issuer"],//谁发布的
                audience: _configuration["Authentication:Audience"],//发布给谁
                claims,
                notBefore:DateTime.UtcNow,//发布时间
                expires:DateTime.UtcNow.AddDays(1),//有效时间
                signingCredentials
                );

            var tokenStr = new JwtSecurityTokenHandler().WriteToken(token);//以string的格式输出token
            //3.return 200 ok+jwt
            return Ok(tokenStr);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            if (_userManager.FindByEmailAsync(registerUserDto.Email) != null)
            {
                return BadRequest("用户名已经存在");
            }
            //1.使用用户名创建用户对象
            var user = new ApplicationUser()
            {
                UserName = registerUserDto.Email,
                Email = registerUserDto.Email

            };
            //2.hash 保存密码
            var result = await _userManager.CreateAsync(user, registerUserDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest("用户创建失败");
            }
            return Ok();
        }
    }
}
