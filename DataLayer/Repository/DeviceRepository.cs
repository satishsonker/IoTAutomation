using IoT.DataLayer.Interface;
using IoT.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IoT.DataLayer.Repository
{
    public class DeviceRepository : IDevices
    {
        private readonly AppDbContext context;
        public DeviceRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Device Add(Device newDevice)
        {
            var Device = context.Devices.Where(x => x.DeviceId == newDevice.DeviceId).FirstOrDefault();
            if (Device == null)
            {
                newDevice.CreatedDate = DateTime.Now;
                context.Devices.Add(newDevice);
                context.SaveChanges();
            }
            return newDevice;
        }

        public Device Delete(string DeviceKey)
        {
            var deleteDevice = context.Devices.Where(x => x.DeviceKey == DeviceKey).FirstOrDefault();
            var Device = context.Devices.Attach(deleteDevice);
            Device.State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            context.SaveChangesAsync();
            return deleteDevice;
        }

        public IEnumerable<DeviceExt> GetAllDevices()
        {
            return context.Devices.Select(x => new DeviceExt
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
            }).ToList().OrderBy(x=>x.DeviceName);
        }

        public DeviceExt GetDevice(int DeviceId)
        {
            return context.Devices.Select(x => new DeviceExt
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
            }).Where(x => x.DeviceId == DeviceId).FirstOrDefault();
        }

        public IEnumerable<object> GetDeviceDropdown()
        {
            return context.Devices.Select(x => new { x.DeviceId, x.DeviceName, x.DeviceKey }).OrderBy(x => x.DeviceName).ToList();
        }

        public IEnumerable<object> GetDeviceTypeDropdown()
        {
            return context.DeviceTypes.Select(x => new { x.DeviceTypeId, x.DeviceTypeName }).OrderBy(x => x.DeviceTypeName).ToList();
        }

        public IEnumerable<DeviceExt> SearchDevices(string searchTerm)
        {
            searchTerm = searchTerm.ToUpper();
            return context.Devices
                .Include(x => x.Room)
                .Include(x => x.DeviceType)
                .Select(x => new DeviceExt
                {
                    ConnectionCount = x.ConnectionCount,
                    DeviceDesc=x.DeviceDesc,
                    DeviceName=x.DeviceName,
                    DeviceKey=x.DeviceKey,
                    DeviceId=x.DeviceId,
                    LastConnected=x.LastConnected,
                    RoomName=x.Room.RoomName,
                    DeviceTypeName=x.DeviceType.DeviceTypeName,
                    RoomId=x.RoomId,
                    RoomKey=x.Room.RoomKey,
                    DeviceType=x.DeviceType
                }).ToList().Where(x => searchTerm == "All" || x.DeviceName.ToUpper().Contains(searchTerm) || x.DeviceKey.Contains(searchTerm) || x.DeviceType.DeviceTypeName.ToUpper().Contains(searchTerm) || x.DeviceDesc.ToUpper().Contains(searchTerm)).OrderBy(x => x.DeviceName);
        }

        public Device Update(Device updateDevice)
        {
            var Device = context.Devices.Attach(updateDevice);
            Device.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChangesAsync();
            return updateDevice;
        }
    }
}
