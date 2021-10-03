using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Lambda.Core;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AlexaSmartHomeLambda
{
    public class AlexaHandler
    {
        public Stream Handler(Stream inputStream, ILambdaContext context)
        {
            StreamReader reader = new StreamReader(inputStream);
            string request = reader.ReadToEnd();

            Console.Out.WriteLine("Request:");
            Console.Out.WriteLine(request);

            if (context != null)
            {
                context.Logger.Log("Request:");
                context.Logger.Log(request);
            }

            AlexaResponse ar=new AlexaResponse();

            JObject jsonRequest = JObject.Parse(request);
            string nameSpace = jsonRequest["directive"]["header"]["namespace"].Value<string>();
            switch (nameSpace)
            {
                case "Alexa.Authorization":
                    if (context != null)
                        context.Logger.Log("Alexa.Authorization Request");
                    ar = new AlexaResponse("Alexa.Authorization", "AcceptGrant.Response");
                    break;

                case "Alexa.Discovery":
                    if (context != null)
                        context.Logger.Log("Alexa.Discovery Request @"+DateTime.Now);
                    JArray Data = null;
                    HttpClient httpClient = new HttpClient();
                    var httpResponse = httpClient.GetAsync("http://www.iotHomeAutomation.somee.com/api/v1/AlexaPayload/GetAlexaDiscoveryPayload").Result;
                    if (httpResponse.IsSuccessStatusCode)
                    {
                        string httpResponseString = httpResponse.Content.ReadAsStringAsync().Result;
                        Data = JArray.Parse(httpResponseString);
                        if (context != null)
                        {
                            context.Logger.Log("Alexa.Discovery API response");
                            context.Logger.Log(httpResponseString);
                        }
                    }
                    foreach (var item in Data)
                    {
                        var deviceData = JObject.Parse(item.ToString());
                        ar = new AlexaResponse("Alexa.Discovery", "Discover.Response", item["deviceKey"].ToString());

                        JObject capabilityAlexa = JObject.Parse(ar.CreatePayloadEndpointCapability());
                        foreach (var deviceCap in JArray.Parse(item["deviceType"]["deviceCapabilities"].ToString()))
                        {
                            JObject propertyPowerstate = new JObject();
                            propertyPowerstate.Add("name", deviceCap["supportedProperty"]);
                            JObject capabilityAlexaPowerController = JObject.Parse(ar.CreatePayloadEndpointCapability(deviceCap["capabilityType"].ToString(), deviceCap["capabilityInterface"].ToString(), deviceCap["version"].ToString(), propertyPowerstate.ToString()));

                            JArray capabilities = new JArray();
                            capabilities.Add(capabilityAlexa);
                            capabilities.Add(capabilityAlexaPowerController);

                            ar.AddPayloadEndpoint(item["deviceKey"].ToString(), capabilities.ToString(),item["friendlyName"].ToString(), item["deviceDesc"].ToString(), item["manufacturerName"].ToString());
                        }                       
                    }
                    break;

                case "Alexa.PowerController":
                    if (context != null)
                        context.Logger.Log("Alexa.PowerController Request");
                    string correlationToken = jsonRequest["directive"]["header"]["correlationToken"].Value<string>();
                    string endpointId = jsonRequest["directive"]["endpoint"]["endpointId"].Value<string>();
                    string name = jsonRequest["directive"]["header"]["name"].Value<string>();

                    string state = (name == "TurnOff") ? "OFF" : "ON";

                    bool result = SendDeviceState(endpointId, "powerState", state);
                    if (result)
                    {
                        ar = new AlexaResponse("Alexa", "Response", endpointId, "INVALID", correlationToken);
                        ar.AddContextProperty("Alexa.PowerController", "powerState", state, 200);
                    }
                    else
                    {
                        JObject payloadError = new JObject();
                        payloadError.Add("type", "ENDPOINT_UNREACHABLE");
                        payloadError.Add("message", "There wa an error setting the device state.");
                        ar = new AlexaResponse("Alexa", "ErrorResponse");
                        ar.SetPayload(payloadError.ToString());
                    }

                    break;

                default:
                    if (context != null)
                        context.Logger.Log("INVALID Namespace");
                    ar = new AlexaResponse();
                    break;
            }

            string response = ar.ToString();

            if (context != null)
            {
                context.Logger.Log("Response:");
                context.Logger.Log(response);
            }

            return new MemoryStream(Encoding.UTF8.GetBytes(response));
        }

        public bool SendDeviceState(String endpointId, String state, String value)
        {
            String attributeValue = state + "Value";

            AmazonDynamoDBClient client = new AmazonDynamoDBClient(RegionEndpoint.USEast1);
            Table table = Table.LoadTable(client, "SampleSmartHome");

            Document item = new Document();
            item["ItemId"] = endpointId;
            item[attributeValue] = value;

            Task<Document> updateTask = table.UpdateItemAsync(item);
            updateTask.Wait();

            if (updateTask.Status == TaskStatus.RanToCompletion)
                return true;

            return false;
        }
    }
}
