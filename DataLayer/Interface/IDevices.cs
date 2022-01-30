using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IoT.ModelLayer;

namespace IoT.DataLayer.Interface
{
  public  interface IDevices
    {
        Task<bool> UpdateFavourite(string userKey, string deviceKey, bool isFavourite);
        Task<List<Device>> GetFavourite(string userKey);
        Task<PagingRecord> GetDeviceTypePaging(int pageNo, int pageSize);
        Task<ResponseModel> Add(Device newDevice, string userKey);
        Task<Device> Update(Device updateDevice,string userKey);
        Task<Device> Delete(string DeviceKey, string userKey);
        Task<PagingRecord> GetAllDevices(string userKey,int pageNo,int pageSize, string deviceKey = "");
        Task<DeviceExt> GetDevice(string userKey,int DeviceId);
        Task<bool> UpdateDeviceHistory(string userKey, string deviceKey,bool isConnected);
        Task<List<DeviceExt>> SearchDevices(string searchTerm, string userKey);
        Task<List<dynamic>> GetDeviceDropdown(string userKey);
        Task<List<dynamic>> GetDeviceTypeDropdown(int pageNo, int pageSize);
        Task<PagingRecord> GetDeviceTypeAction();

    }
}
