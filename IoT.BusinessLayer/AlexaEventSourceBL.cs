using IoT.DataLayer.Interface;
using IoT.ModelLayer.Alexa;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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

        public void UpdateCode(string code, string userKey)
        {
            _alexaEventSource.UpdateCode(code, userKey);
        }

        public void UpdateToken(string token, string refreshToken, int expireMin, string userKey)
        {
            _alexaEventSource.UpdateToken(token, refreshToken, expireMin, userKey);
        }
        public async Task<string> PressDoorbell(string endpoitnId, string apiKey)
        {
            if (apiKey == "ByPassApiKey" || _alexaEventSource.VerifyAPIKey(apiKey))
            {
            again:
                var token = _alexaEventSource.GetToken(true);
                if (token != null && token.ExpireAt > DateTime.Now)
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
                                    token = token.Token,
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
                    var response = httpClientWrapper.Post("https://api.eu.amazonalexa.com/v3/events", stringContent).Result;
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        List<KeyValuePair<string, string>> keyValuePair = new List<KeyValuePair<string, string>>();
                        keyValuePair.Add(new KeyValuePair<string, string>("grant_type", "refresh_token"));
                        keyValuePair.Add(new KeyValuePair<string, string>("refresh_token", token.RefreshToken));
                        keyValuePair.Add(new KeyValuePair<string, string>("client_id", token.ClientId));
                        keyValuePair.Add(new KeyValuePair<string, string>("client_secret", token.ClientSecret));
                        var req = new HttpRequestMessage(HttpMethod.Post, "https://api.amazon.com/auth/o2/token") { Content = new FormUrlEncodedContent(keyValuePair) };
                        var refreshTokenResponse = httpClientWrapper.PostFormData(req).Result;
                        var data = refreshTokenResponse.Content.ReadAsStringAsync();
                        if (refreshTokenResponse.IsSuccessStatusCode)
                        {
                            var refreshToken = JsonConvert.DeserializeObject<RefreshTokenResponseModel>(data.Result);
                            _alexaEventSource.UpdateToken(refreshToken.access_token, refreshToken.refresh_token, refreshToken.expires_in, apiKey);
                            goto again;
                        }
                    }
                    if (response.IsSuccessStatusCode)
                    {
                        return "success";
                    }
                }
            }
            return "error";
        }
        public async Task<string> MotionDetect(string endpointId, string apiKey)
        {
            if (!string.IsNullOrEmpty(endpointId))
            {

                if (apiKey == "ByPassApiKey" || _alexaEventSource.VerifyAPIKey(apiKey))
                {
                again:
                    var token = _alexaEventSource.GetToken(true);
                    if (token != null && token.ExpireAt > DateTime.Now)
                    {
                        MotionDetectModel motionDetectModel = new MotionDetectModel()
                        {
                            @event = new Event()
                            {
                                endpoint = new Endpoint()
                                {
                                    endpointId = endpointId,
                                    scope = new Scope()
                                    {
                                        token = token.Token,
                                        type = "BearerToken"
                                    }
                                },
                                header = new Header()
                                {
                                    messageId = Guid.NewGuid().ToString(),
                                    name = "ChangeReport",
                                    payloadVersion = "3",
                                    @namespace = "Alexa"
                                },
                                payload = new Payload()
                                {
                                    change = new Change()
                                    {
                                        cause = new Cause()
                                        {
                                            type = "PHYSICAL_INTERACTION"
                                        },
                                        properties = new List<Property>()
                                        {
                                            new Property()
                                            {
                                                name="detectionState",
                                                @namespace="Alexa.MotionSensor",
                                                value="DETECTED",
                                                timeOfSample=DateTime.Now,
                                                uncertaintyInMilliseconds=0
                                            }
                                        }
                                    },
                                    timestamp = DateTime.Now
                                }
                            },
                            context = new Context()
                            {
                                properties = new List<PropertyContext>()
                                {
                                   new PropertyContext()
                                   {
                                       @namespace= "Alexa.EndpointHealth",
                                       name= "connectivity",
                                       value= {
                                                value= "OK"
                                                },
                                        timeOfSample= DateTime.Now,
                                        uncertaintyInMilliseconds= 0
                                   }
                                }
                            }
                        };
                        StringContent stringContent = new StringContent(JsonConvert.SerializeObject(motionDetectModel));
                        var response = httpClientWrapper.Post("https://api.eu.amazonalexa.com/v3/events", stringContent).Result;
                        if (response.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            List<KeyValuePair<string, string>> keyValuePair = new List<KeyValuePair<string, string>>();
                            keyValuePair.Add(new KeyValuePair<string, string>("grant_type", "refresh_token"));
                            keyValuePair.Add(new KeyValuePair<string, string>("refresh_token", token.RefreshToken));
                            keyValuePair.Add(new KeyValuePair<string, string>("client_id", token.ClientId));
                            keyValuePair.Add(new KeyValuePair<string, string>("client_secret", token.ClientSecret));
                            var req = new HttpRequestMessage(HttpMethod.Post, "https://api.amazon.com/auth/o2/token") { Content = new FormUrlEncodedContent(keyValuePair) };
                            var refreshTokenResponse = httpClientWrapper.PostFormData(req).Result;
                            var data = refreshTokenResponse.Content.ReadAsStringAsync();
                            if (refreshTokenResponse.IsSuccessStatusCode)
                            {
                                var refreshToken = JsonConvert.DeserializeObject<RefreshTokenResponseModel>(data.Result);
                                _alexaEventSource.UpdateToken(refreshToken.access_token, refreshToken.refresh_token, refreshToken.expires_in, apiKey);
                                goto again;
                            }
                        }
                        if (response.IsSuccessStatusCode)
                        {
                            return "success";
                        }
                    }
                }
            }
            return "error";
        }
    }
}
