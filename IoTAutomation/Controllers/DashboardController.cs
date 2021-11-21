using IoT.BusinessLayer;
using IoT.DataLayer.Interface;
using IoT.ModelLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IoT.WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private DashboardBL _dashboardBL;
        private readonly ILogger _logger;
        public DashboardController(IDashboard dashboard, ILogger<DashboardController> logger)
        {
            _dashboardBL = new DashboardBL(dashboard);
            _logger = logger;
        }
        [HttpGet]
        [Route("GetDashboardData")]
        public async Task<DashboardModel> GetDashboardData([FromHeader] string userKey)
        {
            try
            {
                if (string.IsNullOrEmpty(userKey))
                    return new DashboardModel();
                return await _dashboardBL.GetDashboardData(userKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while getting dashboard data");
                return new DashboardModel();
            }
        }
    }
}