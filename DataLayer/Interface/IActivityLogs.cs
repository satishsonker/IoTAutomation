using IoT.ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IoT.DataLayer.Interface
{
    public interface IActivityLogs
    {
        Task<int> Add(ActivityLog entity, string userKey);
        Task<List<ActivityLog>> GetAll(string userKey);
    }
}
