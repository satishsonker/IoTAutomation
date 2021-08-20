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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/User/5
        [HttpPost]
        [Route("AddUser")]
        public void AddUser([FromBody] User user)
        {
            if (user != null)
                user.CreatedDate = DateTime.Now;
            _userBL.AddUser(user);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
