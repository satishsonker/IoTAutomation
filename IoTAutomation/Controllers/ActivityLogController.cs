using IoT.BusinessLayer;
using IoT.DataLayer.Interface;
using IoT.ModelLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger _logger;
        public ActivityLogController(IActivityLogs activityLogs, ILogger<ActivityLogController> logger)
        {
            activityLogsBL = new ActivityLogsBL(activityLogs);
            _logger = logger;
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] ActivityLog device, [FromHeader] string userKey)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newdevice = activityLogsBL.Add(device, userKey);
                    if (newdevice.ActivityLogId > 0)
                    {
                        return Ok();
                    }
                    _logger.LogError("Unable to Add Activity Log: User Key {0}", userKey);
                    return BadRequest("Unable to Add Activity Log");
                }
                _logger.LogError("Get invalid Model while Adding the Activity log: User Key {0}", userKey);
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occured while Adding the Activity log: User Key {0}", userKey);
                return BadRequest();
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occured while getting all Activity log: User Key {0}", userKey);
                return new List<ActivityLog>();
            }
        }
    }
}
