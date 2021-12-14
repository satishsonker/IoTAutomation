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
    public class DeviceRepository : CommonRepository, IDevices
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
                var Device =await context.Devices.Where(x => x.DeviceName == newDevice.DeviceName || x.FriendlyName == newDevice.FriendlyName).FirstOrDefaultAsync();
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

        public async Task<Device> Delete(string DeviceKey, string userKey)
        {
            var deleteDevice =await context.Devices.Where(x => x.DeviceKey == DeviceKey && x.UserKey == userKey).FirstOrDefaultAsync();
            var Device = context.Devices.Attach(deleteDevice);
            Device.State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            await context.SaveChangesAsync();
            return deleteDevice;
        }

        public async Task<List<DeviceExt>> GetAllDevices(string userKey, string deviceKey = "")
        {
            return await context.Devices.Include(x => x.DeviceType).Where(x => x.UserKey == userKey && (deviceKey == string.Empty || x.DeviceKey == deviceKey)).Select(x => new DeviceExt
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
                RoomKey = x.Room.RoomKey,
                CustomIdentifier=x.CustomIdentifier,
                FirmwareVersion=x.FirmwareVersion,
                Model=x.Model,
                SerialNumber=x.SerialNumber,
                SoftwareVersion=x.SoftwareVersion,
                Status=x.Status
            }).OrderBy(x => x.DeviceName).ThenBy(x => x.RoomName).ToListAsync();
        }

        public async Task<DeviceExt> GetDevice(string userKey, int DeviceId)
        {
            return await context.Devices.Where(x => x.UserKey == userKey && x.DeviceId == DeviceId).Select(x => new DeviceExt
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
                RoomKey = x.Room.RoomKey,
                CustomIdentifier = x.CustomIdentifier,
                FirmwareVersion = x.FirmwareVersion,
                Model = x.Model,
                SerialNumber = x.SerialNumber,
                SoftwareVersion = x.SoftwareVersion,
                Status = x.Status
            }).FirstOrDefaultAsync();
        }

        public async Task<List<dynamic>> GetDeviceDropdown(string userKey)
        {
            return await Task.Factory.StartNew(()=> context.Devices.Where(x => x.UserKey == userKey).Select(x => new { x.DeviceId, x.DeviceName, x.DeviceKey, x.DeviceType.DeviceTypeName }).OrderBy(x => x.DeviceName).AsEnumerable().Cast<dynamic>().ToList());
        }

        public async Task<List<DeviceType>> GetDeviceTypeAction()
        {
            return await context.DeviceTypes.Include(x => x.DeviceActions).ToListAsync();
        }

        public async Task<List<dynamic>> GetDeviceTypeDropdown(int pageNo, int pageSize)
        {
            int skipRecords = (pageNo - 1) * pageSize;
            return await Task.Factory.StartNew(() => context.DeviceTypes.Select(x => new { x.DeviceTypeId, x.DeviceTypeName }).OrderBy(x => x.DeviceTypeName).Skip(skipRecords).Take(pageSize).AsEnumerable().Cast<dynamic>().ToList());
        }

        public async Task<List<DeviceExt>> SearchDevices(string searchTerm, string userKey)
        {
            searchTerm = searchTerm.ToUpper();
            return await context.Devices.Include(x => x.DeviceType).Where(x => x.UserKey == userKey && (searchTerm == "ALL" || x.DeviceName.ToUpper().Contains(searchTerm) || x.DeviceKey.ToUpper().Contains(searchTerm) || x.DeviceType.DeviceTypeName.ToUpper().Contains(searchTerm) || x.DeviceDesc.ToUpper().Contains(searchTerm) || x.FriendlyName.ToUpper().Contains(searchTerm))).Select(x => new DeviceExt
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
                RoomKey = x.Room.RoomKey,
                CustomIdentifier = x.CustomIdentifier,
                FirmwareVersion = x.FirmwareVersion,
                Model = x.Model,
                SerialNumber = x.SerialNumber,
                SoftwareVersion = x.SoftwareVersion,
                Status = x.Status
            }).OrderBy(x => x.DeviceName).ThenBy(x => x.RoomName).ToListAsync();
        }

        public async Task<Device> Update(Device updateDevice, string userKey)
        {
            if (await context.Devices.Where(x => x.UserKey == userKey).CountAsync() > 0)
            {
                updateDevice.ModifiedDate = DateTime.Now;
                updateDevice.ManufacturerName = "Areana-IoT";
                var Device = context.Devices.Attach(updateDevice);
                Device.State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            return updateDevice;
        }

        public async Task<bool> UpdateDeviceHistory(string userKey, string deviceKey, bool isConnected)
        {
            bool isUpdated = false;
            if (userKey == "ByPassApiKey" || await context.Users.Where(x => x.UserKey == userKey).CountAsync() > 0)
            {
                var oldDevice =await context.Devices.Where(x => x.DeviceKey == deviceKey).FirstOrDefaultAsync();
                if (oldDevice != null)
                {
                    oldDevice.ModifiedDate = DateTime.Now;
                    if (isConnected)
                        oldDevice.ConnectionCount += 1;
                    oldDevice.LastConnected = DateTime.Now;
                    var attachedDevice = context.Attach(oldDevice);
                    attachedDevice.State = EntityState.Modified;
                    if (await context.SaveChangesAsync() > 0)
                        isUpdated = true;
                }
            }
            return isUpdated;
        }
    }
}
