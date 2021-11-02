using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IoT.ModelLayer;

namespace IoT.DataLayer.Interface
{
  public  interface IDevices
    {
        Task<ResponseModel> Add(Device newDevice, string userKey);
        Device Update(Device updateDevice,string userKey);
        Device Delete(string DeviceKey, string userKey);
        IEnumerable<DeviceExt> GetAllDevices(string userKey,string deviceKey="");
        DeviceExt GetDevice(string userKey,int DeviceId);
        bool UpdateDeviceHistory(string userKey, string deviceKey,bool isConnected);
        IEnumerable<Device> SearchDevices(string searchTerm, string userKey);
        IEnumerable<object> GetDeviceDropdown(string userKey);
        IEnumerable<object> GetDeviceTypeDropdown(int pageNo, int pageSize);
        IEnumerable<DeviceType> GetDeviceTypeAction();

    }
}
