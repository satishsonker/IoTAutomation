using IoT.BusinessLayer;
using IoT.DataLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using IoT.ModelLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public User AddUser([FromBody] User user)
        {
            // if (user != null)
            //     user.CreatedDate = DateTime.Now;
            //return _userBL.AddUser(user);
            return user;
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
        [HttpGet]
        [Route("GetUserPermission")]
        public async Task<UserPermission> GetUserPermission([FromHeader] string userKey)
        {
            return await _userBL.GetUserPermission(userKey);
        }

        [HttpGet]
        [Route("GetAllUserPermissions")]
        public IEnumerable<UserPermission> GetAllUserPermissions([FromHeader] string userKey)
        {
            return _userBL.GetAllUserPermissions(userKey);
        }
    }
}
