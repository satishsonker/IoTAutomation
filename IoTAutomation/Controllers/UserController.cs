using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IoT.BusinessLayer;
using IoT.DataLayer.Interface;
using IoT.DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace IoT.WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserBL _userBL;
        public UserController(IUsers users)
        {
            _userBL = new UserBL(users);
        }
        // GET: api/User
        [HttpGet]
        [Route("GetUser")]
        public User GetUser([FromHeader] string userKey)
        {
           return _userBL.GetUser(userKey);
        }

        [HttpPost]
        [Route("AddUser")]
        public void AddUser([FromBody] User user)
        {
            if (user != null)
                user.CreatedDate = DateTime.Now;
            _userBL.AddUser(user);
        }

        [HttpPost]
        [Route("UpdateUser")]
        public void UpdateUser([FromBody] User user)
        {
            if (user != null)
                user.ModifiedDate = DateTime.Now;
            _userBL.UpdateUser(user);
        }

        [HttpGet]
        [Route("GetAPIKey")]
        public User GetAPIKey([FromHeader] string userKey)
        {
            
           return _userBL.GetAPIKey(userKey);
        }
        [HttpGet]
        [Route("ResetAPIKey")]
        public User ResetAPIKey([FromHeader] string userKey)
        {

           return _userBL.ResetAPIKey(userKey);
        }
    }
}
