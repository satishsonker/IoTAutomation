using NUnit.Framework;
using IoT.WebAPI.Controllers;

namespace IoT.WebAPI.Mock
{
    public class ActivityLogControllerTest
    {
        private readonly ActivityLogController activityLogController;
        [SetUp]
        public void Setup()
        {
            //activityLogController=new ActivityLogController()
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}