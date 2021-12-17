using IoT.DataLayer.Interface;
using IoT.ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IoT.BusinessLayer
{
    public class AdminBL
    {
        private readonly IAdmin _adminBL;
        public AdminBL(IAdmin iadmin)
        {
            _adminBL = iadmin;
        }
        public async Task<int> AddDeviceType(DeviceType deviceType,string userKey)
        {
            return await _adminBL.AddDeviceType(deviceType,userKey);
        }

        public async Task<int> UpdateDeviceType(DeviceType deviceType, string userKey)
        {
            return await _adminBL.UpdateDeviceType(deviceType, userKey);
        }

        public async Task<int> DeleteDeviceType(int deviceTypeId, string userKey)
        {
            return await _adminBL.DeleteDeviceType(deviceTypeId, userKey);
        }

        public async Task<List<DeviceType>> SearchDeviceType(string searchTerm, string userKey)
        {
            return await _adminBL.SearchDeviceType(searchTerm, userKey);
        }

        public async Task<DeviceType> GetDeviceType(int deviceTypeId, string userKey)
        {
            return await _adminBL.GetDeviceType(deviceTypeId, userKey);
        }
        public async Task<IEnumerable<DeviceAction>> GetAllDeviceType(string userKey)
        {
            return await _adminBL.GetAllDeviceAction(userKey);
        }

        public async Task<int> AddDeviceAction(DeviceAction deviceAction, string userKey)
        {
            if(deviceAction!=null)
            {
                deviceAction.CreatedDate = DateTime.Now;
                deviceAction.ModifiedDate = DateTime.Now;
            }
            return await _adminBL.AddDeviceAction(deviceAction, userKey);
        }

        public async Task<int> UpdateDeviceAction(DeviceAction deviceAction, string userKey)
        {
            return await _adminBL.UpdateDeviceAction(deviceAction,userKey);
        }

        public async Task<int> DeleteDeviceAction(int deviceActionId, string userKey)
        {
            return await _adminBL.DeleteDeviceAction(deviceActionId, userKey);
        }

        public async Task<IEnumerable<DeviceAction>> SearchDeviceAction(string searchTerm,string userKey)
        {
            return await _adminBL.SearchDeviceAction(searchTerm, userKey);
        }

        public async Task<DeviceAction> GetDeviceAction(int deviceActionId, string userKey)
        {
            return await _adminBL.GetDeviceAction(deviceActionId, userKey);
        }
        public async Task<bool> UpdateAdminPermission(List<UserPermission> userPermissions, string userKey)
        {
            if(userPermissions!=null)
            {
                foreach (UserPermission userPermission in userPermissions)
                {
                    userPermission.ModifiedDate = DateTime.Now;
                }
            }
            return await _adminBL.UpdateAdminPermission(userPermissions, userKey);
        }

        public async Task<IEnumerable<DeviceAction>> GetAllDeviceAction(string userKey)
        {
            throw new NotImplementedException();
        }

        public async Task<int> AddDeviceCapability(DeviceCapability deviceCapability, string userKey)
        {
            if (deviceCapability != null)
                deviceCapability.CreatedDate = DateTime.Now;
            return await _adminBL.AddDeviceCapability(deviceCapability, userKey);
        }

        public async Task<int> UpdateDeviceCapability(DeviceCapability deviceCapability, string userKey)
        {
            if (deviceCapability != null)
                deviceCapability.ModifiedDate = DateTime.Now;
            return await _adminBL.UpdateDeviceCapability(deviceCapability, userKey);
        }

        public async Task<int> DeleteDeviceCapability(int deviceCapabilityId, string userKey)
        {
            return await _adminBL.DeleteDeviceCapability(deviceCapabilityId, userKey);
        }

        public async Task<IEnumerable<DeviceCapability>> SearchDeviceCapability(string searchTerm, string userKey)
        {
            return await _adminBL.SearchDeviceCapability(searchTerm, userKey);
        }

        public async Task<DeviceCapability> GetDeviceCapability(int deviceCapabilityId, string userKey)
        {
            return await _adminBL.GetDeviceCapability(deviceCapabilityId, userKey);
        }

        public async Task<PagingRecord> GetAllDeviceCapability(string userKey,int PageNo, int PageSize)
        {
            return await _adminBL.GetAllDeviceCapability(userKey,PageNo,PageSize);
        }
    }
}
