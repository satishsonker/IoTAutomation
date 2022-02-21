using IoT.DataLayer.Interface;
using IoT.ModelLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.DataLayer.Repository
{
   public class DropdownMasterRepository: IDropdownMaster
    {
        private readonly AppDbContext context;
        public DropdownMasterRepository(AppDbContext _context)
        {
            this.context = _context;
        }

        public async Task<int> AddData(DropdownMaster dropdownMaster, string userKey)
        {
            if (await IsUserAdmin(userKey))
            {
                context.DropdownMasters.Add(dropdownMaster);
                return await context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<int> DeleteData(int dropdowndataId, string userKey)
        {
            if (await IsUserAdmin(userKey))
            {
                var deleteData = await context.DropdownMasters.Where(x => x.DropdownDataId == dropdowndataId).FirstOrDefaultAsync();
                if (deleteData != null)
                {
                    var room = context.DropdownMasters.Attach(deleteData);
                    room.State = EntityState.Deleted;
                    await context.SaveChangesAsync();
                }
            }
            return 0;
        }

        public async Task<PagingRecord> GetAllData(int pageNo, int pageSize, string userKey)
        {
                var result = new PagingRecord();
                var data = await context.DropdownMasters
                   .OrderBy(x => x.DataType)
                    .ToListAsync();
                result.Data = data.Skip((pageNo - 1) * pageSize).Take(pageSize).AsEnumerable().Cast<object>().ToList(); ;
                result.PageNo = pageNo;
                result.PageSize = pageSize;
                result.TotalRecord = data.Count;
                return result;
        }

        public async Task<DropdownMaster> GetData(int dropdowndataId, string userKey)
        {
            return await context.DropdownMasters.Where(x => x.DropdownDataId == dropdowndataId).FirstOrDefaultAsync();
        }

        public async Task<PagingRecord> SearchData(string searchTeam, int pageNo, int pageSize, string userKey)
        {
            if (await IsUserAdmin(userKey))
            {
                searchTeam = searchTeam.ToLower().Trim();
                var result = new PagingRecord();
                var data = await context.DropdownMasters
                    .Where(x=> x.DataValue.ToLower().Contains(searchTeam) || x.DataType.ToLower().Contains(searchTeam) || x.DataText.ToLower().Contains(searchTeam))
                    .OrderBy(x => x.DataType)
                    .ToListAsync();
                result.Data = data.Skip((pageNo - 1) * pageSize).Take(pageSize).AsEnumerable().Cast<object>().ToList(); ;
                result.PageNo = pageNo;
                result.PageSize = pageSize;
                result.TotalRecord = data.Count;
                return result;
            }
            return new PagingRecord();
        }

        public async Task<int> UpdateData(DropdownMaster dropdownMaster, string userKey)
        {
           if(await IsUserAdmin(userKey))
            {
                var data = context.DropdownMasters.Attach(dropdownMaster);
                data.State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            return 0;
        }

        private async Task<bool> IsUserAdmin(string userKey)
        {
          return await context.UserPermissions.Where(x => x.UserKey == userKey && x.IsAdmin).CountAsync() > 0;
        }
    }
}
