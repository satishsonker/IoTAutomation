using System;
using IoT.ModelLayer;
using IoT.DataLayer.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoT.BusinessLayer
{
    public class ActivityLogsBL
    {
        private readonly IActivityLogs _áctivityLogs;
        public ActivityLogsBL(IActivityLogs áctivityLogs)
        {
            _áctivityLogs = áctivityLogs;
        }

        public  Task<int> Add(ActivityLog activityLog,string userKey)
        {
            if(activityLog != null)
            {
                activityLog.CreatedDate = DateTime.Now;
                activityLog.UserKey = userKey;
            }
          return  _áctivityLogs.Add(activityLog,userKey);
        }
       
        public Task<List<ActivityLog>> GetAll(string userKey)
        {
            return _áctivityLogs.GetAll(userKey);
        }
    }
}
