using IoT.BusinessLayer;
using IoT.DataLayer.Interface;
using IoT.ModelLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
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
        public async Task<IActionResult> AddDevice([FromBody] Device device, [FromHeader] string userKey)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newdevice = await _deviceBL.AddDevice(device, userKey);
                    if (newdevice.MessageType==MessageTypes.NotSaved)
                        return Ok(newdevice);
                    if(newdevice.MessageType==MessageTypes.Saved)
                        return Ok(newdevice);
                    return BadRequest(newdevice);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while Adding Device {0}", device.DeviceName);
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        [Route("GetAllDevice")]
        public IActionResult GetAllDevice([FromHeader] string userKey, [FromQuery] string deviceKey = "")
        {
            try
            {
                return Ok(_deviceBL.GetAllDevice(userKey, deviceKey));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while getting all Devices");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("SearchDevice")]
        public IActionResult SearchDevice([FromQuery] string searchTerm, [FromHeader] string userKey)
        {
            try
            {
                return Ok(_deviceBL.SearchDevice(searchTerm, userKey));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while serching Device with keyword {0}", searchTerm);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetDeviceDropdown")]
        public IActionResult GetDeviceDropdown([FromHeader] string userKey)
        {
            try
            {
                return Ok(_deviceBL.GetDeviceDropdown(userKey));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while getting device dropdown data");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetDeviceTypeDropdown")]
        public IActionResult GetDeviceTypeDropdown([FromQuery] int pageNo=1,[FromQuery] int pageSize=100)
        {
            try
            {
                return Ok(_deviceBL.GetDeviceTypeDropdown(pageNo,pageSize));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while getting device type dropdown data");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("UpdateDevice")]
        public Device UpdateDevice([FromBody] Device device, [FromHeader] string userKey)
        {
            try
            {
                device.UserKey = userKey;
                return _deviceBL.UpdateDevice(device, userKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while updating device {0}",device.DeviceName);
                return new Device();
            }
        }
        [HttpPost]
        [Route("UpdateDeviceHistory")]
        public bool UpdateDeviceHistory([FromQuery] string deviceKey,[FromQuery] bool isConnected, [FromHeader] string userKey)
        {
            try
            {                
                return _deviceBL.UpdateDeviceHistory(userKey,deviceKey,isConnected);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while updating device history with device Key {0}, Userkey {1}", deviceKey,userKey);
                return false;
            }
        }

        [HttpDelete]
        [Route("DeleteDevice")]
        public IActionResult Deletedevice([FromQuery] string deviceKey, [FromHeader] string userKey)
        {
            try
            {
                return Ok(_deviceBL.DeleteDevice(deviceKey, userKey));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while deleting device with key{0}", deviceKey);
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("GetDeviceTypeAction")]
        public IActionResult GetDeviceAction()
        {
            try
            {
                return Ok(_deviceBL.GetDeviceTypeAction());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while getting device action");
                return BadRequest(ex.Message);
            }
        }
    }
}