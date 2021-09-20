using IoT.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoT.DataLayer.Interface
{
  public  interface IAdmin
    {
        int AddDeviceType(DeviceType deviceType, string userKey);
        int UpdateDeviceType(DeviceType deviceType, string userKey);
        int DeleteDeviceType(int deviceTypeId, string userKey);
        IEnumerable<DeviceType> SearchDeviceType(string searchTerm, string userKey);
        DeviceType GetDeviceType(int deviceTypeId, string userKey);

        int AddDeviceAction(DeviceAction deviceAction, string userKey);
        int UpdateDeviceAction(DeviceAction deviceAction, string userKey);
        int DeleteDeviceAction(int deviceActionId, string userKey);
        IEnumerable<DeviceAction> SearchDeviceAction(string searchTerm, string userKey);
        DeviceAction GetDeviceAction(int deviceActionId, string userKey);
        IEnumerable<DeviceAction> GetAllDeviceAction(string userKey);
        bool UpdateAdminPermission(List<UserPermission> userPermissions, string userKey);
    }
}
