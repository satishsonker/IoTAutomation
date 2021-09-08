using IoT.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoT.DataLayer.Interface
{
  public  interface IAdmin
    {
        int AddDeviceType(DeviceType deviceType);
        int UpdateDeviceType(DeviceType deviceType);
        int DeleteDeviceType(int deviceTypeId);
        IEnumerable<DeviceType> SearchDeviceType(string searchTerm);
        DeviceType GetDeviceType(int deviceTypeId);

        int AddDeviceAction(DeviceAction deviceAction);
        int UpdateDeviceAction(DeviceAction deviceAction);
        int DeleteDeviceAction(int deviceActionId);
        IEnumerable<DeviceAction> SearchDeviceAction(string searchTerm);
        DeviceAction GetDeviceAction(int deviceActionId);
        IEnumerable<DeviceAction> GetAllDeviceAction();
    }
}
