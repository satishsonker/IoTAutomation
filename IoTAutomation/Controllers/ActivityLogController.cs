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
        public async  Task<int> Add([FromBody] ActivityLog device, [FromHeader] string userKey)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newdevice =await activityLogsBL.Add(device, userKey);
                    if (newdevice > 0)
                    {
                        return newdevice;
                    }
                    _logger.LogError("Unable to Add Activity Log: User Key {0}", userKey);
                    return 0;
                }
                _logger.LogError("Get invalid Model while Adding the Activity log: User Key {0}", userKey);
                return 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occured while Adding the Activity log: User Key {0}", userKey);
                return 0;
            }
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<PagingRecord> GetAll([FromHeader] string userKey, int pageNo=1, int pageSize=10)
        {
            try
            {
                return await activityLogsBL.GetAll(userKey,pageNo,pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occured while getting all Activity log: User Key {0}", userKey);
                return new PagingRecord();
            }
        }

        [HttpGet]
        [Route("SearchLog")]
        public async Task<PagingRecord> SearchLog([FromHeader] string userKey, [FromHeader] string searchTerm, int pageNo = 1, int pageSize = 10)
        {
            try
            {
                return await activityLogsBL.Search(userKey,searchTerm, pageNo, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occured while searching Activity log: User Key {0}", userKey);
                return new PagingRecord();
            }
        }
    }
}
