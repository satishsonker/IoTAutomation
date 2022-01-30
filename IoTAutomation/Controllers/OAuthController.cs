using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IoT.BusinessLayer;
using IoT.DataLayer.Interface;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using IoT.ModelLayer;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;

namespace IoT.WebAPI.Controllers
{
    [Route("[controller]")]
    public class OAuthController : Controller
    {
        private readonly UserBL _user;
        private readonly OAuthBL _oAuthBL;
        public OAuthController(IUsers users,IOptions<AppSettingConfig> config)
        {
            _user = new UserBL(users);
            _oAuthBL = new OAuthBL(users, config);
        }

        [HttpGet]
        [Route("Authorize")]
        public IActionResult Authorize(string response_type,string client_id,string redirect_uri,string scope,string state)
        {
            var query = new QueryBuilder
            {
                { "redirectUri", redirect_uri },
                { "state", state },
                { "scope", scope??"" },
                { "response_type", response_type }
            };
            return View(model:query.ToString());
        }

        [HttpPost]
        [Route("Authorize")]
        public async Task<IActionResult> Authorize([FromForm] string userName, [FromForm] string password, string redirectUri,string state)
        {
            var code = Guid.NewGuid().ToString().ToUpper().Replace("-","");
            var query = new QueryBuilder
            {
                { "redirectUri", redirectUri },
                { "code", code },
                { "state", state }
            };
            if (await _user.CheckUser(userName,password))
            return Redirect($"{redirectUri}{query}");
            return View(model: query.ToString());
        }


        [Route("Token")]
        public async Task<IActionResult> Token(string redirect_uri)
        {
            var jsonResponse = JsonConvert.SerializeObject(new
            {
                access_token = _oAuthBL.GetToken(),
                token_type = "bearer",
                raw_claim = "Test"
            });
            var responseByteArray = Encoding.UTF8.GetBytes(jsonResponse);
            await Response.Body.WriteAsync(responseByteArray);
            return Redirect(redirect_uri);
        }
    }
}
