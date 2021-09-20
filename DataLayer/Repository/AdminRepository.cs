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
            context.DeviceActions.Add(deviceAction);
            return context.SaveChanges();
        }

        public int AddDeviceType(DeviceType deviceType,string userKey)
        {
            var user = context.UserPermissions.Where(x => x.UserKey == userKey).FirstOrDefault();
            if (user != null && user.IsAdmin)
            {
                context.DeviceTypes.Add(deviceType);
                return context.SaveChanges();
            }
            return 0;
        }

        public int DeleteDeviceAction(int deviceActionId, string userKey)
        {
            var oldDeviceType = context.DeviceActions.Where(x => x.DeciveActionId == deviceActionId).FirstOrDefault();
            if (oldDeviceType != null)
            {
                var deviceType = context.DeviceActions.Attach(oldDeviceType);
                deviceType.State = EntityState.Deleted;
                return context.SaveChanges();
            }
            return 0;
        }

        public int DeleteDeviceType(int deviceTypeId, string userKey)
        {
            var oldDeviceType = context.DeviceTypes.Where(x => x.DeviceTypeId == deviceTypeId).FirstOrDefault();
            if(oldDeviceType!=null)
            {
                var deviceType=context.DeviceTypes.Attach(oldDeviceType);
                deviceType.State = EntityState.Deleted;
                return context.SaveChanges();
            }
            return 0;
        }

        public IEnumerable<DeviceAction> GetAllDeviceAction(string userKey)
        {
           var data= context.DeviceActions.Include(x=>x.DeviceType).OrderBy(x => x.DeviceActionName);
            if(data!=null)
            {
                foreach (var item in data)
                {
                    item.DeviceType.DeviceActions = null;
                }
            }
            return data;
        }

        public DeviceAction GetDeviceAction(int deviceActionId, string userKey)
        {
            var data =context.DeviceActions.Include(x=>x.DeviceType).Where(x => x.DeciveActionId==deviceActionId).FirstOrDefault();
            if(data!=null)
            {
                data.DeviceType.DeviceActions = null;
            }
            return data;
        }

        public DeviceType GetDeviceType(int deviceTypeId, string userKey)
        {
            return context.DeviceTypes.Where(x => x.DeviceTypeId == deviceTypeId).FirstOrDefault();
        }

        public IEnumerable<DeviceAction> SearchDeviceAction(string searchTerm, string userKey)
        {
            searchTerm = searchTerm.ToLower();
            return context.DeviceActions.Where(x => searchTerm == "all" || x.DeviceActionName.ToLower().Contains(searchTerm) || x.DeviceActionValue.ToLower().Contains(searchTerm)).OrderBy(x => x.DeviceActionName);
        }

        public IEnumerable<DeviceType> SearchDeviceType(string searchTerm, string userKey)
        {
            searchTerm = searchTerm.ToLower();
            return context.DeviceTypes.Where(x => searchTerm == "all" || x.DeviceTypeName.ToLower().Contains(searchTerm)).OrderBy(x => x.DeviceTypeName);
}

        public bool UpdateAdminPermission(List<UserPermission> userPermissions,string userKey)
        {
            bool result = false;
          if(userPermissions!=null && userPermissions.Count>0 && !string.IsNullOrEmpty(userKey))
            {
                if(context.Users.Include(x=>x.UserPermissions).Where(x=>x.UserKey==userKey && x.UserPermissions.FirstOrDefault().IsAdmin).Count()>0)
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
            if (deviceAction != null)
            {
                var deviceType = context.DeviceActions.Attach(deviceAction);
                deviceType.State = EntityState.Modified;
                return context.SaveChanges();
            }
            return 0;
        }

        public int UpdateDeviceType(DeviceType updateDeviceType,string userKey)
        {
            if (updateDeviceType != null)
            {
                var deviceType = context.DeviceTypes.Attach(updateDeviceType);
                deviceType.State = EntityState.Modified;
                return context.SaveChanges();
            }
            return 0;
        }
    }
}
