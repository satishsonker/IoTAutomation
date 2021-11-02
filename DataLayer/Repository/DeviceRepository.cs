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
    public class DeviceRepository : CommonRepository,IDevices
    {
        private readonly AppDbContext context;

        public DeviceRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<ResponseModel> Add(Device newDevice, string userKey)
        {
            ResponseModel responseModel = new ResponseModel();
            if (context.Users.Where(x => x.UserKey == userKey).Count() > 0)
            {
                var Device = context.Devices.Where(x => x.DeviceName == newDevice.DeviceName || x.FriendlyName==newDevice.FriendlyName).FirstOrDefault();
                if (Device == null)
                {
                    newDevice.CreatedDate = DateTime.Now;
                    context.Devices.Add(newDevice);
                    var result = await context.SaveChangesAsync();
                    if (result > 0)
                        return CommonRepository.GetResponseModel("Data saved", MessageTypes.Saved);
                    return CommonRepository.GetResponseModel("Data not saved", MessageTypes.NotSaved);
                }
                else
                    return CommonRepository.GetResponseModel($"Device name {newDevice.DeviceName} or {newDevice.FriendlyName} already existed!", MessageTypes.Duplicate);
            }
            return CommonRepository.GetResponseModel("Invalid userkey", MessageTypes.Unauthorized);
        }

        public Device Delete(string DeviceKey, string userKey)
        {
            var deleteDevice = context.Devices.Where(x => x.DeviceKey == DeviceKey && x.UserKey == userKey).FirstOrDefault();
            var Device = context.Devices.Attach(deleteDevice);
            Device.State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            context.SaveChangesAsync();
            return deleteDevice;
        }

        public IEnumerable<DeviceExt> GetAllDevices(string userKey, string deviceKey = "")
        {
            return context.Devices.Include(x => x.DeviceType).Where(x => x.UserKey == userKey && (deviceKey == string.Empty || x.DeviceKey == deviceKey)).Select(x => new DeviceExt
            {
                ConnectionCount = x.ConnectionCount,
                DeviceDesc = x.DeviceDesc,
                DeviceName = x.DeviceName,
                DeviceKey = x.DeviceKey,
                DeviceId = x.DeviceId,
                DeviceTypeId = x.DeviceType.DeviceTypeId,
                LastConnected = x.LastConnected,
                FriendlyName=x.FriendlyName,
                ManufacturerName=x.ManufacturerName,
                RoomName = x.Room.RoomName,
                DeviceTypeName = x.DeviceType.DeviceTypeName,
                RoomId = x.RoomId,
                RoomKey = x.Room.RoomKey
            }).ToList().OrderBy(x => x.DeviceName).ThenBy(x => x.RoomName);
        }

        public DeviceExt GetDevice(string userKey, int DeviceId)
        {
            return context.Devices.Where(x => x.UserKey == userKey && x.DeviceId == DeviceId).Select(x => new DeviceExt
            {
                ConnectionCount = x.ConnectionCount,
                DeviceDesc = x.DeviceDesc,
                DeviceName = x.DeviceName,
                DeviceKey = x.DeviceKey,
                DeviceId = x.DeviceId,
                LastConnected = x.LastConnected,
                RoomName = x.Room.RoomName,
                DeviceTypeName = x.DeviceType.DeviceTypeName,
                RoomId = x.RoomId,
                RoomKey = x.Room.RoomKey
            }).FirstOrDefault();
        }

        public IEnumerable<object> GetDeviceDropdown(string userKey)
        {
            return context.Devices.Where(x => x.UserKey == userKey).Select(x => new { x.DeviceId, x.DeviceName, x.DeviceKey, x.DeviceType.DeviceTypeName }).OrderBy(x => x.DeviceName).ToList();
        }

        public IEnumerable<DeviceType> GetDeviceTypeAction()
        {
            var deviceTypes = context.DeviceTypes.Include(x => x.DeviceActions).ToList();
            foreach (var deviceType in deviceTypes)
            {
                foreach (var deviceAction in deviceType.DeviceActions)
                {
                    deviceAction.DeviceType = null;
                }
            }
            return deviceTypes;
        }

        public IEnumerable<object> GetDeviceTypeDropdown(int pageNo, int pageSize)
        {
            int skipRecords = (pageNo - 1) * pageSize;
            return context.DeviceTypes.Select(x => new { x.DeviceTypeId, x.DeviceTypeName }).OrderBy(x => x.DeviceTypeName).ToList().Skip(skipRecords).Take(pageSize);
        }

        public IEnumerable<Device> SearchDevices(string searchTerm, string userKey )
        {
            searchTerm = searchTerm.ToUpper();
            var result = context.Devices.Include(x => x.DeviceType).Where(x => x.UserKey == userKey && (searchTerm == "ALL" || x.DeviceName.ToUpper().Contains(searchTerm) || x.DeviceKey.ToUpper().Contains(searchTerm) || x.DeviceType.DeviceTypeName.ToUpper().Contains(searchTerm) || x.DeviceDesc.ToUpper().Contains(searchTerm) || x.FriendlyName.ToUpper().Contains(searchTerm))).Select(x => new DeviceExt
            {
                ConnectionCount = x.ConnectionCount,
                DeviceDesc = x.DeviceDesc,
                DeviceName = x.DeviceName,
                DeviceKey = x.DeviceKey,
                DeviceId = x.DeviceId,
                DeviceTypeId = x.DeviceType.DeviceTypeId,
                LastConnected = x.LastConnected,
                FriendlyName = x.FriendlyName,
                ManufacturerName = x.ManufacturerName,
                RoomName = x.Room.RoomName,
                DeviceTypeName = x.DeviceType.DeviceTypeName,
                RoomId = x.RoomId,
                RoomKey = x.Room.RoomKey
            }).ToList().OrderBy(x => x.DeviceName).ThenBy(x => x.RoomName);
            return result;
            //.Where(x => x.UserKey == userKey)
            //.Include(x => x.Room)
            //.Include(x => x.DeviceType).ToList().Where(x => ).OrderBy(x => x.DeviceName);
            //return result;

        }

        public Device Update(Device updateDevice, string userKey)
        {
            if (context.Devices.Where(x => x.UserKey == userKey).Count() > 0)
            {
                updateDevice.ModifiedDate = DateTime.Now;
                updateDevice.ManufacturerName = "Areana-IoT";
                var Device = context.Devices.Attach(updateDevice);
                Device.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChangesAsync();
            }
            return updateDevice;
        }

        public bool UpdateDeviceHistory(string userKey, string deviceKey, bool isConnected)
        {
            bool isUpdated = false;
            if (context.Users.Where(x => x.UserKey == userKey).Count() > 0)
            {
                var oldDevice = context.Devices.Where(x => x.DeviceKey == deviceKey).FirstOrDefault();
                if (oldDevice != null)
                {
                    oldDevice.ModifiedDate = DateTime.Now;
                    if (isConnected)
                        oldDevice.ConnectionCount += 1;
                    oldDevice.LastConnected = DateTime.Now;
                    var attachedDevice = context.Attach(oldDevice);
                    attachedDevice.State = EntityState.Modified;
                    if (context.SaveChanges() > 0)
                        isUpdated = true;
                }
            }
            return isUpdated;
        }
    }
}
