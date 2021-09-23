using IoT.DataLayer.Interface;
using IoT.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IoT.DataLayer.Repository
{
    public class AdminRepository : IAdmin
    {
        private readonly AppDbContext context;
        public AdminRepository(AppDbContext context)
        {
            this.context = context;
        }

        public int AddDeviceAction(DeviceAction deviceAction, string userKey)
        {
            if (isUserExist(userKey))
            {
                context.DeviceActions.Add(deviceAction);
                return context.SaveChanges();
            }
            return 0;
        }

        public int AddDeviceCapability(DeviceCapability deviceCapability, string userKey)
        {
            if (isUserExist(userKey) && deviceCapability!=null)
            {
                context.DeviceCapabilities.Add(deviceCapability);
                return context.SaveChanges();
            }
            return 0;
        }

        public int AddDeviceType(DeviceType deviceType, string userKey)
        {
            if (isUserExist(userKey) && deviceType != null)
            {
                context.DeviceTypes.Add(deviceType);
                return context.SaveChanges();
            }
            return 0;
        }

        public int DeleteDeviceAction(int deviceActionId, string userKey)
        {
            if (isUserExist(userKey) && deviceActionId >0)
            {
                var oldDeviceType = context.DeviceActions.Where(x => x.DeciveActionId == deviceActionId).FirstOrDefault();
                if (oldDeviceType != null)
                {
                    var deviceType = context.DeviceActions.Attach(oldDeviceType);
                    deviceType.State = EntityState.Deleted;
                    return context.SaveChanges();
                }
            }
            return 0;
        }

        public int DeleteDeviceCapability(int deviceCapabilityId, string userKey)
        {
            if(isUserExist(userKey) && deviceCapabilityId>0)
            { 
            var oldDeviceCapability = context.DeviceCapabilities.Where(x => x.DeviceCapabilityId == deviceCapabilityId).FirstOrDefault();
            if (oldDeviceCapability != null)
            {
                var deviceCapability = context.DeviceCapabilities.Attach(oldDeviceCapability);
                deviceCapability.State = EntityState.Deleted;
                return context.SaveChanges();
            }
        }
            return 0;
        }

        public int DeleteDeviceType(int deviceTypeId, string userKey)
        {
            if (isUserExist(userKey) && deviceTypeId > 0)
            {
                var oldDeviceType = context.DeviceTypes.Where(x => x.DeviceTypeId == deviceTypeId).FirstOrDefault();
                if (oldDeviceType != null)
                {
                    var deviceType = context.DeviceTypes.Attach(oldDeviceType);
                    deviceType.State = EntityState.Deleted;
                    return context.SaveChanges();
                }
            }
            return 0;
        }

        public IEnumerable<DeviceAction> GetAllDeviceAction(string userKey)
        {
            var data = new List<DeviceAction>();
            if (isUserExist(userKey))
            {
                data = context.DeviceActions.Include(x => x.DeviceType).OrderBy(x => x.DeviceActionName).ToList();
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        item.DeviceType.DeviceActions = null;
                    }
                }
            }
            return data;
        }

        public IEnumerable<DeviceCapability> GetAllDeviceCapability(string userKey)
        {
            var data = new List<DeviceCapability>();
            if (isUserExist(userKey))
            {
                data = context.DeviceCapabilities.Include(x => x.DeviceType).OrderBy(x => x.CapabilityType).ToList();
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        item.DeviceType.DeviceCapabilities = null;
                    }
                }
            }
            return data;
        }

        public DeviceAction GetDeviceAction(int deviceActionId, string userKey)
        {
            var data = new DeviceAction();
            if (isUserExist(userKey) && deviceActionId > 0)
            {
                data = context.DeviceActions.Include(x => x.DeviceType).Where(x => x.DeciveActionId == deviceActionId).FirstOrDefault();
                if (data != null)
                {
                    data.DeviceType.DeviceActions = null;
                }
            }
            return data;
        }

        public DeviceCapability GetDeviceCapability(int deviceCapabilityId, string userKey)
        {
            
            if (isUserExist(userKey) && deviceCapabilityId > 0)
            {
                var data =context.DeviceCapabilities.Include(x => x.DeviceType).Where(x => x.DeviceCapabilityId == deviceCapabilityId).FirstOrDefault();
                if (data != null)
                    data.DeviceType.DeviceCapabilities = null;
                return data;
            }
            return new DeviceCapability(); ;
        }

        public DeviceType GetDeviceType(int deviceTypeId, string userKey)
        {
            if(isUserExist(userKey) && deviceTypeId>0)
            return context.DeviceTypes.Where(x => x.DeviceTypeId == deviceTypeId).FirstOrDefault();
            return new DeviceType();
        }

        public IEnumerable<DeviceAction> SearchDeviceAction(string searchTerm, string userKey)
        {
            if (isUserExist(userKey) && !string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                return context.DeviceActions.Where(x => searchTerm == "all" || x.DeviceActionName.ToLower().Contains(searchTerm) || x.DeviceActionValue.ToLower().Contains(searchTerm)).OrderBy(x => x.DeviceActionName);
}
            return new List<DeviceAction>();
        }

        public IEnumerable<DeviceCapability> SearchDeviceCapability(string searchTerm, string userKey)
        {
            if (isUserExist(userKey) && !string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                var data = context.DeviceCapabilities.Include(x => x.DeviceType).Where(x => searchTerm == "all" || x.DeviceType.DeviceTypeName.ToLower().Contains(searchTerm) || x.SupportedProperty.ToLower().Contains(searchTerm) || x.CapabilityType.ToLower().Contains(searchTerm) || x.CapabilityInterface.ToLower().Contains(searchTerm)).OrderBy(x => x.CapabilityType);
                foreach (DeviceCapability item in data)
                {
                    item.DeviceType.DeviceCapabilities = null;
                }
                return data;
            }
            return new List<DeviceCapability>();
        }

        public IEnumerable<DeviceType> SearchDeviceType(string searchTerm, string userKey)
        {
            if (isUserExist(userKey) && !string.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                return context.DeviceTypes.Where(x => searchTerm == "all" || x.DeviceTypeName.ToLower().Contains(searchTerm)).OrderBy(x => x.DeviceTypeName);
 }
            return new List<DeviceType>();
        }

        public bool UpdateAdminPermission(List<UserPermission> userPermissions, string userKey)
        {
            bool result = false;
            if (isUserExist(userKey) && userPermissions != null && userPermissions.Count > 0 && !string.IsNullOrEmpty(userKey))
            {
                if (context.Users.Include(x => x.UserPermissions).Where(x => x.UserKey == userKey && x.UserPermissions.FirstOrDefault().IsAdmin).Count() > 0)
                {
                    foreach (UserPermission userPermission in userPermissions)
                    {
                        context.UserPermissions.Attach(userPermission).State = EntityState.Modified;
                    }
                    if (context.SaveChanges() > 0)
                        result = true;
                }
            }
            return result;
        }

        public int UpdateDeviceAction(DeviceAction deviceAction, string userKey)
        {
            if (isUserExist(userKey) && deviceAction != null)
            {
                var deviceType = context.DeviceActions.Attach(deviceAction);
                deviceType.State = EntityState.Modified;
                return context.SaveChanges();
            }
            return 0;
        }

        public int UpdateDeviceCapability(DeviceCapability deviceCapability, string userKey)
        {
            if (isUserExist(userKey) && deviceCapability != null)
            {
                var oldDeviceCapability = context.DeviceCapabilities.Attach(deviceCapability);
                oldDeviceCapability.State = EntityState.Modified;
                return context.SaveChanges();
            }
            return 0;
        }

        public int UpdateDeviceType(DeviceType updateDeviceType, string userKey)
        {
            if (isUserExist(userKey) && updateDeviceType != null)
            {
                var deviceType = context.DeviceTypes.Attach(updateDeviceType);
                deviceType.State = EntityState.Modified;
                return context.SaveChanges();
            }
            return 0;
        }
        private bool isUserExist(string userKey)
        {
            bool result = false;
            if(!string.IsNullOrEmpty(userKey))
            {
                if (context.UserPermissions.Where(x => x.UserKey == userKey && x.IsAdmin).Count() > 0)
                    result = true;
            }
            return result;
        }
    }
}
