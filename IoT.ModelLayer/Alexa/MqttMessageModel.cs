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
    }
}
