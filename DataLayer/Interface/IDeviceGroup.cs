using IoT.ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IoT.DataLayer.Interface
{
    public interface IDeviceGroup
    {
        Task<int> AddGroup(DeviceGroup deviceGroup,string userKey);
        Task<int> AddGroupDetail(List<DeviceGroupDetail> deviceGroupDetails, string groupKey, string userKey);
        Task<List<DeviceGroupDetail>> GetGroupDetails(string groupKey, string userKey);
        Task<int> UpdateGroup(DeviceGroup deviceGroup, string userKey);
        Task<int> DeleteGroup(string groupKey, string userKey);
        Task<List<DeviceGroup>> SearchGroup(string searchTerm, string userKey);
        Task<List<DeviceGroup>> GetGroups(string userKey);
        Task<DeviceGroup> GetGroup(string groupKey,string userKey);
    }
}
