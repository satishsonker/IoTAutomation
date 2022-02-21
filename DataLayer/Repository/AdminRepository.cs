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
    public class AdminRepository : IAdmin
    {
        private readonly AppDbContext context;
        public AdminRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<int> AddDeviceAction(DeviceAction deviceAction, string userKey)
        {
            if (await isUserExist(userKey))
            {
               await context.DeviceActions.AddAsync(deviceAction);
                return context.SaveChanges();
            }
            return 0;
        }

        public async Task<int> AddDeviceCapability(DeviceCapability deviceCapability, string userKey)
        {
            if (await isUserExist(userKey) && deviceCapability != null)
            {
                await context.DeviceCapabilities.AddAsync(deviceCapability);
                return await context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> AddDeviceType(DeviceType deviceType, string userKey)
        {
            if (await isUserExist(userKey) && deviceType != null)
            {
                await context.DeviceTypes.AddAsync(deviceType);
                return context.SaveChanges();
            }
            return 0;
        }

        public async Task<int> DeleteDeviceAction(int deviceActionId, string userKey)
        {
            if (await isUserExist(userKey) && deviceActionId > 0)
            {
                var oldDeviceType =await context.DeviceActions.Where(x => x.DeciveActionId == deviceActionId).FirstOrDefaultAsync();
                if (oldDeviceType != null)
                {
                    var deviceType = context.DeviceActions.Attach(oldDeviceType);
                    deviceType.State = EntityState.Deleted;
                    return await context.SaveChangesAsync();
                }
            }
            return 0;
        }

        public async Task<int> DeleteDeviceCapability(int deviceCapabilityId, string userKey)
        {
            if (await isUserExist(userKey) && deviceCapabilityId > 0)
            {
                var oldDeviceCapability =await context.DeviceCapabilities.Where(x => x.DeviceCapabilityId == deviceCapabilityId).FirstOrDefaultAsync();
                if (oldDeviceCapability != null)
                {
                    var deviceCapability = context.DeviceCapabilities.Attach(oldDeviceCapability);
                    deviceCapability.State = EntityState.Deleted;
                    return await context.SaveChangesAsync();
                }
            }
            return 0;
        }

        public async Task<int> DeleteDeviceType(int deviceTypeId, string userKey)
        {
            if (await isUserExist(userKey) && deviceTypeId > 0)
            {
                var oldDeviceType =await context.DeviceTypes.Where(x => x.DeviceTypeId == deviceTypeId).FirstOrDefaultAsync();
                if (oldDeviceType != null)
                {
                    var deviceType = context.DeviceTypes.Attach(oldDeviceType);
                    deviceType.State = EntityState.Deleted;
                    return await context.SaveChangesAsync();
                }
            }
            return 0;
        }

        public async Task<PagingRecord> GetAllDeviceAction(string userKey,int PageNo,int PageSize)
        {
            var result = new PagingRecord();
            if (await isUserExist(userKey))
            {
                var totalRecord = await context.DeviceActions
                    .Include(x => x.DeviceType)
                    .OrderBy(x => x.DeviceActionName)
                    .ToListAsync();
                result.PageNo = PageNo;
                result.PageSize = PageSize;
                result.TotalRecord = totalRecord.Count;
                result.Data = totalRecord.Skip((PageNo - 1) * PageSize).Take(PageSize).AsEnumerable().Cast<object>().ToList();
                return result;
            }
            return result;
        }

        public async Task<PagingRecord> GetAllDeviceCapability(string userKey,int PageNo,int PageSize)
        {
            var result = new PagingRecord();  
            if (await isUserExist(userKey))
            {
               var totalRecord =await context.DeviceCapabilities
                    .Include(x => x.DeviceType)
                    .OrderBy(x => x.CapabilityType)
                    .ToListAsync();
                if (totalRecord != null)
                {
                    foreach (var item in totalRecord)
                    {
                        item.DeviceType.DeviceCapabilities = null;
                    }
                    result.PageNo = PageNo;
                    result.PageSize = PageSize;
                    result.TotalRecord = totalRecord.Count;
                    result.Data = totalRecord.Skip((PageNo - 1) * PageSize).Take(PageSize).AsEnumerable().Cast<object>().ToList();
                    return result;
                }
                return result;
            }
            return result;
        }

        public async Task<DeviceAction> GetDeviceAction(int deviceActionId, string userKey)
        {
            var data = new DeviceAction();
            if (await isUserExist(userKey) && deviceActionId > 0)
            {
                data =await context.DeviceActions.Include(x => x.DeviceType).Where(x => x.DeciveActionId == deviceActionId).FirstOrDefaultAsync();
                if (data != null)
                {
                    data.DeviceType.DeviceActions = null;
                }
            }
            return data;
        }

        public async Task<DeviceCapability> GetDeviceCapability(int deviceCapabilityId, string userKey)
        {
            if (await isUserExist(userKey) && deviceCapabilityId > 0)
            {
                var data =await context.DeviceCapabilities.Include(x => x.DeviceType).Where(x => x.DeviceCapabilityId == deviceCapabilityId).FirstOrDefaultAsync();
                if (data != null)
                    data.DeviceType.DeviceCapabilities = null;
                return data;
            }
            return new DeviceCapability(); ;
        }

        public async Task<DeviceType> GetDeviceType(int deviceTypeId, string userKey)
        {
            if (await isUserExist(userKey) && deviceTypeId > 0)
                return await context.DeviceTypes.Where(x => x.DeviceTypeId == deviceTypeId).FirstOrDefaultAsync();
            return new DeviceType();
        }

        public async Task<List<DeviceAction>> SearchDeviceAction(string searchTerm, string userKey)
        {
            if (await isUserExist(userKey) && !string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                return await context.DeviceActions.Include(x=>x.DeviceType)
                    .Where(x => searchTerm == "all" || x.DeviceActionName.ToLower().Contains(searchTerm) || x.DeviceActionValue.ToLower().Contains(searchTerm))
                    .OrderBy(x => x.DeviceActionName)
                    .ToListAsync();
            }
            return new List<DeviceAction>();
        }

        public async Task<List<DeviceCapability>> SearchDeviceCapability(string searchTerm, string userKey)
        {
            if (await isUserExist(userKey) && !string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                var data = await context.DeviceCapabilities.Include(x => x.DeviceType).Where(x => searchTerm == "all" || x.DeviceType.DeviceTypeName.ToLower().Contains(searchTerm) || x.SupportedProperty.ToLower().Contains(searchTerm) || x.CapabilityType.ToLower().Contains(searchTerm) || x.CapabilityInterface.ToLower().Contains(searchTerm)).OrderBy(x => x.CapabilityType).ToListAsync();
                foreach (DeviceCapability item in data)
                {
                    item.DeviceType.DeviceCapabilities = null;
                }
                return data;
            }
            return new List<DeviceCapability>();
        }

        public async Task<List<DeviceType>> SearchDeviceType(string searchTerm, string userKey)
        {
            if (await isUserExist(userKey) && !string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                return await context.DeviceTypes.Where(x => searchTerm == "all" || x.DeviceTypeName.ToLower().Contains(searchTerm)).OrderBy(x => x.DeviceTypeName).ToListAsync();
            }
            return new List<DeviceType>();
        }

        public async Task<bool> UpdateAdminPermission(List<UserPermission> userPermissions, string userKey)
        {
            bool result = false;
            try
            {
                if (await isUserExist(userKey) && userPermissions != null && userPermissions.Count > 0 && !string.IsNullOrEmpty(userKey))
                {
                    if (await context.Users.Include(x => x.UserPermissions).Where(x => x.UserKey == userKey && x.UserPermissions.FirstOrDefault().IsAdmin).CountAsync() > 0)
                    {
                        foreach (UserPermission userPermission in userPermissions.Where(x => x.UserPermissionId > 0).ToList())
                        {
                            context.UserPermissions.Attach(userPermission).State = EntityState.Modified;
                        }
                        if (await context.SaveChangesAsync() > 0)
                            result = true;
                        foreach (var item in userPermissions.Where(x => x.UserPermissionId == 0).ToList())
                        {
                            item.ModifiedDate = DateTime.Now;
                            item.CreatedDate = DateTime.Now;
                            context.UserPermissions.Add(item);
                        }

                        if (await context.SaveChangesAsync() > 0)
                            result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                return result;
            }
            return result;
        }

        public async Task<int> UpdateDeviceAction(DeviceAction deviceAction, string userKey)
        {
            if (await isUserExist(userKey) && deviceAction != null)
            {
                var deviceType = context.DeviceActions.Attach(deviceAction);
                deviceType.State = EntityState.Modified;
                return await context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> UpdateDeviceCapability(DeviceCapability deviceCapability, string userKey)
        {
            if (await isUserExist(userKey) && deviceCapability != null)
            {
                var oldDeviceCapability = context.DeviceCapabilities.Attach(deviceCapability);
                oldDeviceCapability.State = EntityState.Modified;
                return await context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> UpdateDeviceType(DeviceType updateDeviceType, string userKey)
        {
            if (await isUserExist(userKey) && updateDeviceType != null)
            {
                var deviceType = context.DeviceTypes.Attach(updateDeviceType);
                deviceType.State = EntityState.Modified;
                return await context.SaveChangesAsync();
            }
            return 0;
        }
        private async Task<bool> isUserExist(string userKey)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(userKey))
            {
                if (await context.UserPermissions.Where(x => x.UserKey == userKey && x.IsAdmin).CountAsync() > 0)
                    result = true;
            }
            return result;
        }
    }
}
