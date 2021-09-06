using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IoT.BusinessLayer;
using IoT.DataLayer.Interface;
using IoT.DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IoT.WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private DeviceBL _deviceBL;
        public DeviceController(IDevices devices)
        {
            _deviceBL = new DeviceBL(devices);
        }

        [HttpPost]
        [Route("AddDevice")]
        public IActionResult AddDevice([FromBody] Device device,[FromHeader] string userKey)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newdevice = _deviceBL.AddDevice(device,userKey);
                    if (newdevice.DeviceId > 0)
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
        [Route("GetAllDevice")]
        public IActionResult GetAllDevice([FromHeader] string userKey,[FromQuery] string deviceKey="")
        {
            try
            {
                return Ok(_deviceBL.GetAllDevice(userKey,deviceKey));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("SearchDevice")]
        public IActionResult SearchDevice([FromQuery] string searchTerm, [FromHeader] string userKey)
        {
            try
            {
                return Ok(_deviceBL.SearchDevice(searchTerm,userKey));
            }
            catch (Exception ex)
            {
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
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Route("GetDeviceTypeDropdown")]
        public IActionResult GetDeviceTypeDropdown()
        {
            try
            {
                return Ok(_deviceBL.GetDeviceTypeDropdown());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        [Route("UpdateDevice")]
        public Device UpdateDevice([FromBody] Device device,[FromHeader] string userKey)
        {
            try
            {
                device.UserKey = userKey;
                return _deviceBL.UpdateDevice(device,userKey);
            }
            catch (Exception ex)
            {
                return new Device();
            }

        }

        [HttpDelete]
        [Route("DeleteDevice")]
        public IActionResult Deletedevice([FromQuery] string deviceKey, [FromHeader] string userKey)
        {
            try
            {
                return Ok(_deviceBL.DeleteDevice(deviceKey,userKey));
            }
            catch (Exception ex)
            {
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
                return BadRequest(ex.Message);
            }

        }
    }
}