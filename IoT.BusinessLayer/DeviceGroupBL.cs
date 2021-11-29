using IoT.DataLayer.Interface;
using IoT.ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IoT.BusinessLayer
{
    public class DeviceGroupBL
    {
        private readonly IDeviceGroup _deviceGroup;
        public DeviceGroupBL(IDeviceGroup deviceGroup)
        {
            this._deviceGroup = deviceGroup;
        }
        public async Task<int> AddGroup(DeviceGroup deviceGroup, string userKey)
        {
            if (deviceGroup == null)
                return 0;
            deviceGroup.CreatedDate = DateTime.Now;
            deviceGroup.ModifiedDate = DateTime.Now;
            deviceGroup.GroupKey = CommonBL.GetGuid();
            deviceGroup.UserKey = userKey;
            return await _deviceGroup.AddGroup(deviceGroup, userKey);
        }

        public async Task<int> AddGroupDetail(List<DeviceGroupDetail> deviceGroupDetails, string groupKey, string userKey)
        {
            return await _deviceGroup.AddGroupDetail(deviceGroupDetails, groupKey, userKey);
        }

        public async Task<int> DeleteGroup(int groupId, string userKey)
        {
            return await _deviceGroup.DeleteGroup(groupId, userKey);
        }

        public async Task<DeviceGroup> GetGroup(int groupId, string userKey)
        {
            return await _deviceGroup.GetGroup(groupId, userKey);
        }

        public async Task<List<DeviceGroup>> GetGroups(string userKey)
        {
            return await _deviceGroup.GetGroups(userKey);
        }

        public async Task<List<DeviceGroup>> SearchGroup(string searchTerm, string userKey)
        {
            searchTerm = string.IsNullOrEmpty(searchTerm) ? "All" : searchTerm;
            return await _deviceGroup.SearchGroup(searchTerm, userKey);
        }
        public async Task<List<DeviceGroupDetail>> GetGroupDetails(string groupKey, string userKey)
        {
            return await _deviceGroup.GetGroupDetails(groupKey, userKey);
        }
        public async Task<int> UpdateGroup(DeviceGroup deviceGroup, string userKey)
        {
            if (deviceGroup == null)
                return 0;
            deviceGroup.ModifiedDate = DateTime.Now;
            return await _deviceGroup.UpdateGroup(deviceGroup, userKey);
        }
    }
}
