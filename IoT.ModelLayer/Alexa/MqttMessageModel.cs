using System;
using System.Collections.Generic;
using System.Text;

namespace IoT.ModelLayer.Alexa
{
   public class MqttMessageModel
    {
        public string Action { get; set; }
        public string DeviceId { get; set; }
        public string Value { get; set; }
        public string Status { get; set; }
        public string WiFi { get; set; }
        public string[] Devices { get; set; }
        public string IP { get; set; }
    }
}
