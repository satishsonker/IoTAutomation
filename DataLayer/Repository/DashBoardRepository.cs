using IoT.DataLayer.Interface;
using IoT.ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IoT.DataLayer.Repository
{
    public class DashBoardRepository : IDashboard
    {
        private readonly AppDbContext context;
        public DashBoardRepository(AppDbContext context)
        {
            this.context = context;
        }
        public DashboardModel GetDashboardData(string userKey)
        {
            DashboardModel dashModel = new DashboardModel
            {
                Devices = context.Devices.Where(x => x.UserKey == userKey).Select(x => new DeviceExt
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
                }).ToList(),
                Rooms = context.Rooms.Where(x => x.UserKey == userKey).OrderBy(x => x.RoomName).ToList()
            };
            return dashModel;

        }
    }
}
