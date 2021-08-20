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
        private string _userKey = "100965730392215373474";
        public DashboardController(IDashboard dashboard)
        {
            _dashboardBL = new DashboardBL(dashboard);
        }
        [HttpGet]
        [Route("GetDashboardData")]
        public IActionResult GetDashboardData()
        {
            try
            {

                //var headerValue = Request.Headers["UserKey"];
                //if (headerValue.Any() == true)
                //{
                //    _userKey = headerValue.ToString();
                //}
                return Ok(_dashboardBL.GetDashboardData(_userKey));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}