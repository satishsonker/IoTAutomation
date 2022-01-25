using IoT.DataLayer.Interface;
using IoT.ModelLayer;
using IoT.WebAPI.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IoT.WebAPI.Mock
{
   public class DeviceControllerTest
    {
        private DeviceController deviceController;
        private Mock<IDevices> iDeviceMock;
        [SetUp]
        public void Setup()
        {
            iDeviceMock = new Mock<IDevices>();
            var iLogger = new Mock<ILogger<DeviceController>>();
            deviceController = new DeviceController(iDeviceMock.Object, iLogger.Object);
        }

        [Test]
        public async Task AddDevice()
        {
            iDeviceMock.Setup(_ => _.Add(It.IsAny<Device>(), Helper.UserKey)).Returns(Task.FromResult(new ResponseModel()));
            var result = await deviceController.AddDevice(new Device(), Helper.UserKey);
            Assert.IsTrue(result != null);
        }

        [Test]
        public async Task UpdateDevice()
        {
            iDeviceMock.Setup(_ => _.Update(It.IsAny<Device>(), Helper.UserKey)).Returns(Task.Factory.StartNew(()=>new Device() { DeviceId = 10 }));
            var result = await deviceController.UpdateDevice(new Device(), Helper.UserKey);
            Assert.IsTrue(result.DeviceId>0);
        }

        [Test]
        public async Task DeleteDevice()
        {
            iDeviceMock.Setup(_ => _.Delete(It.IsAny<string>(), Helper.UserKey)).Returns(Task.Factory.StartNew(() => new Device() { DeviceId = 10 }));
            var result = await deviceController.DeleteDevice(Helper.EndpointId, Helper.UserKey);
            Assert.IsTrue(result.DeviceId > 0);
        }
        [Test]
        public async Task GetAllDevices()
        {
            iDeviceMock.Setup(_ => _.GetAllDevices(It.IsAny<string>(),1,10, Helper.UserKey)).Returns(Task.Factory.StartNew(() => new PagingRecord()));
            var result = await deviceController.GetAllDevice(Helper.EndpointId,1,10, Helper.UserKey);
            Assert.IsTrue(result.PageNo > 0);
        }
        [Test]
        public async Task SearchDevices()
        {
            iDeviceMock.Setup(_ => _.SearchDevices(Helper.EndpointId, Helper.UserKey)).Returns(Task.Factory.StartNew(() => new List<DeviceExt>() { new DeviceExt() { DeviceId = 10 } }));
            var result = await deviceController.SearchDevice(Helper.EndpointId, Helper.UserKey);
            Assert.IsTrue(result.Count > 0);
        }
        [Test]
        public async Task GetDeviceDropdown()
        {
            iDeviceMock.Setup(_ => _.GetDeviceDropdown(Helper.UserKey)).Returns(Task.Factory.StartNew(() => new List<object>() {new object() }));
            var result = await deviceController.GetDeviceDropdown( Helper.UserKey);
            Assert.IsTrue(result.Count > 0);
        }
        [Test]
        public async Task UpdateDeviceHistory()
        {
            iDeviceMock.Setup(_ => _.UpdateDeviceHistory(Helper.UserKey,Helper.EndpointId,true)).Returns(Task.Factory.StartNew(() => true));
            var result = await deviceController.UpdateDeviceHistory(Helper.EndpointId,true, Helper.UserKey);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task GetDeviceActionType()
        {
            iDeviceMock.Setup(_ => _.GetDeviceTypeAction()).Returns(Task.Factory.StartNew(() =>new PagingRecord()));
            var result = await deviceController.GetDeviceAction();
            Assert.IsNotEmpty(result.ToString());
        }
    }
}
