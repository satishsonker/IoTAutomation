using IoT.DataLayer.Interface;
using IoT.ModelLayer;
using IoT.ModelLayer.Alexa;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace IoT.BusinessLayer
{
    public class Mqtt
    {
        private readonly MqttBL mqttBL;
        private static DeviceBL deviceBL;
        private static AlexaEventSourceBL alexaEventSourceBL;
        private static IOptions<AppSettingConfig> config;
        public Mqtt(IMqtt mqtt, IAlexaEventSource ialexaEventSource, IOptions<AppSettingConfig> _config,IDevices _devices)
        {
            mqttBL = new MqttBL(mqtt);
            deviceBL = new DeviceBL(_devices);
            config = _config;
            alexaEventSourceBL = new AlexaEventSourceBL(ialexaEventSource, config);
            MqttClient client = new MqttClient(config.Value.MqttBroker);
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            var clientId = Guid.NewGuid().ToString().Replace("-","").ToUpper();

            client.Connect(clientId);
            // subscribe to the topic "/home/temperature" with QoS 2 
            var subscribeTopic = mqttBL.GetSubscribeTopic();
            if (subscribeTopic.Length > 0)
            {
                string[] serverTopics = subscribeTopic.Select(x => x + "/server").ToArray();
                var newTopic = new string[subscribeTopic.Length + serverTopics.Length];
                subscribeTopic.CopyTo(newTopic, 0);
                serverTopics.CopyTo(newTopic, subscribeTopic.Length);
               //client.Subscribe(newTopic, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
            }
        }
        public async static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string ReceivedMessage = Encoding.UTF8.GetString(e.Message);
            var ConvertedMessage = JsonConvert.DeserializeObject<MqttMessageModel>(ReceivedMessage);
            if (ConvertedMessage != null && ConvertedMessage.Action.ToLower() == config.Value.DeviceAction.Doorbell)
            {
                await alexaEventSourceBL.PressDoorbell(ConvertedMessage.DeviceId, "ByPassApiKey");
            }
            if (ConvertedMessage != null && ConvertedMessage.Action.ToLower() == config.Value.DeviceAction.MotionSensor)
            {
                await alexaEventSourceBL.MotionDetect(ConvertedMessage.DeviceId, "ByPassApiKey");
            }
            if(!string.IsNullOrEmpty(ConvertedMessage.WiFi) && string.IsNullOrEmpty(ConvertedMessage.Status) && e.Topic.ToLower().Contains("/server"))
            {
              await  deviceBL.UpdateDeviceHistory("ByPassApiKey", ConvertedMessage.Devices[0], true);
            }
        }
    }
}
