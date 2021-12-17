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
    public class DeviceController : ControllerBase
    {
        private DeviceBL _deviceBL;
        private readonly ILogger _logger;
        public DeviceController(IDevices devices, ILogger<DeviceController> logger)
        {
            _deviceBL = new DeviceBL(devices);
            _logger = logger;
        }

        [HttpPost]
        [Route("AddDevice")]
        public async Task<ResponseModel> AddDevice([FromBody] Device device, [FromHeader] string userKey)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return await _deviceBL.AddDevice(device, userKey);
                }
                return new ResponseModel();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while Adding Device {0}", device.DeviceName);
                return new ResponseModel();
            }
        }

        [HttpGet]
        [Route("GetAllDevice")]
        public async Task<List<DeviceExt>> GetAllDevice([FromHeader] string userKey, [FromQuery] string deviceKey = "")
        {
            try
            {
                return await _deviceBL.GetAllDevice(userKey, deviceKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while getting all Devices");
                return new List<DeviceExt>();
            }
        }

        [HttpGet]
        [Route("SearchDevice")]
        public async Task<List<DeviceExt>> SearchDevice([FromQuery] string searchTerm, [FromHeader] string userKey)
        {
            try
            {
                return await _deviceBL.SearchDevice(searchTerm, userKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while serching Device with keyword {0}", searchTerm);
                return new List<DeviceExt>();
            }
        }

        [HttpGet]
        [Route("GetDeviceDropdown")]
        public async Task<List<dynamic>> GetDeviceDropdown([FromHeader] string userKey)
        {
            try
            {
                return await _deviceBL.GetDeviceDropdown(userKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while getting device dropdown data");
                return new List<object>();
            }
        }

        [HttpGet]
        [Route("GetDeviceTypeDropdown")]
        public async Task<List<dynamic>> GetDeviceTypeDropdown([FromQuery] int pageNo=1,[FromQuery] int pageSize=100)
        {
            try
            {
                return await _deviceBL.GetDeviceTypeDropdown(pageNo,pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while getting device type dropdown data");
                return new List<object>();
            }
        }
        [HttpGet]
        [Route("GetDeviceTypePaging")]
        public async Task<PagingRecord> GetDeviceTypePaging([FromQuery] int pageNo = 1, [FromQuery] int pageSize = 100)
        {
            try
            {
                return await _deviceBL.GetDeviceTypePaging(pageNo, pageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while getting device type dropdown data");
                return new PagingRecord();
            }
        }

        [HttpPost]
        [Route("UpdateDevice")]
        public async Task<Device> UpdateDevice([FromBody] Device device, [FromHeader] string userKey)
        {
            try
            {
                device.UserKey = userKey;
                return await _deviceBL.UpdateDevice(device, userKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while updating device {0}",device.DeviceName);
                return new Device();
            }
        }
        [HttpPost]
        [Route("UpdateDeviceHistory")]
        public async Task<bool> UpdateDeviceHistory([FromQuery] string deviceKey,[FromQuery] bool isConnected, [FromHeader] string userKey)
        {
            try
            {                
                return await _deviceBL.UpdateDeviceHistory(userKey,deviceKey,isConnected);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while updating device history with device Key {0}, Userkey {1}", deviceKey,userKey);
                return false;
            }
        }

        [HttpDelete]
        [Route("DeleteDevice")]
        public async Task<Device> DeleteDevice([FromQuery] string deviceKey, [FromHeader] string userKey)
        {
            try
            {
                return await _deviceBL.DeleteDevice(deviceKey, userKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while deleting device with key{0}", deviceKey);
                return new Device();
            }
        }
        [HttpGet]

        [Route("GetDeviceTypeAction")]
        public async Task<List<DeviceType>> GetDeviceAction()
        {
            try
            {
                return await _deviceBL.GetDeviceTypeAction();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while getting device action");
               return new List<DeviceType>();
            }
        }
    }
}