using MQTTnet;
using MQTTnet.Protocol;
using MQTTnet.Server;
using System;
using System.Linq;
using System.Text;

namespace IoT.MQTTBroker
{
    class Program
    {
        static void Main(string[] args)
        {
            //Configure Broker
            var optionBuilder = new MqttServerOptionsBuilder()
                .WithConnectionValidator(c =>
                {
                    Console.WriteLine($"{c.ClientId} connection validator for c.Endpoint: {c.Endpoint}");
                    c.ReasonCode = MqttConnectReasonCode.Success;
                }).WithApplicationMessageInterceptor(context =>
                {
                    Console.WriteLine("WithApplicationMessageInterceptor block mergin");
                    var newData = Encoding.UTF8.GetBytes(DateTime.Now.ToString("0"));
                    var oldData = context.ApplicationMessage.Payload;
                    var mergeData = newData.Concat(oldData).ToArray();
                    context.ApplicationMessage.Payload = mergeData;
                })
                .WithConnectionBacklog(100)
                .WithDefaultEndpointPort(1884);
            //Start Server
            var mqttBroker = new MqttFactory().CreateMqttServer();
            mqttBroker.StartAsync(optionBuilder.Build()).Wait();
            Console.WriteLine($"Broker is running {DateTime.Now.ToString()}");
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
            mqttBroker.StopAsync().Wait();
        }
    }
}
