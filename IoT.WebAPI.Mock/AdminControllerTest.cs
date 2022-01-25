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
    public class AdminControllerTest
    {
        private AdminController adminController;
        Mock<IAdmin> iAdmin;
        private Task<int> returnInt;
        private Task<List<DeviceType>> returnDeviceTypeList;
        private Task<DeviceType> returnDeviceType;
        private Task<List<DeviceAction>> returnDeviceActionList;
        private Task<DeviceAction> returnDeviceAction; 
        private Task<List<DeviceCapability>> returnDeviceCapabilityList;
        private Task<DeviceCapability> returnDeviceCapability;
        [SetUp]
        public void Setup()
        {
            iAdmin = new Mock<IAdmin>();
            var iLogger = new Mock<ILogger<AdminController>>();
            adminController = new AdminController(iAdmin.Object, iLogger.Object);
            returnInt = Task.Factory.StartNew(() => 10);
            returnDeviceTypeList = Task.Factory.StartNew(() => new List<DeviceType>() { new DeviceType() });
            returnDeviceType = Task.Factory.StartNew(() => new DeviceType());
            returnDeviceActionList = Task.Factory.StartNew(() => new List<DeviceAction>() { new DeviceAction() });
            returnDeviceAction = Task.Factory.StartNew(() => new DeviceAction());
            returnDeviceCapabilityList = Task.Factory.StartNew(() => new List<DeviceCapability>() { new DeviceCapability() });
            returnDeviceCapability = Task.Factory.StartNew(() => new DeviceCapability());
        }

        [Test]
        public async Task Add_DeviceType()
        {
            iAdmin.Setup(x => x.AddDeviceType(It.IsAny<DeviceType>(), Helper.UserKey)).Returns(returnInt);
            var result = (await adminController.AddDeviceType(new DeviceType(), Helper.UserKey)) as ObjectResult;
            Assert.IsTrue((int)result.Value > 0);
        }

        [Test]
        public async Task Update_DeviceType()
        {
            iAdmin.Setup(x => x.UpdateDeviceType(It.IsAny<DeviceType>(), Helper.UserKey)).Returns(returnInt);
            var result = (await adminController.UpdateDeviceType(new DeviceType(), Helper.UserKey)) as ObjectResult;
            Assert.IsTrue((int)result.Value > 0);
        }

        [Test]
        public async Task Delete_DeviceType()
        {
            iAdmin.Setup(x => x.DeleteDeviceType(It.IsAny<int>(), Helper.UserKey)).Returns(returnInt);
            var result = (await adminController.DeleteDeviceType(Helper.DeleteId, Helper.UserKey)) as ObjectResult;
            Assert.IsTrue((int)result.Value > 0);
        }

        [Test]
        public async Task Search_DeviceType()
        {
            iAdmin.Setup(x => x.SearchDeviceType(It.IsAny<string>(), Helper.UserKey)).Returns(returnDeviceTypeList);
            var result = (await adminController.SearchDeviceType(Helper.SearchTerm, Helper.UserKey)) as ObjectResult;
            Assert.IsTrue(((List<DeviceType>)result.Value).Count>0);
        }

        [Test]
        public async Task Get_DeviceType()
        {
            iAdmin.Setup(x => x.GetDeviceType(It.IsAny<int>(), Helper.UserKey)).Returns(returnDeviceType);
            var result = (await adminController.GetDeviceType(10, Helper.UserKey)) as ObjectResult;
            Assert.IsTrue(((DeviceType)result.Value) !=null);
        }

        [Test]
        public async Task Add_DeviceAction()
        {
            iAdmin.Setup(x => x.AddDeviceAction(It.IsAny<DeviceAction>(), Helper.UserKey)).Returns(returnInt);
            var result = (await adminController.AddDeviceAction(new DeviceAction(), Helper.UserKey)) as ObjectResult;
            Assert.IsTrue((int)result.Value > 0);
        }

        [Test]
        public async Task Update_DeviceAction()
        {
            iAdmin.Setup(x => x.UpdateDeviceAction(It.IsAny<DeviceAction>(), Helper.UserKey)).Returns(returnInt);
            var result = (await adminController.UpdateDeviceAction(new DeviceAction(), Helper.UserKey)) as ObjectResult;
            Assert.IsTrue((int)result.Value > 0);
        }

        [Test]
        public async Task Delete_DeviceAction()
        {
            iAdmin.Setup(x => x.DeleteDeviceAction(It.IsAny<int>(), Helper.UserKey)).Returns(returnInt);
            var result = (await adminController.DeleteDeviceAction(Helper.DeleteId, Helper.UserKey)) as ObjectResult;
            Assert.IsTrue((int)result.Value > 0);
        }

        [Test]
        public async Task Search_DeviceAction()
        {
            iAdmin.Setup(x => x.SearchDeviceAction(It.IsAny<string>(), Helper.UserKey)).Returns(returnDeviceActionList);
            var result = (await adminController.SearchDeviceAction(Helper.SearchTerm, Helper.UserKey)) as ObjectResult;
            Assert.IsTrue(((List<DeviceAction>)result.Value).Count > 0);
        }

        [Test]
        public async Task Get_DeviceAction()
        {
            iAdmin.Setup(x => x.GetDeviceAction(It.IsAny<int>(), Helper.UserKey)).Returns(returnDeviceAction);
            var result = (await adminController.GetDeviceAction(10, Helper.UserKey)) as ObjectResult;
            Assert.IsTrue(((DeviceAction)result.Value) != null);
        }
        [Test]
        public async Task GetAll_DeviceAction()
        {
            iAdmin.Setup(x => x.GetAllDeviceAction(Helper.UserKey,1,10)).Returns(Task.Factory.StartNew(()=>new PagingRecord()));
            var result = (await adminController.GetAllDeviceAction(Helper.UserKey,1,10)) ;
            Assert.IsTrue(result.PageNo>0);
        }

        [Test]
        public async Task Add_DeviceCapability()
        {
            iAdmin.Setup(x => x.AddDeviceCapability(It.IsAny<DeviceCapability>(), Helper.UserKey)).Returns(returnInt);
            var result = (await adminController.AddDeviceCapability(new DeviceCapability(), Helper.UserKey)) as ObjectResult;
            Assert.IsTrue((int)result.Value > 0);
        }

        [Test]
        public async Task Update_DeviceCapability()
        {
            iAdmin.Setup(x => x.UpdateDeviceCapability(It.IsAny<DeviceCapability>(), Helper.UserKey)).Returns(returnInt);
            var result = (await adminController.UpdateDeviceCapability(new DeviceCapability(), Helper.UserKey)) as ObjectResult;
            Assert.IsTrue((int)result.Value > 0);
        }

        [Test]
        public async Task Delete_DeviceCapability()
        {
            iAdmin.Setup(x => x.DeleteDeviceCapability(It.IsAny<int>(), Helper.UserKey)).Returns(returnInt);
            var result = (await adminController.DeleteDeviceCapability(Helper.DeleteId, Helper.UserKey)) as ObjectResult;
            Assert.IsTrue((int)result.Value > 0);
        }

        [Test]
        public async Task Search_DeviceCapability()
        {
            iAdmin.Setup(x => x.SearchDeviceCapability(It.IsAny<string>(), Helper.UserKey)).Returns(Task.Factory.StartNew(()=>new List<DeviceCapability>() {new DeviceCapability() }));
            var result = (await adminController.SearchDeviceCapability(Helper.SearchTerm, Helper.UserKey)) as ObjectResult;
            Assert.IsTrue(((List<DeviceCapability>)result.Value).Count > 0);
        }

        [Test]
        public async Task Get_DeviceCapability()
        {
            iAdmin.Setup(x => x.GetDeviceCapability(It.IsAny<int>(), Helper.UserKey)).Returns(returnDeviceCapability);
            var result = (await adminController.GetDeviceCapability(10, Helper.UserKey)) as ObjectResult;
            Assert.IsTrue(((DeviceCapability)result.Value) != null);
        }
        [Test]
        public async Task GetAll_DeviceCapability()
        {
            iAdmin.Setup(x => x.GetAllDeviceCapability(Helper.UserKey,1,10)).Returns(Task.Factory.StartNew(()=>new PagingRecord()));
            var result = (await adminController.GetAllDeviceCapability(Helper.UserKey,1,10)) as ObjectResult;
            Assert.IsTrue(((List<DeviceCapability>)result.Value).Count > 0);
        }
        [Test]
        public async Task Update_UserAdminPermission()
        {
            iAdmin.Setup(x => x.UpdateAdminPermission(It.IsAny<List<UserPermission>>(),Helper.UserKey)).Returns(Task.Factory.StartNew(()=>true));
            var result = (await adminController.UpdateAdminPermission(It.IsAny<List<UserPermission>>(),Helper.UserKey)) as ObjectResult;
            Assert.IsTrue((bool)result.Value);
        }
    }
}
