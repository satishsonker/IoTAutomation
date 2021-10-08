using IoT.DataLayer.Interface;
using IoT.ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoT.BusinessLayer
{
   public class DashboardBL
    {
        private readonly IDashboard _dashboard;
        public DashboardBL(IDashboard dashboard)
        {
            _dashboard = dashboard;
        }
        public DashboardModel GetDashboardData(string userKey)
        {
            return _dashboard.GetDashboardData(userKey);

        }
    }
}
