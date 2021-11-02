using IoT.DataLayer.Interface;
using IoT.ModelLayer.Alexa;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace IoT.BusinessLayer
{
    public class AlexaEventSourceBL
    {
        private readonly IAlexaEventSource _alexaEventSource;
        private readonly HttpClientWrapper httpClientWrapper;
        public AlexaEventSourceBL(IAlexaEventSource ialexaEventSource)
        {
            _alexaEventSource = ialexaEventSource;
            httpClientWrapper = new HttpClientWrapper();
        }
        private string GetRefreshToken()
        {
            return _alexaEventSource.GetRefreshToken();
        }

        private Tuple<string, string, DateTime> GetToken()
        {
            return _alexaEventSource.GetToken();
        }

        public void UpdateCode(string code, string userKey)
        {
            _alexaEventSource.UpdateCode(code, userKey);
        }

        public void UpdateToken(string token, string refreshToken, int expireMin, string userKey)
        {
            _alexaEventSource.UpdateToken(token, refreshToken, expireMin, userKey);
        }
        public void PressDoorbell(string endpoitnId, string apiKey)
        {
            if (_alexaEventSource.VerifyAPIKey(apiKey))
            {

                var token = GetToken();
                if (token.Item3 > DateTime.Now)
                {
                    DoorbellEventModel doorbellEventModel = new DoorbellEventModel()
                    {
                        @event = new Event()
                        {
                            endpoint = new Endpoint()
                            {
                                endpointId = endpoitnId,
                                scope = new Scope()
                                {
                                    token = token.Item1,
                                    type = "BearerToken"
                                }
                            },
                            header = new Header()
                            {
                                messageId = Guid.NewGuid().ToString(),
                                name = "DoorbellPress",
                                payloadVersion = "3",
                                @namespace = "Alexa.DoorbellEventSource"
                            },
                            payload = new Payload()
                            {
                                cause = new Cause()
                                {
                                    type = "PHYSICAL_INTERACTION"
                                },
                                timestamp = DateTime.Now
                            }
                        }
                    };
                    StringContent stringContent = new StringContent(JsonConvert.SerializeObject(doorbellEventModel));
                    httpClientWrapper.Post("https://api.eu.amazonalexa.com/v3/events", stringContent);
                }
            }
        }
    }
}
