using IoT.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IoT.DataLayer.Interface
{
    public interface IActivityLogs
    {
        Task<ActivityLog> Add(ActivityLog entity, string userKey);
        IEnumerable<ActivityLog> GetAll(string userKey);
    }
}
