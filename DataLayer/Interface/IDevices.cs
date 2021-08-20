using System;
using System.Collections.Generic;
using System.Text;
using IoT.DataLayer.Models;

namespace IoT.DataLayer.Interface
{
  public  interface IDevices
    {
        Device Add(Device newDevice);
        Device Update(Device updateDevice);
        Device Delete(string DeviceKey);
        IEnumerable<DeviceExt> GetAllDevices();
        DeviceExt GetDevice(int DeviceId);
        IEnumerable<DeviceExt> SearchDevices(string searchTerm);
        IEnumerable<object> GetDeviceDropdown();
        IEnumerable<object> GetDeviceTypeDropdown();

    }
}
