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
        Task<PagingRecord> GetAll(string userKey,int pageNo,int pageSize);
        Task<PagingRecord> Search(string userKey,string searchTerm, int pageNo, int pageSize);
    }
}
