using IoT.ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoT.DataLayer.Interface
{
  public  interface IDashboard
    {
        DashboardModel GetDashboardData(string userKey);
    }
}
