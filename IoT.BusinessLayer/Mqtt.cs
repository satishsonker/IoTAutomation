using IoT.DataLayer.Interface;
using IoT.ModelLayer.Alexa;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace IoT.BusinessLayer
{
   public class Mqtt
    {
      private readonly  MqttBL mqttBL;
        private static  AlexaEventSourceBL alexaEventSourceBL;
        private readonly ILogger _logger;

        public Mqtt(IMqtt mqtt, IAlexaEventSource ialexaEventSource)
        {
            mqttBL = new MqttBL(mqtt);
            alexaEventSourceBL = new AlexaEventSourceBL(ialexaEventSource);
            MqttClient client = new MqttClient("broker.hivemq.com");
            client.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
            var clientId = Guid.NewGuid().ToString();

            client.Connect(clientId);
            // subscribe to the topic "/home/temperature" with QoS 2 
            var subscribeTopic = mqttBL.GetSubscribeTopic();
            if (subscribeTopic.Length > 0)
            {
                client.Subscribe(subscribeTopic, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

            }
        }
       public static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string ReceivedMessage = Encoding.UTF8.GetString(e.Message);
            var ConvertedMessage = JsonConvert.DeserializeObject<MqttMessageModel>(ReceivedMessage);
           if(ConvertedMessage!=null && ConvertedMessage.Action.ToLower()=="doorbell")
            {
                alexaEventSourceBL.PressDoorbell(ConvertedMessage.DeviceId, "ByPassApiKey");
            }
        }
    }
}
