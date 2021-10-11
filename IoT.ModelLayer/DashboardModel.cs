using System;
using System.Collections.Generic;
using System.Text;

namespace IoT.ModelLayer
{
   public class DashboardModel
    {
        public ICollection<DeviceExt> Devices { get; set; }
        public ICollection<Room> Rooms { get; set; }
    }
}
