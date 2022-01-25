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
        public OAuthModel OAuth { get; set; }
    }
    public class DeviceActionModel
    {
        public string Doorbell { get; set; }
        public string MotionSensor { get; set; }
    }
    public class OAuthModel
    {
        public string Issuer { get; set; }
        public string Audiance { get; set; }
        public string Secret { get; set; }
    }
}
