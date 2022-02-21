using IoT.DataLayer.Interface;
using IoT.ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IoT.BusinessLayer
{
    public class DropdownMasterBL
    {
        private readonly IDropdownMaster ddlMaster;
        public DropdownMasterBL(IDropdownMaster _dropdownMaster)
        {
            this.ddlMaster = _dropdownMaster;
        }
        public async Task<int> AddData(DropdownMaster dropdownMaster, string userKey)
        {
            if (dropdownMaster != null)
            {
                dropdownMaster.CreatedDate = DateTime.Now;
                dropdownMaster.UserKey = userKey;
                return await ddlMaster.AddData(dropdownMaster, userKey);
            }
            return 0;
        }

        public async Task<int> DeleteData(int dropdowndataId, string userKey)
        {
            return await ddlMaster.DeleteData(dropdowndataId, userKey);
        }

        public async Task<PagingRecord> GetAllData(int pageNo, int pageSize, string userKey)
        {
            return await ddlMaster.GetAllData(pageNo, pageSize, userKey);
        }

        public async Task<DropdownMaster> GetData(int dropdowndataId, string userKey)
        {
            return await ddlMaster.GetData(dropdowndataId, userKey);
        }

        public async Task<PagingRecord> SearchData(string searchTeam, int pageNo, int pageSize, string userKey)
        {
            return await ddlMaster.SearchData(searchTeam, pageNo, pageSize, userKey);
        }

        public async Task<int> UpdateData(DropdownMaster dropdownMaster, string userKey)
        {
            if (dropdownMaster != null)
            {
                dropdownMaster.ModifiedDate = DateTime.Now;
                return await ddlMaster.UpdateData(dropdownMaster, userKey);
            }
            return 0;
        }
    }
}
