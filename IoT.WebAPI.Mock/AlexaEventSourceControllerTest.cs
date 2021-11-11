using IoT.BusinessLayer;
using IoT.DataLayer.Interface;
using IoT.ModelLayer;
using IoT.WebAPI.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IoT.WebAPI.Mock
{
    public class AlexaEventSourceControllerTest
    {
        private AlexaEventSourceController alexaEventSourceController;
        Mock<IAlexaEventSource> iAlexaEventSource;
        Mock<AlexaEventSourceBL> alexaEventSourceBL;
        Mock<HttpClientWrapper> httpClientWrapper;
        [SetUp]
        public void Setup()
        {
            iAlexaEventSource = new Mock<IAlexaEventSource>();
            alexaEventSourceBL = new Mock<AlexaEventSourceBL>();
            httpClientWrapper = new Mock<HttpClientWrapper>();
            var iLogger = new Mock<ILogger<AlexaEventSourceController>>();
            var iOption = new Mock<IOptions<AppSettingConfig>>();
            alexaEventSourceController = new AlexaEventSourceController(iLogger.Object, iAlexaEventSource.Object,iOption.Object);
        }

        [Test]
        public async Task UpdateCode()
        {
            iAlexaEventSource.Setup(x => x.UpdateCode(It.IsAny<string>(), Helper.UserKey)).Returns(Task.Factory.StartNew(()=>true));
          var result=await  alexaEventSourceController.UpdateCode(It.IsAny<string>(), Helper.UserKey);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task PressDoorbell()
        {
                Tuple<RefreshTokenResponseModel, bool> refreshTokenResult = new Tuple<RefreshTokenResponseModel, bool>(new RefreshTokenResponseModel(), true);
                iAlexaEventSource.Setup(x => x.VerifyAPIKey(It.IsAny<string>())).Returns(Task.Factory.StartNew(() => true));
                httpClientWrapper.Setup(_=>_.Post(It.IsAny<string>(),It.IsAny<HttpContent>())).Returns(Task.Factory.StartNew(() => new HttpResponseMessage()));
                iAlexaEventSource.Setup(x => x.GetToken(It.IsAny<bool>())).Returns(Task.Factory.StartNew(() => new ModelLayer.Alexa.SkillToken() { Token = string.Empty, ExpireAt = DateTime.Now.AddMinutes(10) }));
                alexaEventSourceBL.Setup(_ => _.GetRefreshToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), Helper.ApiKey)).Returns(Task.Factory.StartNew(() => refreshTokenResult));
                var result = await alexaEventSourceController.PressDoorbell(Helper.EndpointId, Helper.UserKey);
                Assert.IsTrue(result == "success");
        }

    }
}
