using IoT.BusinessLayer;
using IoT.DataLayer.Interface;
using IoT.ModelLayer;
using IoT.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoT.WebAPI.Mock
{
    public class AlexaPayloadControllerTest
    {
        private AlexaPayloadController alexaPayloadController;
        private Mock<AlexaPayloadBL> alexaPayloadBL;
        Mock<IAlexaPayload> iAlexa;

        [SetUp]
        public void Setup()
        {
            iAlexa=new Mock<IAlexaPayload>();
            alexaPayloadBL = new Mock<AlexaPayloadBL>();
            var iLogger = new Mock<ILogger<AlexaPayloadController>>();
            alexaPayloadController = new AlexaPayloadController(iAlexa.Object, iLogger.Object);
        }

        [Test]
        public async Task Get_AlexaDiscoveryPayload()
        {
            iAlexa.Setup(_ => _.GetAlexaDiscoveryPayload(Helper.UserKey)).Returns(Task.Factory.StartNew(()=> new List<Device>() { new Device() }));
            var result=await alexaPayloadController.GetAllDevice(Helper.UserKey);
            Assert.IsTrue(result.Count > 0);
        }
        [Test]
        public async Task Get_DeviceStatus()
        {
            iAlexa.Setup(_ => _.GetDeviceStatus(Helper.EndpointId, Helper.UserKey)).Returns(Task.FromResult("ON"));
            var result =await alexaPayloadController.GetDeviceStatus(Helper.EndpointId, Helper.UserKey);
            Assert.IsTrue(result=="ON" || result=="OFF");
        }

        [Test]
        public async Task Update_DeviceStatus()
        {
            iAlexa.Setup(_ => _.UpdateDeviceStatus(Helper.EndpointId,"OFF", Helper.UserKey)).Returns(Task.FromResult(true));
            var result = await alexaPayloadController.UpdateDeviceStatus(Helper.EndpointId, "OFF", Helper.UserKey);
            Assert.IsTrue(result);
        }
    }
}
