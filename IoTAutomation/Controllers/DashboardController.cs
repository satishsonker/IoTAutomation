using IoT.BusinessLayer;
using IoT.DataLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace IoT.WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private DashboardBL _dashboardBL;
        public DashboardController(IDashboard dashboard)
        {
            _dashboardBL = new DashboardBL(dashboard);
        }
        [HttpGet]
        [Route("GetDashboardData")]
        public IActionResult GetDashboardData([FromHeader] string userKey)
        {
            try
            {

                if (string.IsNullOrEmpty(userKey))
                    return Unauthorized(new object());
                return Ok(_dashboardBL.GetDashboardData(userKey));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}