using IoT.BusinessLayer;
using IoT.DataLayer.Interface;
using IoT.ModelLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoT.WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private AdminBL _adminBL;
        private readonly ILogger _logger;
        public AdminController(IAdmin iadmin, ILogger<AdminController> logger)
        {
            _adminBL = new AdminBL(iadmin);
            _logger = logger;
        }

        [HttpPost]
        [Route("AddDeviceType")]
        public async Task<IActionResult> AddDeviceType([FromBody] DeviceType deviceType,[FromHeader] string userKey)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    deviceType.CreatedDate = DateTime.Now;
                    int result =await _adminBL.AddDeviceType(deviceType, userKey);
                    return Ok(result);
                }
                _logger.LogWarning("Get invalid model while adding the Device Type, UserKey : {0}", userKey);
                return BadRequest("Get invalid model while adding the Device Type");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while adding the Device Type, UserKey : {0}", userKey);
                return BadRequest(ModelState);
            }


        }

        [HttpPost]
        [Route("UpdateDeviceType")]
        public async Task<IActionResult> UpdateDeviceType([FromBody] DeviceType deviceType, [FromHeader] string userKey)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (deviceType != null)
                    {
                        deviceType.ModifiedDate = DateTime.Now;
                        return Ok(await _adminBL.UpdateDeviceType(deviceType, userKey));
                    }
                }
                _logger.LogWarning("Get invalid model while updating the Device Type UserKey : {0}", userKey);
                return BadRequest("Get invalid model while updating the Device Type");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while updating the Device Type, UserKey : {0}", userKey);
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        [Route("DeleteDeviceType")]
        public async Task<IActionResult> DeleteDeviceType([FromQuery] int deviceTypeId, [FromHeader] string userKey)
        {
            try
            {
                if (deviceTypeId>0)
                {
                    return Ok(await _adminBL.DeleteDeviceType(deviceTypeId, userKey)); 
                }
                _logger.LogWarning("Get invalid Device Type id while deleting the Device Type, userKey : {0} & ID:{1}", userKey,deviceTypeId);
                return BadRequest("Unable to delete the Device Type");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while deleting the Device Type, UserKey : {0} & ID:{1}", userKey, deviceTypeId);
                return BadRequest("Unable to delete the Device Type");
            }
        }

        [HttpGet]
        [Route("SearchDeviceType")]
        public async Task<IActionResult> SearchDeviceType([FromQuery] string searchTerm, [FromHeader] string userKey)
        {
            try
            {
                searchTerm = searchTerm == null ? string.Empty : searchTerm;
                return Ok(await _adminBL.SearchDeviceType(searchTerm, userKey));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while search the Device Type, UserKey : {0}", userKey);
                return BadRequest("Unable to search the Device Type");
            }
        }

        [HttpGet]
        [Route("GetDeviceType")]
        public async Task<IActionResult> GetDeviceType([FromQuery] int deviceTypeId, [FromHeader] string userKey)
        {
            try
            {
                if (deviceTypeId > 0)
                {
                    return Ok(await _adminBL.GetDeviceType(deviceTypeId, userKey));
                }
                _logger.LogWarning("Get invalid Device Type id while getting the Device Type, userKey : {0} & ID:{1}", userKey, deviceTypeId);
                return BadRequest("Unable to get the Device Type");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while getting the Device Type, UserKey : {0} & ID:{1}", userKey, deviceTypeId);
                return BadRequest("Unable to delete the Device Type");
            }
        }

        [HttpPost]
        [Route("AddDeviceAction")]
        public async Task<IActionResult> AddDeviceAction([FromBody] DeviceAction deviceAction, [FromHeader] string userKey)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (deviceAction != null)
                    {
                        return Ok(await _adminBL.AddDeviceAction(deviceAction, userKey));
                    }
                    _logger.LogWarning("Get model=null while adding the Device Action, UserKey : {0}", userKey);
                }
                _logger.LogWarning("Get invalid model while adding the Device Action, UserKey : {0}", userKey);
                return BadRequest("Get invalid model while adding the Device Action");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while adding the Device Action, UserKey : {0}", userKey);
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        [Route("UpdateDeviceAction")]
        public async Task<IActionResult> UpdateDeviceAction([FromBody] DeviceAction deviceAction, [FromHeader] string userKey)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (deviceAction != null)
                    {
                        return Ok(await _adminBL.UpdateDeviceAction(deviceAction, userKey));
                    }
                    _logger.LogWarning("Get model=null while updating the Device Action, UserKey : {0}", userKey);
                }
                _logger.LogWarning("Get invalid model while updating the Device Action, UserKey : {0}", userKey);
                return BadRequest("Get invalid model while updating the Device Action");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while updating the Device Action, UserKey : {0}", userKey);
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        [Route("DeleteDeviceAction")]
        public async Task<IActionResult> DeleteDeviceAction([FromQuery] int deviceActionId, [FromHeader] string userKey)
        {
            try
            {
                if (deviceActionId > 0)
                {
                    return Ok(await _adminBL.DeleteDeviceAction(deviceActionId, userKey));
                }
                _logger.LogWarning("Get invalid Device Action Id while deleting the Device Action, userKey : {0} & ID:{1}", userKey, deviceActionId);
                return BadRequest("Unable to delete the Device Action");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while deleting the Device Action, UserKey : {0} & ID:{1}", userKey, deviceActionId);
                return BadRequest("Unable to delete the Device Action");
            }
        }

        [HttpGet]
        [Route("SearchDeviceAction")]
        public async Task<IActionResult> SearchDeviceAction([FromQuery] string searchTerm, [FromHeader] string userKey)
        {
            try
            {
                searchTerm = searchTerm == null ? string.Empty : searchTerm;
                return Ok(await _adminBL.SearchDeviceAction(searchTerm, userKey));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while search the Device Action, UserKey : {0}", userKey);
                return BadRequest("Unable to search the Device Action");
            }
        }

        [HttpGet]
        [Route("GetDeviceAction")]
        public async Task<IActionResult> GetDeviceAction([FromQuery] int deviceActionId, [FromHeader] string userKey)
        {
            try
            {
                if (deviceActionId > 0)
                {
                    return Ok(await _adminBL.GetDeviceAction(deviceActionId, userKey));
                }
                _logger.LogWarning("Get invalid Device Action id while Getting the Device Action, userKey : {0} & ID:{1}", userKey, deviceActionId);
                return BadRequest("Unable to get the Device Action");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while getting the Device Action, UserKey : {0} & ID:{1}", userKey, deviceActionId);
                return BadRequest("Unable to get the Device Action");
            }
        }

        [HttpGet]
        [Route("GetAllDeviceAction")]
        public async Task<PagingRecord> GetAllDeviceAction([FromHeader] string userKey,[FromQuery] int PageNo,[FromQuery] int PageSize)
        {
            try
            {
                return await _adminBL.GetAllDeviceType(userKey,PageNo,PageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while getting the Device Actions, UserKey : {0}", userKey);
                return new PagingRecord();
            }
        }

        [HttpPost]
        [Route("AddDeviceCapability")]
        public async Task<IActionResult> AddDeviceCapability([FromBody] DeviceCapability deviceCapability, [FromHeader] string userKey)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (deviceCapability != null)
                    {
                        return Ok(await _adminBL.AddDeviceCapability(deviceCapability, userKey));
                    }
                    _logger.LogWarning("Get model=null while adding the Device Capability, UserKey : {0}", userKey);
                }
                _logger.LogWarning("Get invalid model while adding the Device Capability, UserKey : {0}", userKey);
                return BadRequest("Get invalid model while adding the Device Capability");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while adding the Device Capability, UserKey : {0}", userKey);
                return BadRequest(ModelState);
            }
        }

        [HttpPost]
        [Route("UpdateDeviceCapability")]
        public async Task<IActionResult> UpdateDeviceCapability([FromBody] DeviceCapability DeviceCapability, [FromHeader] string userKey)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (DeviceCapability != null)
                    {
                        return Ok(await _adminBL.UpdateDeviceCapability(DeviceCapability, userKey));
                    }
                    _logger.LogWarning("Get model=null while updating the Device Capability, UserKey : {0}", userKey);
                }
                _logger.LogWarning("Get invalid model while updating the Device Capability, UserKey : {0}", userKey);
                return BadRequest("Get invalid model while updating the Device Capability");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while updating the Device Capability, UserKey : {0}", userKey);
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        [Route("DeleteDeviceCapability")]
        public async Task<IActionResult> DeleteDeviceCapability([FromQuery] int DeviceCapabilityId, [FromHeader] string userKey)
        {
            try
            {
                if (DeviceCapabilityId > 0)
                {
                    return Ok(await _adminBL.DeleteDeviceCapability(DeviceCapabilityId, userKey));
                }
                _logger.LogWarning("Get invalid Device Capability Id while deleting the Device Capability, userKey : {0} & ID:{1}", userKey, DeviceCapabilityId);
                return BadRequest("Unable to delete the Device Capability");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while deleting the Device Capability, UserKey : {0} & ID:{1}", userKey, DeviceCapabilityId);
                return BadRequest("Unable to delete the Device Capability");
            }
        }

        [HttpGet]
        [Route("SearchDeviceCapability")]
        public async Task<IActionResult> SearchDeviceCapability([FromQuery] string searchTerm, [FromHeader] string userKey)
        {
            try
            {
                searchTerm = searchTerm == null ? string.Empty : searchTerm;
                return Ok(await _adminBL.SearchDeviceCapability(searchTerm, userKey));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while search the Device Capability, UserKey : {0}", userKey);
                return BadRequest("Unable to search the Device Capability");
            }
        }

        [HttpGet]
        [Route("GetDeviceCapability")]
        public async Task<IActionResult> GetDeviceCapability([FromQuery] int DeviceCapabilityId, [FromHeader] string userKey)
        {
            try
            {
                if (DeviceCapabilityId > 0)
                {
                    return Ok(await _adminBL.GetDeviceCapability(DeviceCapabilityId, userKey));
                }
                _logger.LogWarning("Get invalid Device Capability id while Getting the Device Capability, userKey : {0} & ID:{1}", userKey, DeviceCapabilityId);
                return BadRequest("Unable to get the Device Capability");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while getting the Device Capability, UserKey : {0} & ID:{1}", userKey, DeviceCapabilityId);
                return BadRequest("Unable to get the Device Capability");
            }
        }

        [HttpGet]
        [Route("GetAllDeviceCapability")]
        public async Task<IActionResult> GetAllDeviceCapability([FromHeader] string userKey,[FromQuery] int PageNo, [FromQuery] int PageSize)
        {
            try
            {
                return Ok(await _adminBL.GetAllDeviceCapability(userKey,PageNo,PageSize));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while getting the Device Capabilitys, UserKey : {0}", userKey);
                return BadRequest("Unable to get the Device Capabilitys");
            }
        }

        [HttpPost]
        [Route("UpdateAdminPermission")]
        public async Task<IActionResult> UpdateAdminPermission([FromBody] List<UserPermission> userPermissions, [FromHeader] string userKey)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _adminBL.UpdateAdminPermission(userPermissions, userKey)); 
                }
                _logger.LogWarning("Get invalid model while updating the admin permission, UserKey : {0}", userKey);
                return BadRequest(ModelState);

            }
            catch (Exception ex)
            {
                _logger.LogError("Error occured while updating the admin permission, UserKey : {0}", userKey);
                return BadRequest("Error : Unable to update admin perminssion");
            }
        }
    }
}

