using IoT.DataLayer.Interface;
using IoT.ModelLayer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IoT.BusinessLayer
{
    public class OAuthBL
    {
        private readonly IUsers _userBL;
        private readonly IOptions<AppSettingConfig> _config;
        public OAuthBL(IUsers iUser, IOptions<AppSettingConfig> config)
        {
            _userBL = iUser;
            _config = config;
        }

        public string GetToken()
        {
            var claims = new[]
           {
                new Claim(JwtRegisteredClaimNames.Sub,"some_id"),
                new Claim("Granny","cookie")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Value.OAuth.Secret));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _config.Value.OAuth.Issuer,
                _config.Value.OAuth.Audiance,
                claims, notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(1),
                signingCredentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
