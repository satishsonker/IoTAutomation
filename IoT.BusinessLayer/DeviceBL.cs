using IoT.DataLayer.Interface;
using IoT.ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IoT.BusinessLayer
{
   public class DeviceBL:CommonBL
    {
        private readonly IDevices _device;
        public DeviceBL(IDevices device)
        {
            _device = device;
        }

        public async Task<ResponseModel> AddDevice(Device device, string userKey)
        {
            if (device == null || string.IsNullOrEmpty(userKey))
                return CommonBL.GetResponseModel("Server didn't received any data", MessageTypes.ValidationIssue);
            if (device != null)
            {
                device.CreatedDate = DateTime.Now;
                device.ConnectionCount = 0;
                device.DeviceKey = Guid.NewGuid().ToString().Replace("-", "").ToUpper();
                device.UserKey = userKey;
            }
           return await _device.Add(device,userKey);
        }
        public IEnumerable<Device> GetAllDevice(string userKey,string deviceKey="")
        {
            return _device.GetAllDevices(userKey,deviceKey);
        }

        public IEnumerable<object> GetDeviceDropdown( string userKey)
        {
            return _device.GetDeviceDropdown(userKey);
        }

        public IEnumerable<object> GetDeviceTypeDropdown(int pageNo, int pageSize)
        {
            return _device.GetDeviceTypeDropdown(pageNo,pageSize);
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
        public bool UpdateDeviceHistory(string userKey,string deviceKey,bool isConnected)
        {
            return _device.UpdateDeviceHistory(userKey,deviceKey,isConnected);
        }
    }
}
