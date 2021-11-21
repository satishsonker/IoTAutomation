using NUnit.Framework;
using IoT.BusinessLayer;
using IoT.DataLayer.Interface;
using IoT.ModelLayer;
using IoT.WebAPI.Controllers;
using Moq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace IoT.WebAPI.Mock
{
    public class DashboardControllerTest
    {
        private DashboardController dashboardController;
        private Mock<IDashboard> iDashboadMock;
        [SetUp]
        public void Setup()
        {
            iDashboadMock = new Mock<IDashboard>();
            var iLogger = new Mock<ILogger<DashboardController>>();
            dashboardController = new DashboardController(iDashboadMock.Object,iLogger.Object);
        }

        [Test]
        public async Task GetDashboardData()
        {
            iDashboadMock.Setup(_ => _.GetDashboardData(Helper.UserKey)).Returns(Task.FromResult(new DashboardModel()));
            var result=await dashboardController.GetDashboardData(Helper.UserKey);
            Assert.IsTrue(result != null);
        }
    }
}
