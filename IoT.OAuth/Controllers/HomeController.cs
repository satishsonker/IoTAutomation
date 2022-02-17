using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IoT.OAuth.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }
        public IActionResult Authenticate()
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,"some_id"),
                new Claim("Granny","cookie")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constant.Secret));
            var signingCredentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var token =new JwtSecurityToken(
                Constant.Issuer,
                Constant.Audiance,
                claims,notBefore:DateTime.Now,
                expires:DateTime.Now.AddHours(1),
                signingCredentials
                );
            var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new { access_token = tokenJson });
        }
    }
}
