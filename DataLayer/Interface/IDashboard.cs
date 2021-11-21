using IoT.ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IoT.DataLayer.Interface
{
  public  interface IDashboard
    {
        Task<DashboardModel> GetDashboardData(string userKey);
    }
}
