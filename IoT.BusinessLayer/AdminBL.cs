using IoT.DataLayer.Interface;
using IoT.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoT.BusinessLayer
{
    public class AdminBL
    {
        private readonly IAdmin _adminBL;
        public AdminBL(IAdmin iadmin)
        {
            _adminBL = iadmin;
        }
        public int AddDeviceType(DeviceType deviceType)
        {
            return _adminBL.AddDeviceType(deviceType);
        }

        public int UpdateDeviceType(DeviceType deviceType)
        {
            return _adminBL.UpdateDeviceType(deviceType);
        }

        public int DeleteDeviceType(int deviceTypeId)
        {
            return _adminBL.DeleteDeviceType(deviceTypeId);
        }

        public IEnumerable<DeviceType> SearchDeviceType(string searchTerm)
        {
            return _adminBL.SearchDeviceType(searchTerm);
        }

        public DeviceType GetDeviceType(int deviceTypeId)
        {
            return _adminBL.GetDeviceType(deviceTypeId);
        }
        public IEnumerable<DeviceAction> GetAllDeviceType()
        {
            return _adminBL.GetAllDeviceAction();
        }

        public int AddDeviceAction(DeviceAction deviceAction)
        {
            if(deviceAction!=null)
            {
                deviceAction.CreatedDate = DateTime.Now;
                deviceAction.ModifiedDate = DateTime.Now;
            }
            return _adminBL.AddDeviceAction(deviceAction);
        }

        public int UpdateDeviceAction(DeviceAction deviceAction)
        {
            return _adminBL.UpdateDeviceAction(deviceAction);
        }

        public int DeleteDeviceAction(int deviceActionId)
        {
            return _adminBL.DeleteDeviceAction(deviceActionId);
        }

        public IEnumerable<DeviceAction> SearchDeviceAction(string searchTerm)
        {
            return _adminBL.SearchDeviceAction(searchTerm);
        }

        public DeviceAction GetDeviceAction(int deviceActionId)
        {
            return _adminBL.GetDeviceAction(deviceActionId);
        }
    }
}
