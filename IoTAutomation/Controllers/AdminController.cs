using IoT.BusinessLayer;
using IoT.DataLayer.Interface;
using IoT.DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace IoT.WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private AdminBL _adminBL;
        public AdminController(IAdmin iadmin)
        {
            _adminBL = new AdminBL(iadmin);
        }

        [HttpPost]
        [Route("AddDeviceType")]
        public int AddDeviceType([FromBody] DeviceType deviceType)
        {
            if (deviceType != null)
                deviceType.CreatedDate = DateTime.Now;
            return _adminBL.AddDeviceType(deviceType);
        }
        [HttpPost]
        [Route("UpdateDeviceType")]
        public int UpdateDeviceType([FromBody] DeviceType deviceType)
        {
            if (deviceType != null)
                deviceType.ModifiedDate = DateTime.Now;
            return _adminBL.UpdateDeviceType(deviceType);
        }

        [HttpDelete]
        [Route("DeleteDeviceType")]
        public int DeleteDeviceType([FromQuery] int deviceTypeId)
        {
            return _adminBL.DeleteDeviceType(deviceTypeId);
        }

        [HttpGet]
        [Route("SearchDeviceType")]
        public IEnumerable<DeviceType> SearchDeviceType([FromQuery] string searchTerm)
        {
           return _adminBL.SearchDeviceType(searchTerm);
        }

        [HttpGet]
        [Route("GetDeviceType")]
        public DeviceType GetDeviceType([FromQuery]int deviceTypeId)
        {
            return _adminBL.GetDeviceType(deviceTypeId);
        }

        [HttpPost]
        [Route("AddDeviceAction")]
        public int AddDeviceAction([FromBody] DeviceAction deviceAction)
        {
            return _adminBL.AddDeviceAction(deviceAction);
        }

        [HttpPost]
        [Route("UpdateDeviceAction")]
        public int UpdateDeviceAction([FromBody] DeviceAction deviceAction)
        {
            return _adminBL.UpdateDeviceAction(deviceAction);
        }

        [HttpDelete]
        [Route("DeleteDeviceAction")]
        public int DeleteDeviceAction([FromQuery] int deviceActionId)
        {
            return _adminBL.DeleteDeviceAction(deviceActionId);
        }

        [HttpGet]
        [Route("SearchDeviceAction")]
        public IEnumerable<DeviceAction> SearchDeviceAction([FromQuery] string searchTerm)
        {
            return _adminBL.SearchDeviceAction(searchTerm);
        }

        [HttpGet]
        [Route("GetDeviceAction")]
        public DeviceAction GetDeviceAction([FromQuery]int deviceActionId)
        {
            return _adminBL.GetDeviceAction(deviceActionId);
        } 
        
        [HttpGet]
        [Route("GetAllDeviceAction")]
        public IEnumerable<DeviceAction> GetAllDeviceAction()
        {
            return _adminBL.GetAllDeviceType();
        }
    }
}

