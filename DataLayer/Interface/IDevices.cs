using System;
using System.Collections.Generic;
using System.Text;
using IoT.DataLayer.Models;

namespace IoT.DataLayer.Interface
{
  public  interface IDevices
    {
        Device Add(Device newDevice, string userKey);
        Device Update(Device updateDevice,string userKey);
        Device Delete(string DeviceKey, string userKey);
        IEnumerable<DeviceExt> GetAllDevices(string userKey,string deviceKey="");
        DeviceExt GetDevice(string userKey,int DeviceId);
        bool UpdateDeviceHistory(string userKey, string deviceKey,bool isConnected);
        IEnumerable<DeviceExt> SearchDevices(string searchTerm, string userKey);
        IEnumerable<object> GetDeviceDropdown(string userKey);
        IEnumerable<object> GetDeviceTypeDropdown();
        IEnumerable<DeviceType> GetDeviceTypeAction();

    }
}
