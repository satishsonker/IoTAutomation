using System;
using System.Collections.Generic;
using System.Text;

namespace IoT.DataLayer.Models
{
   public class DashboardModel
    {
        public ICollection<Device> Devices { get; set; }
        public ICollection<Room> Rooms { get; set; }
    }
}
