using IoT.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoT.DataLayer.Interface
{
    public interface IActivityLogs
    {
        ActivityLog Add(ActivityLog entity, string userKey);
        IEnumerable<ActivityLog> GetAll(string userKey);
    }
}
