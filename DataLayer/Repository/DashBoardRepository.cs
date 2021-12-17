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
    public class DashBoardRepository : IDashboard
    {
        private readonly AppDbContext context;
        public DashBoardRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<DashboardModel> GetDashboardData(string userKey)
        {
            DashboardModel dashModel = new DashboardModel
            {
                Devices = await context.Devices.Where(x => x.UserKey == userKey).Select(x => new DeviceExt
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
                    IsAlexaCompatible = x.DeviceType.IsAlexaCompatible,
                    IsGoogleCompatible = x.DeviceType.IsGoogleCompatible
                }).ToListAsync(),
                Rooms =await context.Rooms.Where(x => x.UserKey == userKey).OrderBy(x => x.RoomName).ToListAsync()
            };
            return dashModel;

        }
    }
}
