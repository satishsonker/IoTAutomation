using IoT.DataLayer.Interface;
using IoT.ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IoT.BusinessLayer
{
    public class DeviceBL : CommonBL
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
            return await _device.Add(device, userKey);
        }
        public async Task<PagingRecord> GetAllDevice(string userKey, int pageNo, int pageSize, string deviceKey = "")
        {
            return await _device.GetAllDevices(userKey, pageNo, pageSize, deviceKey);
        }

        public async Task<List<dynamic>> GetDeviceDropdown(string userKey)
        {
            return await _device.GetDeviceDropdown(userKey);
        }

        public async Task<List<dynamic>> GetDeviceTypeDropdown(int pageNo, int pageSize)
        {
            return await _device.GetDeviceTypeDropdown(pageNo, pageSize);
        }
        public async Task<PagingRecord> GetDeviceTypePaging(int pageNo, int pageSize)
        {
            return await _device.GetDeviceTypePaging(pageNo, pageSize);
        }
        public async Task<List<DeviceExt>> SearchDevice(string searchTerm, string userKey)
        {
            return await _device.SearchDevices(searchTerm, userKey);
        }
        public async Task<Device> DeleteDevice(string DeviceKey, string userKey)
        {
            return await _device.Delete(DeviceKey, userKey);
        }
        public async Task<Device> UpdateDevice(Device device, string userKey)
        {
            return await _device.Update(device, userKey);
        }
        public async Task<object> GetDeviceTypeAction()
        {
            var result = await _device.GetDeviceTypeAction();
            return result;
        }
        public async Task<bool> UpdateDeviceHistory(string userKey, string deviceKey, bool isConnected)
        {
            return await _device.UpdateDeviceHistory(userKey, deviceKey, isConnected);

        }
        public async Task<bool> UpdateFavourite(string userKey, string deviceKey, bool isFavourite)
        {
            return await _device.UpdateFavourite(userKey, deviceKey, isFavourite);
        }
        public async Task<List<Device>> GetFavourite(string userKey)
        {
            return await _device.GetFavourite(userKey);
        }
    }
}
