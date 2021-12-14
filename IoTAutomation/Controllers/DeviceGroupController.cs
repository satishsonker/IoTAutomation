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
    public class DeviceGroupController : ControllerBase
    {
        private DeviceGroupBL deviceGroupBL;
        private readonly ILogger _logger;
        public DeviceGroupController(IDeviceGroup deviceGroup, ILogger<ActivityLogController> logger)
        {
            deviceGroupBL = new DeviceGroupBL(deviceGroup);
            _logger = logger;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<int> Add([FromBody] DeviceGroup deviceGroup, [FromHeader] string userKey)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newdevice = await deviceGroupBL.AddGroup(deviceGroup, userKey);
                    if (newdevice > 0)
                    {
                        return newdevice;
                    }
                    _logger.LogError("Unable to Add device group Log: User Key {0}", userKey);
                    return 0;
                }
                _logger.LogError("Get invalid Model while Adding the device group Log: User Key {0}", userKey);
                return 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occured while Adding the device group Log: User Key {0}", userKey);
                return 0;
            }
        }
        [HttpGet]
        [Route("GetGroupDetails/{groupKey}")]
        public async Task<List<DeviceGroupDetail>> GetGroupDetails([FromRoute]string groupKey,[FromHeader] string userKey)
        {
            try
            {
                return await deviceGroupBL.GetGroupDetails(groupKey, userKey);
            }
            catch (Exception ex)
            {
                _logger.LogError("Unable to get device group details: User Key {0}, error :{1}", userKey,ex.Message);
                return new List<DeviceGroupDetail>();
            }
        }

        [HttpPost]
        [Route("AddGroupDetails/{groupKey}")]
        public async Task<int> AddGroupDetails([FromBody] List<DeviceGroupDetail> deviceGroupDetail, [FromRoute] string groupKey,  [FromHeader] string userKey)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newdevice = await deviceGroupBL.AddGroupDetail(deviceGroupDetail,groupKey, userKey);
                    if (newdevice > 0)
                    {
                        return newdevice;
                    }
                    _logger.LogError("Unable to Add device group details: User Key {0}", userKey);
                    return 0;
                }
                _logger.LogError("Get invalid Model while Adding the device group details: User Key {0}", userKey);
                return 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occured while Adding the device group details: User Key {0}", userKey);
                return 0;
            }
        }

        [HttpPost]
        [Route("UpdateGroup")]
        public async Task<int> UpdateGroup([FromBody] DeviceGroup deviceGroup, [FromHeader] string userKey)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newdevice = await deviceGroupBL.UpdateGroup(deviceGroup, userKey);
                    if (newdevice > 0)
                    {
                        return newdevice;
                    }
                    _logger.LogError("Unable to update device group Log: User Key {0}", userKey);
                    return 0;
                }
                _logger.LogError("Get invalid Model while updating the device group Log: User Key {0}", userKey);
                return 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occured while updateing the device group Log: User Key {0}", userKey);
                return 0;
            }
        }

        [HttpDelete]
        [Route("DeleteGroup/{groupKey}")]
        public async Task<int> DeleteGroup([FromRoute] string groupKey, [FromHeader] string userKey)
        {
            try
            {
                    var newdevice = await deviceGroupBL.DeleteGroup(groupKey, userKey);
                    if (newdevice > 0)
                    {
                        return newdevice;
                    }
                    _logger.LogError("Unable to delete device group Log: User Key {0}", userKey);
                    return 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occured while deleting the device group Log: User Key {0}", userKey);
                return 0;
            }
        }

        [HttpGet]
        [Route("Search/{searchTerm}")]
        public async Task<List<DeviceGroup>> Search([FromRoute] string searchTerm, [FromHeader] string userKey)
        {
            try
            {
                return await deviceGroupBL.SearchGroup(searchTerm, userKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occured while searching the device group: User Key {0}", userKey);
              return  new List<DeviceGroup>();
            }
        }

        [HttpGet]
        [Route("GetGroup/{groupKey}")]
        public async Task<DeviceGroup> GetGroup([FromRoute] string groupKey, [FromHeader] string userKey)
        {
            try
            {
                return await deviceGroupBL.GetGroup(groupKey, userKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occured while searching the device group: User Key {0}", userKey);
                return new DeviceGroup();
            }
        }

        [HttpGet]
        [Route("GetGroups")]
        public async Task<List<DeviceGroup>> GetGroups([FromHeader] string userKey)
        {
            try
            {
                return await deviceGroupBL.GetGroups(userKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occured while searching the device group: User Key {0}", userKey);
                return new List<DeviceGroup>();
            }
        }

    }
}
