using IoT.DataLayer.Interface;
using IoT.ModelLayer;
using IoT.ModelLayer.Alexa;
using Microsoft.Extensions.Options;
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
        private readonly IOptions<AppSettingConfig> config;
        public AlexaEventSourceBL(IAlexaEventSource ialexaEventSource,IOptions<AppSettingConfig> _config)
        {
            _alexaEventSource = ialexaEventSource;
            config = _config;
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
                string newToken = null;
                var token = new SkillToken();
            again:
                token = _alexaEventSource.GetToken(true);
                if (newToken!=null || (token != null && token.ExpireAt > DateTime.Now))
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
                                    token = newToken == null ? token.Token : newToken,
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
                    var response = httpClientWrapper.Post(config.Value.AlexaEventUrl, stringContent).Result;
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        var rtoken = GetRefreshToken(token.RefreshToken, token.ClientId, token.ClientSecret, apiKey);
                        newToken = rtoken.Item1.access_token;
                        goto again;
                    }
                    if (response.IsSuccessStatusCode)
                    {
                        return "success";
                    }
                }
                else
                {
                    var rtoken = GetRefreshToken(token.RefreshToken, token.ClientId, token.ClientSecret, apiKey);
                    newToken = rtoken.Item1.access_token;
                    goto again;
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
                    string newToken = null;
                    var token = new SkillToken();
                again:
                    token = _alexaEventSource.GetToken(true);
                    if (newToken!=null || (token != null && token.ExpireAt > DateTime.Now))
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
                                        token = newToken == null ? token.Token : newToken,
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
                                       value=new Value()
                                       {
                                           value="OK"
                                       },
                                        timeOfSample= DateTime.Now,
                                        uncertaintyInMilliseconds= 0
                                   }
                                }
                            }
                        };
                        StringContent stringContent = new StringContent(JsonConvert.SerializeObject(motionDetectModel));
                        var response = httpClientWrapper.Post(config.Value.AlexaEventUrl, stringContent).Result;
                        if (response.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            var rtoken = GetRefreshToken(token.RefreshToken, token.ClientId, token.ClientSecret, apiKey);
                            newToken = rtoken.Item1.access_token;
                            goto again;
                        }
                        if (response.IsSuccessStatusCode)
                        {
                            return "success";
                        }
                    }
                    else
                    {
                        var rtoken = GetRefreshToken(token.RefreshToken, token.ClientId, token.ClientSecret, apiKey);
                        newToken = rtoken.Item1.access_token;
                        goto again;
                    }
                }
              
            }
            return "error";
        }
        private Tuple<RefreshTokenResponseModel, bool> GetRefreshToken(string refreshToken, string clientId, string clientSecret, string apiKey)
        {
            List<KeyValuePair<string, string>> keyValuePair = new List<KeyValuePair<string, string>>();
            keyValuePair.Add(new KeyValuePair<string, string>("grant_type", "refresh_token"));
            keyValuePair.Add(new KeyValuePair<string, string>("refresh_token", refreshToken));
            keyValuePair.Add(new KeyValuePair<string, string>("client_id", clientId));
            keyValuePair.Add(new KeyValuePair<string, string>("client_secret", clientSecret));
            var req = new HttpRequestMessage(HttpMethod.Post, config.Value.AlexaTokenUrl) { Content = new FormUrlEncodedContent(keyValuePair) };
            var refreshTokenResponse = httpClientWrapper.PostFormData(req).Result;
            var data = refreshTokenResponse.Content.ReadAsStringAsync();
            if (refreshTokenResponse.IsSuccessStatusCode)
            {
                var refreshTokenObj = JsonConvert.DeserializeObject<RefreshTokenResponseModel>(data.Result);
                _alexaEventSource.UpdateToken(refreshTokenObj.access_token, refreshTokenObj.refresh_token, refreshTokenObj.expires_in, apiKey);
                return new Tuple<RefreshTokenResponseModel, bool>(refreshTokenObj, true);
            }
            return new Tuple<RefreshTokenResponseModel, bool>(new RefreshTokenResponseModel(), false);
        }
    }
}
