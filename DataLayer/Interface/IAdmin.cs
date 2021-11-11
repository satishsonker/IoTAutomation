using IoT.ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IoT.DataLayer.Interface
{
  public  interface IAdmin
    {
        Task<int> AddDeviceType(DeviceType deviceType, string userKey);
        Task<int> UpdateDeviceType(DeviceType deviceType, string userKey);
        Task<int> DeleteDeviceType(int deviceTypeId, string userKey);
        Task<List<DeviceType>> SearchDeviceType(string searchTerm, string userKey);
        Task<DeviceType> GetDeviceType(int deviceTypeId, string userKey);

        Task<int> AddDeviceAction(DeviceAction deviceAction, string userKey);
        Task<int> UpdateDeviceAction(DeviceAction deviceAction, string userKey);
        Task<int> DeleteDeviceAction(int deviceActionId, string userKey);
        Task<List<DeviceAction>> SearchDeviceAction(string searchTerm, string userKey);
        Task<DeviceAction> GetDeviceAction(int deviceActionId, string userKey);
        Task<List<DeviceAction>> GetAllDeviceAction(string userKey);
        Task<bool> UpdateAdminPermission(List<UserPermission> userPermissions, string userKey);
        Task<int> AddDeviceCapability(DeviceCapability deviceCapability, string userKey);
        Task<int> UpdateDeviceCapability(DeviceCapability deviceCapability, string userKey);
        Task<int> DeleteDeviceCapability(int deviceCapabilityId, string userKey);
        Task<List<DeviceCapability>> SearchDeviceCapability(string searchTerm, string userKey);
        Task<DeviceCapability> GetDeviceCapability(int deviceCapabilityId, string userKey);
        Task<List<DeviceCapability>> GetAllDeviceCapability(string userKey);
    }
}
