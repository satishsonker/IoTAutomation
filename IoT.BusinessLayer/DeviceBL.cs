using IoT.DataLayer.Interface;
using IoT.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoT.BusinessLayer
{
   public class DeviceBL
    {
        private readonly IDevices _device;
        public DeviceBL(IDevices device)
        {
            _device = device;
        }

        public Device AddDevice(Device device, string userKey)
        {
            if (device != null)
            {
                device.CreatedDate = DateTime.Now;
                device.ConnectionCount = 0;
                device.DeviceKey = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
            }
            _device.Add(device,userKey);
            return device;
        }
        public IEnumerable<Device> GetAllDevice(string userKey,string deviceKey="")
        {
            return _device.GetAllDevices(userKey,deviceKey);
        }

        public IEnumerable<object> GetDeviceDropdown( string userKey)
        {
            return _device.GetDeviceDropdown(userKey);
        }

        public IEnumerable<object> GetDeviceTypeDropdown()
        {
            return _device.GetDeviceTypeDropdown();
        }
        public IEnumerable<Device> SearchDevice(string searchTerm, string userKey)
        {
            return _device.SearchDevices(searchTerm,userKey);
        }
        public Device DeleteDevice(string DeviceKey, string userKey)
        {
            return _device.Delete(DeviceKey,userKey);
        }
        public Device UpdateDevice(Device device, string userKey)
        {
            return _device.Update(device,userKey);
        }
        public IEnumerable<DeviceType> GetDeviceTypeAction()
        {
            return _device.GetDeviceTypeAction();
        }
    }
}
