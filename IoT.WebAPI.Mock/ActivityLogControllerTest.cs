using NUnit.Framework;
using IoT.WebAPI.Controllers;
using IoT.DataLayer.Interface;
using Moq;
using Microsoft.Extensions.Logging;
using IoT.ModelLayer;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace IoT.WebAPI.Mock
{
    public class ActivityLogControllerTest
    {
        private ActivityLogController activityLogController;
        Mock<IActivityLogs> IActivityLogsMock;
        [SetUp]
        public void Setup()
        {
            IActivityLogsMock = new Mock<IActivityLogs>();
            var LoggerMock = new Mock<ILogger<ActivityLogController>>();
            activityLogController = new ActivityLogController(IActivityLogsMock.Object, LoggerMock.Object);
        }

        [Test]
        public async Task Add_Activity_Log()
        {
            IActivityLogsMock.Setup(x => x.Add(It.IsAny<ActivityLog>(), It.IsAny<string>())).Returns(Task.Factory.StartNew(()=>10 ));
           
          var result=await activityLogController.Add(new ActivityLog(), Helper.UserKey);

            Assert.IsTrue(result>0);
        }

        [Test]
        public async Task GetAll_Activity_Log()
        {
            IActivityLogsMock.Setup(x => x.GetAll(It.IsAny<string>(),1,10)).Returns(Task.Factory.StartNew(() =>new PagingRecord()));

            var result = await activityLogController.GetAll(Helper.UserKey);

            Assert.IsTrue(result.PageNo>0);
        }
    }
}