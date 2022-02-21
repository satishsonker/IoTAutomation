using IoT.ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IoT.DataLayer.Interface
{
  public  interface IDropdownMaster
    {
        Task<int> AddData(DropdownMaster dropdownMaster,string userKey);
        Task<int> UpdateData(DropdownMaster dropdownMaster, string userKey);
        Task<int> DeleteData(int dropdowndataId, string userKey);
        Task<DropdownMaster> GetData(int dropdowndataId, string userKey);
        Task<PagingRecord> GetAllData(int pageNo, int pageSize, string userKey);
        Task<PagingRecord> SearchData(string searchTeam,int pageNo,int pageSize, string userKey); 
    }
}
