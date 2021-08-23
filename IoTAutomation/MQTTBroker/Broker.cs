using MQTTnet;
using MQTTnet.Protocol;
using MQTTnet.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace IoT.WebAPI.MQTTBroker
{
    public class BrokerMQTT
    {
      private static MqttClient mqttClient;
        public static void StartBroker()
        {
            Task.Run(() =>
            {
                //mqttClient = new MqttClient("127.0.0.1");
                //mqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived;
                //mqttClient.Subscribe(new string[] { "Application2/Message" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                //mqttClient.Connect("Application1");
            });

        }

        private static void MqttClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            var message = Encoding.UTF8.GetString(e.Message);
        }
    }
}
