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

        public Device AddDevice(Device device)
        {
            if (device != null)
            {
                device.CreatedDate = DateTime.Now;
                device.ConnectionCount = 0;
                device.DeviceKey = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
            }
            _device.Add(device);
            return device;
        }
        public IEnumerable<Device> GetAllDevice()
        {
            return _device.GetAllDevices();
        }

        public IEnumerable<object> GetDeviceDropdown()
        {
            return _device.GetDeviceDropdown();
        }

        public IEnumerable<object> GetDeviceTypeDropdown()
        {
            return _device.GetDeviceTypeDropdown();
        }
        public IEnumerable<Device> SearchDevice(string searchTerm)
        {
            return _device.SearchDevices(searchTerm);
        }
        public Device DeleteDevice(string DeviceKey)
        {
            return _device.Delete(DeviceKey);
        }
    }
}
