using IoT.BusinessLayer;
using IoT.DataLayer.Interface;
using IoT.DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoT.WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ActivityLogController : ControllerBase
    {
        private ActivityLogsBL activityLogsBL;
        public ActivityLogController(IActivityLogs activityLogs)
        {
            activityLogsBL = new ActivityLogsBL(activityLogs);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult AddDevice([FromBody] ActivityLog device, [FromHeader] string userKey)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newdevice = activityLogsBL.Add(device, userKey);
                    if (newdevice.ActivityLogId > 0)
                        return Ok();
                    return BadRequest(ModelState);
                }
                return BadRequest(ModelState);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetAll")]
        public IEnumerable<ActivityLog> GetAll([FromHeader] string userKey)
        {
            try
            {
                return activityLogsBL.GetAll(userKey);
            }
            catch (Exception)
            {
                return new List<ActivityLog>();
            }
        }
    }
}
