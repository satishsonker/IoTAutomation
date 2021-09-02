using System;
using IoT.DataLayer.Models;
using IoT.DataLayer.Interface;
using System.Collections.Generic;

namespace IoT.BusinessLayer
{
    public class ActivityLogsBL
    {
        private readonly IActivityLogs _áctivityLogs;
        public ActivityLogsBL(IActivityLogs áctivityLogs)
        {
            _áctivityLogs = áctivityLogs;
        }

        public ActivityLog Add(ActivityLog activityLog,string userKey)
        {
            if(activityLog != null)
            {
                activityLog.CreatedDate = DateTime.Now;
            }
            _áctivityLogs.Add(activityLog,userKey);
            return activityLog;
        }
       
        public IEnumerable<ActivityLog> GetAll(string userKey)
        {
            return _áctivityLogs.GetAll(userKey);
        }
    }
}
