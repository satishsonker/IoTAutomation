using IoT.DataLayer.Interface;
using IoT.ModelLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.DataLayer.Repository
{
    public class DeviceGroupRepository : IDeviceGroup
    {
        private readonly AppDbContext context;
        public DeviceGroupRepository(AppDbContext appContext)
        {
            context = appContext;
        }
        public async Task<int> AddGroup(DeviceGroup deviceGroup, string userKey)
        {
            if (await context.Users.Where(x => x.UserKey == userKey).CountAsync() > 0)
            {
                context.DeviceGroups.Add(deviceGroup);
                return await context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> AddGroupDetail(List<DeviceGroupDetail> deviceGroupDetails,string groupKey, string userKey)
        {
            if(deviceGroupDetails!=null)
            {
                int groupId =await context.DeviceGroups
                    .Where(x => x.GroupKey == groupKey && x.UserKey==userKey)
                    .Select(x => x.GroupId)
                    .FirstOrDefaultAsync();
                if (groupId > 0 && deviceGroupDetails.Any(x=>x.DeviceId!=0))
                {
                    var deleteRecord = await context.DeviceGroups
                        .Include(x=>x.DeviceGroupDetails)
                        .Where(x=>x.GroupId==groupId && x.UserKey==userKey)
                        .FirstOrDefaultAsync();
                    if (deleteRecord!=null && deleteRecord.DeviceGroupDetails!=null)
                    {
                        context.DeviceGroupDetails.RemoveRange(deleteRecord.DeviceGroupDetails);
                        await context.SaveChangesAsync();
                        foreach (var item in deviceGroupDetails)
                        {
                            item.GroupId = groupId;
                            item.UserKey = userKey;
                            item.CreatedDate = DateTime.Now;
                        }

                        context.DeviceGroupDetails.AddRange(deviceGroupDetails);
                        return await context.SaveChangesAsync(); 
                    }
                }
            }
            return 0;
        }

        public async Task<int> DeleteGroup(string groupKey, string userKey)
        {
            var oldData =await context.DeviceGroups.Where(x => x.GroupKey == groupKey && x.UserKey==userKey).FirstOrDefaultAsync();
            var entity = context.DeviceGroups.Attach(oldData);
            entity.State = EntityState.Deleted;
            return await context.SaveChangesAsync();
        }

        public async Task<DeviceGroup> GetGroup(string groupKey,string userKey)
        {
            return await context.DeviceGroups
                .Where(x => x.GroupKey == groupKey && x.UserKey==userKey)
                .FirstOrDefaultAsync();
        }

        public async Task<List<DeviceGroupDetail>> GetGroupDetails(string groupKey, string userKey)
        {
            if(string.IsNullOrEmpty(groupKey))
                return new List<DeviceGroupDetail>();
            var data= await context.DeviceGroups
                .Include(x => x.DeviceGroupDetails)
                .ThenInclude(x => x.Device)
                .ThenInclude(x=>x.DeviceType)
                .Where(x => x.GroupKey == groupKey && x.UserKey == userKey)
                .FirstOrDefaultAsync();
            if(data!=null && data.DeviceGroupDetails!=null)
            {
                return data.DeviceGroupDetails.ToList();
            }
            return new List<DeviceGroupDetail>();
        }

        public async Task<List<DeviceGroup>> GetGroups(string userKey)
        {
            return await context.DeviceGroups
                .Where(x=>x.UserKey==userKey)
                .Include(x=>x.DeviceGroupDetails)
                .ThenInclude(x=>x.Device)
                .OrderBy(x=>x.GroupName)
                .ToListAsync();
        }

        public async Task<List<DeviceGroup>> SearchGroup(string searchTerm, string userKey)
        {
            searchTerm = searchTerm.ToLower();
            return await context.DeviceGroups
                .Include(x=>x.DeviceGroupDetails)
                .ThenInclude(x=>x.Device)
                .Where(x => (searchTerm=="all" || x.GroupName.ToLower().Contains(searchTerm)) && x.UserKey==userKey)
                .OrderBy(x=>x.GroupName)
                .ToListAsync();
        }

        public async Task<int> UpdateGroup(DeviceGroup deviceGroup, string userKey)
        {
            if (await context.Users.Where(x => x.UserKey == userKey).CountAsync() > 0)
            {
                var oldData =await context.DeviceGroups.Where(x => x.GroupKey == deviceGroup.GroupKey).FirstOrDefaultAsync();
                if (oldData != null)
                {
                    oldData.GroupName = deviceGroup.GroupName;
                    oldData.ModifiedDate = deviceGroup.ModifiedDate;
                    var entity = context.DeviceGroups.Attach(oldData);
                    entity.State = EntityState.Modified;
                    return await context.SaveChangesAsync();
                }
            }
            return 0;
        }
    }
}
