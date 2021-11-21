using IoT.BusinessLayer;
using IoT.DataLayer.Interface;
using IoT.ModelLayer;
using IoT.ModelLayer.Alexa;
using IoT.WebAPI.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace IoT.WebAPI.Mock
{
    public class AlexaEventSourceControllerTest
    {
        private AlexaEventSourceController alexaEventSourceController;
        private Mock<IAlexaEventSource> iAlexaEventSource;
        private Mock<AlexaEventSourceBL> alexaEventSourceBL;
        private Mock<HttpClientWrapper> httpClientWrapper;
        private Mock<IOptions<AppSettingConfig>> iOption;
        [SetUp]
        public void Setup()
        {
            iAlexaEventSource = new Mock<IAlexaEventSource>();
            alexaEventSourceBL = new Mock<AlexaEventSourceBL>();
            httpClientWrapper = new Mock<HttpClientWrapper>();
            var iLogger = new Mock<ILogger<AlexaEventSourceController>>();
            iOption = new Mock<IOptions<AppSettingConfig>>();
            alexaEventSourceController = new AlexaEventSourceController(iLogger.Object, iAlexaEventSource.Object, iOption.Object);
        }

        [Test]
        public async Task UpdateCode()
        {
            iAlexaEventSource.Setup(x => x.UpdateCode(It.IsAny<string>(), Helper.UserKey)).Returns(Task.Factory.StartNew(() => true));
            var result = await alexaEventSourceController.UpdateCode(It.IsAny<string>(), Helper.UserKey);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task PressDoorbell()
        {
            DoorbellEventModel doorbellEventModel = new DoorbellEventModel()
            {
                @event = new Event()
                {
                    endpoint = new Endpoint()
                    {
                        endpointId = Helper.EndpointId,
                        scope = new Scope()
                        {
                            token = string.Empty,
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
            Tuple<RefreshTokenResponseModel, bool> refreshTokenResult = new Tuple<RefreshTokenResponseModel, bool>(new RefreshTokenResponseModel(), true);
            iAlexaEventSource.Setup(x => x.VerifyAPIKey(It.IsAny<string>())).Returns(Task.Factory.StartNew(() => true));
            httpClientWrapper.Setup(_ => _.Post(Helper.AlexaEventUrl, stringContent)).Returns(Task.Factory.StartNew(() => new HttpResponseMessage() { StatusCode = HttpStatusCode.OK }));
            iAlexaEventSource.Setup(x => x.GetToken(It.IsAny<bool>())).Returns(Task.Factory.StartNew(() => new ModelLayer.Alexa.SkillToken() { Token = string.Empty, ExpireAt = DateTime.Now.AddMinutes(10) }));
            alexaEventSourceBL.Setup(_ => _.GetRefreshToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), Helper.ApiKey)).Returns(Task.Factory.StartNew(() => refreshTokenResult));

            iOption.SetupGet(_ => _.Value).Returns(new AppSettingConfig()
            {
                AlexaEventUrl = Helper.AlexaEventUrl
            });
            var result =await alexaEventSourceController.PressDoorbell(Helper.EndpointId, Helper.UserKey).ConfigureAwait(false);
           
                Assert.IsTrue(result == "error");
        }
    }
}

