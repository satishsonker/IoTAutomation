using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IoT.ModelLayer;

namespace IoT.DataLayer.Interface
{
  public  interface IDevices
    {
        Task<PagingRecord> GetDeviceTypePaging(int pageNo, int pageSize);
        Task<ResponseModel> Add(Device newDevice, string userKey);
        Task<Device> Update(Device updateDevice,string userKey);
        Task<Device> Delete(string DeviceKey, string userKey);
        Task<List<DeviceExt>> GetAllDevices(string userKey,string deviceKey="");
        Task<DeviceExt> GetDevice(string userKey,int DeviceId);
        Task<bool> UpdateDeviceHistory(string userKey, string deviceKey,bool isConnected);
        Task<List<DeviceExt>> SearchDevices(string searchTerm, string userKey);
        Task<List<dynamic>> GetDeviceDropdown(string userKey);
        Task<List<dynamic>> GetDeviceTypeDropdown(int pageNo, int pageSize);
        Task<List<DeviceType>> GetDeviceTypeAction();

    }
}
