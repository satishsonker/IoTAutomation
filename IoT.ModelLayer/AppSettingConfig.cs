using System;
using System.Collections.Generic;
using System.Text;

namespace IoT.ModelLayer
{
    public class AppSettingConfig
    {
        public string AlexaEventUrl { get; set; }
        public string AlexaTokenUrl { get; set; }
        public string MqttBroker { get; set; }
        public DeviceActionModel DeviceAction { get; set; }
    }
    public class DeviceActionModel
    {
        public string Doorbell { get; set; }
        public string MotionSensor { get; set; }
    }
}
