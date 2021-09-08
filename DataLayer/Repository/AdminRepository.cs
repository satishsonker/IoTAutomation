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

        public int AddDeviceAction(DeviceAction deviceAction)
        {
            context.DeviceActions.Add(deviceAction);
            return context.SaveChanges();
        }

        public int AddDeviceType(DeviceType deviceType)
        {
            context.DeviceTypes.Add(deviceType);
            return context.SaveChanges();
        }

        public int DeleteDeviceAction(int deviceActionId)
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

        public int DeleteDeviceType(int deviceTypeId)
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

        public IEnumerable<DeviceAction> GetAllDeviceAction()
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

        public DeviceAction GetDeviceAction(int deviceActionId)
        {
            var data =context.DeviceActions.Where(x => x.DeciveActionId==deviceActionId).FirstOrDefault();
            if(data!=null)
            {
                data.DeviceType.DeviceActions = null;
            }
            return data;
        }

        public DeviceType GetDeviceType(int deviceTypeId)
        {
            return context.DeviceTypes.Where(x => x.DeviceTypeId == deviceTypeId).FirstOrDefault();
        }

        public IEnumerable<DeviceAction> SearchDeviceAction(string searchTerm)
        {
            searchTerm = searchTerm.ToLower();
            return context.DeviceActions.Where(x => searchTerm == "all" || x.DeviceActionName.ToLower().Contains(searchTerm) || x.DeviceActionValue.ToLower().Contains(searchTerm)).OrderBy(x => x.DeviceActionName);
        }

        public IEnumerable<DeviceType> SearchDeviceType(string searchTerm)
        {
            searchTerm = searchTerm.ToLower();
            return context.DeviceTypes.Where(x => searchTerm == "all" || x.DeviceTypeName.ToLower().Contains(searchTerm)).OrderBy(x => x.DeviceTypeName);
}

        public int UpdateDeviceAction(DeviceAction deviceAction)
        {
            if (deviceAction != null)
            {
                var deviceType = context.DeviceActions.Attach(deviceAction);
                deviceType.State = EntityState.Modified;
                return context.SaveChanges();
            }
            return 0;
        }

        public int UpdateDeviceType(DeviceType updateDeviceType)
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
