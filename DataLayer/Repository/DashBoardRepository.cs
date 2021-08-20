using IoT.DataLayer.Interface;
using IoT.DataLayer.Models;
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
                Devices = context.Devices.Where(x => x.UserKey == userKey).OrderBy(x => x.DeviceName).ToList(),
                Rooms = context.Rooms.Where(x => x.UserKey == userKey).OrderBy(x => x.RoomName).ToList()
            };
            return dashModel;

        }
    }
}
