using IoT.DataLayer.Interface;
using IoT.ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IoT.BusinessLayer
{
   public class DashboardBL
    {
        private readonly IDashboard _dashboard;
        public DashboardBL(IDashboard dashboard)
        {
            _dashboard = dashboard;
        }
        public async Task<DashboardModel> GetDashboardData(string userKey)
        {
            return await _dashboard.GetDashboardData(userKey);
        }
    }
}
