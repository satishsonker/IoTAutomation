using System;
using System.Collections.Generic;
using System.Text;

namespace IoT.ModelLayer
{
   public class DeviceExt:Device
    {
        public string RoomName { get; set; }
        public string RoomKey { get; set; }
        public string DeviceTypeName { get; set; }
    }
}
