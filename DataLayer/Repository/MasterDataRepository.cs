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
    public class MasterDataRepository : IMasterData
    {
        private readonly AppDbContext context;
        public MasterDataRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<int> AddCapabilityInterface(CapabilityInterface capabilityInterface, string userKey)
        {
            if (!isUserExist(userKey))
                return 0;
            else
            {
                context.CapabilityInterfaces.Add(capabilityInterface);
                return await context.SaveChangesAsync();
            }
        }

        public async Task<int> AddCapabilitySupportedProperty(CapabilitySupportedProperty capabilitySupportedProperty, string userKey)
        {
            if (!isUserExist(userKey))
                return 0;
            else
            {
                context.CapabilitySupportedProperties.Add(capabilitySupportedProperty);
                return await context.SaveChangesAsync();
            }
        }

        public async Task<int> AddCapabilityType(CapabilityType capabilityType, string userKey)
        {
            if (!isUserExist(userKey))
                return 0;
            else
            {
                context.CapabilityTypes.Add(capabilityType);
                return await context.SaveChangesAsync();
            }
        }

        public async Task<int> AddCapabilityVersion(CapabilityVersion capabilityVersion, string userKey)
        {
            if (!isUserExist(userKey))
                return 0;
            else
            {
                context.CapabilityVersions.Add(capabilityVersion);
                return await context.SaveChangesAsync();
            }
        }

        public async Task<int> AddDisplayCategory(List<DisplayCategory> displayCategory, string userKey)
        {
            if (!isUserExist(userKey))
                return 0;
            else
            {               
               await context.DisplayCategorys.AddRangeAsync(displayCategory);
                return await context.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteCapabilityInterface(int capabilityInterfaceId, string userKey)
        {
            if (!isUserExist(userKey))
                 return 0;
            else
            {
                var oldData = context.CapabilityInterfaces.Where(x => x.CapabilityInterfaceId == capabilityInterfaceId).FirstOrDefaultAsync();
                if(oldData!=null)
                {
                    var entity = context.Entry(oldData);
                    entity.State = EntityState.Deleted;
                    return await context.SaveChangesAsync();
                }
                return 0;
            }
        }

        public async Task<int> DeleteCapabilitySupportedProperty(int capabilitySupportedPropertyId, string userKey)
        {
            if (!isUserExist(userKey))
                return 0;
            else
            {
                var oldData =await context.CapabilitySupportedProperties
                    .Where(x => x.CapabilitySupportedPropertyId == capabilitySupportedPropertyId)
                    .FirstOrDefaultAsync();
                if (oldData != null)
                {
                    var entity = context.Entry(oldData);
                    entity.State = EntityState.Deleted;
                    return await context.SaveChangesAsync();
                }
                return 0;
            }
        }

        public async Task<int> DeleteCapabilityType(int capabilityTypeId, string userKey)
        {
            if (!isUserExist(userKey))
                 return 0;
            else
            {
                var oldData =await context.CapabilityTypes.Where(x => x.CapabilityTypeId == capabilityTypeId)
                    .FirstOrDefaultAsync();
                if (oldData != null)
                {
                    var entity = context.Entry(oldData);
                    entity.State = EntityState.Deleted;
                    return await context.SaveChangesAsync();
                }
                return 0;
            }
        }

        public async Task<int> DeleteCapabilityVersion(int capabilityVersionId, string userKey)
        {
            if (!isUserExist(userKey))
                return 0;
            else
            {
                var oldData =await context.CapabilityVersions.Where(x => x.CapabilityVersionId == capabilityVersionId).FirstOrDefaultAsync();
                if (oldData != null)
                {
                    var entity = context.Entry(oldData);
                    entity.State = EntityState.Deleted;
                    return await context.SaveChangesAsync();
                }
                return 0;
            }
        }

        public async Task<int> DeleteDisplayCategory(int displayCategoryId, string userKey)
        {
            if (!isUserExist(userKey))
                return 0;
            else
            {
                var oldData =await context.DisplayCategorys.Where(x => x.DisplayCategoryId == displayCategoryId).FirstOrDefaultAsync();
                if (oldData != null)
                {
                    var entity = context.Entry(oldData);
                    entity.State = EntityState.Deleted;
                    return await context.SaveChangesAsync();
                }
                return 0;
            }
        }

        public async Task<AllCapabilityMasterModel> GetAllCapabilityDropdownData(string userKey, string searchTerm = "All")
        {
            searchTerm =string.IsNullOrEmpty(searchTerm)?"all": searchTerm.ToLower();
            AllCapabilityMasterModel allCapabilityMasterModel = new AllCapabilityMasterModel();
            if (!isUserExist(userKey))
                return allCapabilityMasterModel;
            else
            {
                allCapabilityMasterModel.CapabilityInterfaces =await context.
                                                                CapabilityInterfaces
                                                                .Where(x => searchTerm == "all" || x.CapabilityInterfaceName.ToLower().Contains(searchTerm))
                                                                .Select(x =>
                                                                            new DropdownDataModel()
                                                                            {
                                                                                Key = x.CapabilityInterfaceId.ToString()
                                                                                                        ,
                                                                                Value = x.CapabilityInterfaceName
                                                                            })
                                                                .OrderBy(x=>x.Value)
                                                                .ToListAsync();
                allCapabilityMasterModel.CapabilitySupportedProperties =await context.
                                                                CapabilitySupportedProperties
                                                                .Where(x => searchTerm == "all" || x.CapabilitySupportedPropertyName.ToLower().Contains(searchTerm))
                                                                .Select(x =>
                                                                            new DropdownDataModel()
                                                                            {
                                                                                Key = x.CapabilitySupportedPropertyId.ToString()
                                                                                                        ,
                                                                                Value = x.CapabilitySupportedPropertyName
                                                                            })
                                                                .OrderBy(x=>x.Value)
                                                                .ToListAsync();
                allCapabilityMasterModel.CapabilityTypes =await context.
                                                                CapabilityTypes
                                                                .Where(x => searchTerm == "all" || x.CapabilityTypeName.ToLower().Contains(searchTerm))
                                                                .Select(x =>
                                                                            new DropdownDataModel()
                                                                            {
                                                                                Key = x.CapabilityTypeId.ToString(),
                                                                                Value = x.CapabilityTypeName
                                                                            })
                                                                .OrderBy(x=>x.Value)
                                                                .ToListAsync();
                allCapabilityMasterModel.CapabilityVersions =await context.
                                                                CapabilityVersions
                                                                .Where(x => searchTerm == "all" || x.CapabilityVersionName.ToLower().Contains(searchTerm))
                                                                .Select(x =>
                                                                            new DropdownDataModel()
                                                                            {
                                                                                Key = x.CapabilityVersionId.ToString(),
                                                                                Value = x.CapabilityVersionName
                                                                            })
                                                                .OrderBy(x=>x.Value)
                                                                .ToListAsync();
                allCapabilityMasterModel.DisplayCategories =await context.
                                                                DisplayCategorys
                                                                .Where(x => searchTerm == "all" || x.DisplayCategoryLabel.ToLower().Contains(searchTerm))
                                                                .Select(x =>
                                                                            new DropdownDataModel()
                                                                            {
                                                                                Key = x.DisplayCategoryValue,
                                                                                Value = x.DisplayCategoryLabel
                                                                            })
                                                                .OrderBy(x=>x.Value)
                                                                .ToListAsync();
                return allCapabilityMasterModel;
            }
        }

        public async Task<PagingRecord> GetCapabilityInterface(string userKey, int id, int pageNo, int pageSize)
        {
            var result = new PagingRecord();
            if (!isUserExist(userKey))
                return result;
            else
            {
                int skipRecords = (pageNo - 1) * pageSize;
                var totalRecord = await context.CapabilityInterfaces
                    .Where(x => id == 0 || x.CapabilityInterfaceId == id)
                    .OrderBy(x => x.CapabilityInterfaceName)
                    .ToListAsync();
                result.PageNo = pageNo;
                result.PageSize = pageSize;
                result.TotalRecord = totalRecord.Count;
                result.Data = totalRecord.Skip(skipRecords).Take(pageSize).AsEnumerable().Cast<object>().ToList();
                return result;
            }
        }

        public async Task<List<DropdownDataModel>> GetCapabilityInterfaceDropdownData(string userKey, int id = 0)
        {
            var result= new List<DropdownDataModel>();
            if (!isUserExist(userKey))
                return result;
            else
            {
                result =await context.CapabilityInterfaces
                    .Where(x => id == 0 || x.CapabilityInterfaceId == id)
                    .Select(x=>new DropdownDataModel() {Key=x.CapabilityInterfaceId.ToString(),Value=x.CapabilityInterfaceName })
                    .OrderBy(x => x.Value)
                    .ToListAsync();
                return result;
            }
        }

        public async Task<PagingRecord> GetCapabilitySupportedProperty(string userKey, int id,int pageNo,int pageSize)
        {
            var result = new PagingRecord();
            if (!isUserExist(userKey))
                return result;
            else
            {
                int skipRecords = (pageNo - 1) * pageSize;
                var totalRecord =await context.CapabilitySupportedProperties
                    .Where(x => id == 0 || x.CapabilitySupportedPropertyId == id).OrderBy(x => x.CapabilitySupportedPropertyName)
                    .ToListAsync();
                result.PageNo = pageNo;
                result.PageSize = pageSize;
                result.TotalRecord = totalRecord.Count;
                result.Data = totalRecord.Skip(skipRecords).Take(pageSize).AsEnumerable().Cast<object>().ToList();
                return result;
            }
        }

        public async Task<List<DropdownDataModel>> GetCapabilitySupportedPropertyDropdownData(string userKey, int id = 0)
        {
            var result = new List<DropdownDataModel>();
            if (!isUserExist(userKey))
                return result;
            else
            {
                result =await context.CapabilitySupportedProperties
                    .Where(x => id == 0 || x.CapabilitySupportedPropertyId == id)
                    .Select(x => new DropdownDataModel() { Key = x.CapabilitySupportedPropertyId.ToString(), Value = x.CapabilitySupportedPropertyName })
                    .OrderBy(x => x.Value)
                    .ToListAsync();
                return result;
            }
        }

        public async Task<PagingRecord> GetCapabilityType(string userKey, int id, int pageNo, int pageSize)
        {
            PagingRecord result = new PagingRecord();
            if (!isUserExist(userKey))
                return result;
            else
            {
                int skipRecords = (pageNo - 1) * pageSize;
                var data =await context.CapabilityTypes
                    .Where(x => id == 0 || x.CapabilityTypeId == id).OrderBy(x => x.CapabilityTypeName)
                    .ToListAsync();
                result.Data = data.Skip(skipRecords).Take(pageSize).AsEnumerable().Cast<object>().ToList(); ;
                result.PageNo = pageNo;
                result.PageSize = pageSize;
                result.TotalRecord = data.Count;
                return result;
            }
        }

        public async Task<List<DropdownDataModel>> GetCapabilityTypeDropdownData(string userKey, int id = 0)
        {
            var result = new List<DropdownDataModel>();
            if (!isUserExist(userKey))
                return result;
            else
            {
                result =await context.CapabilityTypes
                    .Where(x => id == 0 || x.CapabilityTypeId == id)
                    .Select(x => new DropdownDataModel() { Key = x.CapabilityTypeId.ToString(), Value = x.CapabilityTypeName })
                    .OrderBy(x => x.Value)
                    .ToListAsync();
                return result;
            }
        }

        public async Task<PagingRecord> GetCapabilityVersion(string userKey, int id,int pageNo,int pageSize)
        {
            var result = new PagingRecord();
            if (!isUserExist(userKey))
                return result;
            else
            {
                var allRecords =await context.CapabilityVersions
                    .Where(x => id == 0 || x.CapabilityVersionId == id).OrderBy(x => x.CapabilityVersionName)
                    .ToListAsync();
                result.PageNo = pageNo;
                result.PageSize = pageSize;
                result.TotalRecord = allRecords.Count;
                result.Data = allRecords.Skip((pageNo - 1) * pageSize).Take(pageSize).AsEnumerable().Cast<object>().ToList();
                return result;
            }
        }

        public async Task<List<DropdownDataModel>> GetCapabilityVersionDropdownData(string userKey, int id = 0)
        {
            var result = new List<DropdownDataModel>();
            if (!isUserExist(userKey))
                return result;
            else
            {
                result =await context.CapabilityInterfaces
                    .Where(x => id == 0 || x.CapabilityInterfaceId == id)
                    .Select(x => new DropdownDataModel() { Key = x.CapabilityInterfaceId.ToString(), Value = x.CapabilityInterfaceName })
                    .OrderBy(x => x.Value)
                    .ToListAsync();
                return result;
            }
        }

        public async Task<PagingRecord> GetDisplayCategory(string userKey, int id, int pageNo, int pageSize)
        {
            var result = new PagingRecord();
            if (isUserExist(userKey))
            {
                var totalRecord = await context.DisplayCategorys
                    .Where(x => id == 0 || x.DisplayCategoryId == id).OrderBy(x => x.DisplayCategoryLabel)
                    .OrderBy(x => x.DisplayCategoryLabel)
                    .ToListAsync();
                result.PageNo = pageNo;
                result.PageSize = pageSize;
                result.TotalRecord = totalRecord.Count;
                result.Data = totalRecord.Skip((pageNo - 1) * pageSize).Take(pageSize).AsEnumerable().Cast<object>().ToList();
                return result;
            }
                return result;
        }

        public async Task<List<DropdownDataModel>> GetDisplayCategoryDropdownData(string userKey, int id = 0)
        {
            var result = new List<DropdownDataModel>();
            if (!isUserExist(userKey))
                return result;
            else
            {
                result =await context.DisplayCategorys
                    .Where(x=>id==0 ||x.DisplayCategoryId==id)
                    .Select(x => new DropdownDataModel() { Key = x.DisplayCategoryValue, Value = x.DisplayCategoryLabel })
                    .OrderBy(x => x.Value)
                    .ToListAsync();
                return result;
            }
        }

        public async Task<List<CapabilityInterface>> SearchCapabilityInterface(string userKey, string searchTerm)
        {
            searchTerm = string.IsNullOrEmpty(searchTerm) ? "all" : searchTerm.ToLower();
            return await context.CapabilityInterfaces
                .Where(x => searchTerm == "all" || x.CapabilityInterfaceName.ToLower().Contains(searchTerm))
                .OrderBy(x=>x.CapabilityInterfaceName)
                .ToListAsync();
        }

        public async Task<List<CapabilitySupportedProperty>> SearchCapabilitySupportedProperty(string userKey, string searchTerm)
        {
            searchTerm = string.IsNullOrEmpty(searchTerm) ? "all" : searchTerm.ToLower();
            return await context.CapabilitySupportedProperties
                .Where(x => searchTerm == "all" || x.CapabilitySupportedPropertyName.ToLower().Contains(searchTerm))
                .OrderBy(x => x.CapabilitySupportedPropertyName)
                .ToListAsync();                
        }

        public async Task<List<CapabilityType>> SearchCapabilityType(string userKey, string searchTerm)
        {
            searchTerm = string.IsNullOrEmpty(searchTerm) ? "all" : searchTerm.ToLower();
            return await context.CapabilityTypes
                .Where(x => searchTerm == "all" || x.CapabilityTypeName.ToLower().Contains(searchTerm))
                .OrderBy(x => x.CapabilityTypeName)
                .ToListAsync();
        }

        public async Task<List<CapabilityVersion>> SearchCapabilityVersion(string userKey, string searchTerm)
        {
            searchTerm = string.IsNullOrEmpty(searchTerm) ? "all" : searchTerm.ToLower();
            return await context.CapabilityVersions
                .Where(x => searchTerm == "all" || x.CapabilityVersionName.ToLower().Contains(searchTerm))
                .OrderBy(x => x.CapabilityVersionName)
                .ToListAsync();
        }

        public async Task<List<DisplayCategory>> SearchDisplayCategory(string userKey, string searchTerm)
        {
            searchTerm = string.IsNullOrEmpty(searchTerm) ? "all" : searchTerm.ToLower();
            return await context.DisplayCategorys
                .Where(x => searchTerm == "all" || x.DisplayCategoryValue.ToLower().Contains(searchTerm) || x.DisplayCategoryLabel.ToLower().Contains(searchTerm))
                .OrderBy(x => x.DisplayCategoryLabel).ToListAsync();
        }

        public async Task<int> UpdateCapabilityInterface(CapabilityInterface capabilityInterface, string userKey)
        { 
            if (!isUserExist(userKey))
                 return 0;
            else
            {
                var entity = context.Attach(capabilityInterface);
                entity.State = EntityState.Modified;
                return await context.SaveChangesAsync();
            }
        }

        public async Task<int> UpdateCapabilitySupportedProperty(CapabilitySupportedProperty capabilitySupportedProperty, string userKey)
        {
            if (!isUserExist(userKey))
                return 0;
            else
            {
                var entity = context.Attach(capabilitySupportedProperty);
                entity.State = EntityState.Modified;
                return await context.SaveChangesAsync();
            }
        }

        public async Task<int> UpdateCapabilityType(CapabilityType capabilityType, string userKey)
        {
            if (!isUserExist(userKey))
                return 0;
            else
            {
                var entity = context.Attach(capabilityType);
                entity.State = EntityState.Modified;
                return await context.SaveChangesAsync();
            }
        }

        public async Task<int> UpdateCapabilityVersion(CapabilityVersion capabilityVersion, string userKey)
        {
            if (!isUserExist(userKey))
                return 0;
            else
            {
                var entity = context.Attach(capabilityVersion);
                entity.State = EntityState.Modified;
                return await context.SaveChangesAsync();
            }
        }

        public async Task<int> UpdateDisplayCategory(DisplayCategory displayCategory, string userKey)
        {
            if (!isUserExist(userKey))
                return 0;
            else
            {
                var entity = context.Attach(displayCategory);
                entity.State = EntityState.Modified;
                return await context.SaveChangesAsync();
            }
        }

        private bool isUserExist(string userKey)
        {
            bool result = false;
            if(!string.IsNullOrEmpty(userKey))
            {
                if (context.UserPermissions.Where(x => x.UserKey == userKey && x.IsAdmin).Count() > 0)
                    result = true;
            }
            return result;
        }
    }
}
