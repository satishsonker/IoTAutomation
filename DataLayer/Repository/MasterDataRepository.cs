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

        public Task<int> AddCapabilityInterface(CapabilityInterface capabilityInterface, string userKey)
        {
            if (!isUserExist(userKey))
                return Task.Factory.StartNew(()=>0);
            else
            {
                context.CapabilityInterfaces.Add(capabilityInterface);
                return context.SaveChangesAsync();
            }
        }

        public Task<int> AddCapabilitySupportedProperty(CapabilitySupportedProperty capabilitySupportedProperty, string userKey)
        {
            if (!isUserExist(userKey))
                return Task.Factory.StartNew(() => 0);
            else
            {
                context.CapabilitySupportedProperties.Add(capabilitySupportedProperty);
                return context.SaveChangesAsync();
            }
        }

        public Task<int> AddCapabilityType(CapabilityType capabilityType, string userKey)
        {
            if (!isUserExist(userKey))
                return Task.Factory.StartNew(() => 0);
            else
            {
                context.CapabilityTypes.Add(capabilityType);
                return context.SaveChangesAsync();
            }
        }

        public Task<int> AddCapabilityVersion(CapabilityVersion capabilityVersion, string userKey)
        {
            if (!isUserExist(userKey))
                return Task.Factory.StartNew(() => 0);
            else
            {
                context.CapabilityVersions.Add(capabilityVersion);
                return context.SaveChangesAsync();
            }
        }

        public Task<int> AddDisplayCategory(DisplayCategory displayCategory, string userKey)
        {
            if (!isUserExist(userKey))
                return Task.Factory.StartNew(() => 0);
            else
            {
                context.DisplayCategorys.Add(displayCategory);
                return context.SaveChangesAsync();
            }
        }

        public Task<int> DeleteCapabilityInterface(int capabilityInterfaceId, string userKey)
        {
            if (!isUserExist(userKey))
                 return Task.Factory.StartNew(()=>0);
            else
            {
                var oldData = context.CapabilityInterfaces.Where(x => x.CapabilityInterfaceId == capabilityInterfaceId).FirstOrDefault();
                if(oldData!=null)
                {
                    var entity = context.Entry(oldData);
                    entity.State = EntityState.Deleted;
                    return context.SaveChangesAsync();
                }
                return Task.Factory.StartNew(() => 0);
            }
        }

        public Task<int> DeleteCapabilitySupportedProperty(int capabilitySupportedPropertyId, string userKey)
        {
            if (!isUserExist(userKey))
                return Task.Factory.StartNew(() => 0);
            else
            {
                var oldData = context.CapabilitySupportedProperties.Where(x => x.CapabilitySupportedPropertyId == capabilitySupportedPropertyId).FirstOrDefault();
                if (oldData != null)
                {
                    var entity = context.Entry(oldData);
                    entity.State = EntityState.Deleted;
                    return context.SaveChangesAsync();
                }
                return Task.Factory.StartNew(() => 0);
            }
        }

        public Task<int> DeleteCapabilityType(int capabilityTypeId, string userKey)
        {
            if (!isUserExist(userKey))
                 return Task.Factory.StartNew(()=>0);
            else
            {
                var oldData = context.CapabilityTypes.Where(x => x.CapabilityTypeId == capabilityTypeId).FirstOrDefault();
                if (oldData != null)
                {
                    var entity = context.Entry(oldData);
                    entity.State = EntityState.Deleted;
                    return context.SaveChangesAsync();
                }
                return Task.Factory.StartNew(() => 0);
            }
        }

        public Task<int> DeleteCapabilityVersion(int capabilityVersionId, string userKey)
        {
            if (!isUserExist(userKey))
                return Task.Factory.StartNew(() => 0);
            else
            {
                var oldData = context.CapabilityVersions.Where(x => x.CapabilityVersionId == capabilityVersionId).FirstOrDefault();
                if (oldData != null)
                {
                    var entity = context.Entry(oldData);
                    entity.State = EntityState.Deleted;
                    return context.SaveChangesAsync();
                }
                return Task.Factory.StartNew(() => 0);
            }
        }

        public Task<int> DeleteDisplayCategory(int displayCategoryId, string userKey)
        {
            if (!isUserExist(userKey))
                return Task.Factory.StartNew(() => 0);
            else
            {
                var oldData = context.DisplayCategorys.Where(x => x.DisplayCategoryId == displayCategoryId).FirstOrDefault();
                if (oldData != null)
                {
                    var entity = context.Entry(oldData);
                    entity.State = EntityState.Deleted;
                    return context.SaveChangesAsync();
                }
                return Task.Factory.StartNew(() => 0);
            }
        }

        public AllCapabilityMasterModel GetAllCapabilityDropdownData(string userKey, string searchTerm = "All")
        {
            searchTerm =string.IsNullOrEmpty(searchTerm)?"all": searchTerm.ToLower();
            AllCapabilityMasterModel allCapabilityMasterModel = new AllCapabilityMasterModel();
            if (!isUserExist(userKey))
                return allCapabilityMasterModel;
            else
            {
                allCapabilityMasterModel.CapabilityInterfaces = context.
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
                                                                .ToList();
                allCapabilityMasterModel.CapabilitySupportedProperties = context.
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
                                                                .ToList();
                allCapabilityMasterModel.CapabilityTypes = context.
                                                                CapabilityTypes
                                                                .Where(x => searchTerm == "all" || x.CapabilityTypeName.ToLower().Contains(searchTerm))
                                                                .Select(x =>
                                                                            new DropdownDataModel()
                                                                            {
                                                                                Key = x.CapabilityTypeId.ToString(),
                                                                                Value = x.CapabilityTypeName
                                                                            })
                                                                .OrderBy(x=>x.Value)
                                                                .ToList();
                allCapabilityMasterModel.CapabilityVersions = context.
                                                                CapabilityVersions
                                                                .Where(x => searchTerm == "all" || x.CapabilityVersionName.ToLower().Contains(searchTerm))
                                                                .Select(x =>
                                                                            new DropdownDataModel()
                                                                            {
                                                                                Key = x.CapabilityVersionId.ToString(),
                                                                                Value = x.CapabilityVersionName
                                                                            })
                                                                .OrderBy(x=>x.Value)
                                                                .ToList();
                allCapabilityMasterModel.DisplayCategories = context.
                                                                DisplayCategorys
                                                                .Where(x => searchTerm == "all" || x.DisplayCategoryLabel.ToLower().Contains(searchTerm))
                                                                .Select(x =>
                                                                            new DropdownDataModel()
                                                                            {
                                                                                Key = x.DisplayCategoryValue,
                                                                                Value = x.DisplayCategoryLabel
                                                                            })
                                                                .OrderBy(x=>x.Value)
                                                                .ToList();
                return allCapabilityMasterModel;
            }
        }

        public dynamic GetCapabilityInterface(string userKey, int id, int pageNo, int pageSize)
        {
            var result = new List<CapabilityInterface>();
            if (!isUserExist(userKey))
                return result;
            else
            {
                int skipRecords = (pageNo - 1) * pageSize;
                result = context.CapabilityInterfaces
                    .Where(x => id == 0 || x.CapabilityInterfaceId == id)
                    .OrderBy(x => x.CapabilityInterfaceName)
                    .ToList();
                return new { Data = result.Skip(skipRecords).Take(pageSize), TotalRecords = result.Count };
            }
        }

        public IEnumerable<DropdownDataModel> GetCapabilityInterfaceDropdownData(string userKey, int id = 0)
        {
            var result= new List<DropdownDataModel>();
            if (!isUserExist(userKey))
                return result;
            else
            {
                result = context.CapabilityInterfaces
                    .Where(x => id == 0 || x.CapabilityInterfaceId == id)
                    .Select(x=>new DropdownDataModel() {Key=x.CapabilityInterfaceId.ToString(),Value=x.CapabilityInterfaceName })
                    .OrderBy(x => x.Value)
                    .ToList();
                return result;
            }
        }

        public IEnumerable<CapabilitySupportedProperty> GetCapabilitySupportedProperty(string userKey, int id)
        {
            var result = new List<CapabilitySupportedProperty>();
            if (!isUserExist(userKey))
                return result;
            else
            {
                result = context.CapabilitySupportedProperties
                    .Where(x => id == 0 || x.CapabilitySupportedPropertyId == id).OrderBy(x => x.CapabilitySupportedPropertyName)
                    .ToList();
                return result;
            }
        }

        public IEnumerable<DropdownDataModel> GetCapabilitySupportedPropertyDropdownData(string userKey, int id = 0)
        {
            var result = new List<DropdownDataModel>();
            if (!isUserExist(userKey))
                return result;
            else
            {
                result = context.CapabilitySupportedProperties
                    .Where(x => id == 0 || x.CapabilitySupportedPropertyId == id)
                    .Select(x => new DropdownDataModel() { Key = x.CapabilitySupportedPropertyId.ToString(), Value = x.CapabilitySupportedPropertyName })
                    .OrderBy(x => x.Value)
                    .ToList();
                return result;
            }
        }

        public IEnumerable<CapabilityType> GetCapabilityType(string userKey, int id, int pageNo, int pageSize)
        {
            var result = new List<CapabilityType>();
            if (!isUserExist(userKey))
                return result;
            else
            {
                int skipRecords = (pageNo - 1) * pageSize;
                result = context.CapabilityTypes
                    .Where(x => id == 0 || x.CapabilityTypeId == id).OrderBy(x => x.CapabilityTypeName).Skip(skipRecords).Take(pageSize)
                    .ToList();
                return result;
            }
        }

        public IEnumerable<DropdownDataModel> GetCapabilityTypeDropdownData(string userKey, int id = 0)
        {
            var result = new List<DropdownDataModel>();
            if (!isUserExist(userKey))
                return result;
            else
            {
                result = context.CapabilityTypes
                    .Where(x => id == 0 || x.CapabilityTypeId == id)
                    .Select(x => new DropdownDataModel() { Key = x.CapabilityTypeId.ToString(), Value = x.CapabilityTypeName })
                    .OrderBy(x => x.Value)
                    .ToList();
                return result;
            }
        }

        public IEnumerable<CapabilityVersion> GetCapabilityVersion(string userKey, int id)
        {
            var result = new List<CapabilityVersion>();
            if (!isUserExist(userKey))
                return result;
            else
            {
                result = context.CapabilityVersions
                    .Where(x => id == 0 || x.CapabilityVersionId == id).OrderBy(x => x.CapabilityVersionName)
                    .ToList();
                return result;
            }
        }

        public IEnumerable<DropdownDataModel> GetCapabilityVersionDropdownData(string userKey, int id = 0)
        {var result = new List<DropdownDataModel>();
            if (!isUserExist(userKey))
                return result;
            else
            {
                result = context.CapabilityInterfaces
                    .Where(x => id == 0 || x.CapabilityInterfaceId == id)
                    .Select(x => new DropdownDataModel() { Key = x.CapabilityInterfaceId.ToString(), Value = x.CapabilityInterfaceName })
                    .OrderBy(x => x.Value)
                    .ToList();
                return result;
            }
        }

        public IEnumerable<DisplayCategory> GetDisplayCategory(string userKey, int id)
        {
            var result = new List<DisplayCategory>();
            if (!isUserExist(userKey))
                return result;
            else
            {
                result = context.DisplayCategorys
                    .Where(x => id == 0 || x.DisplayCategoryId == id).OrderBy(x => x.DisplayCategoryLabel)
                    .ToList();
                return result;
            }
        }

        public IEnumerable<DropdownDataModel> GetDisplayCategoryDropdownData(string userKey, int id = 0)
        {
            var result = new List<DropdownDataModel>();
            if (!isUserExist(userKey))
                return result;
            else
            {
                result = context.DisplayCategorys
                    .Where(x=>id==0 ||x.DisplayCategoryId==id)
                    .Select(x => new DropdownDataModel() { Key = x.DisplayCategoryValue, Value = x.DisplayCategoryLabel })
                    .OrderBy(x => x.Value)
                    .ToList();
                return result;
            }
        }

        public IEnumerable<CapabilityInterface> SearchCapabilityInterface(string userKey, string searchTerm)
        {
            searchTerm = string.IsNullOrEmpty(searchTerm) ? "all" : searchTerm.ToLower();
            return context.CapabilityInterfaces
                .Where(x => searchTerm == "all" || x.CapabilityInterfaceName.ToLower().Contains(searchTerm))
                .ToList()
                .OrderBy(x=>x.CapabilityInterfaceName);
        }

        public IEnumerable<CapabilitySupportedProperty> SearchCapabilitySupportedProperty(string userKey, string searchTerm)
        {
            searchTerm = string.IsNullOrEmpty(searchTerm) ? "all" : searchTerm.ToLower();
            return context.CapabilitySupportedProperties
                .Where(x => searchTerm == "all" || x.CapabilitySupportedPropertyName.ToLower().Contains(searchTerm))
                .ToList()
                .OrderBy(x => x.CapabilitySupportedPropertyName);
        }

        public IEnumerable<CapabilityType> SearchCapabilityType(string userKey, string searchTerm)
        {
            searchTerm = string.IsNullOrEmpty(searchTerm) ? "all" : searchTerm.ToLower();
            return context.CapabilityTypes
                .Where(x => searchTerm == "all" || x.CapabilityTypeName.ToLower().Contains(searchTerm))
                .ToList()
                .OrderBy(x => x.CapabilityTypeName);
        }

        public IEnumerable<CapabilityVersion> SearchCapabilityVersion(string userKey, string searchTerm)
        {
            searchTerm = string.IsNullOrEmpty(searchTerm) ? "all" : searchTerm.ToLower();
            return context.CapabilityVersions
                .Where(x => searchTerm == "all" || x.CapabilityVersionName.ToLower().Contains(searchTerm))
                .ToList()
                .OrderBy(x => x.CapabilityVersionName);
        }

        public IEnumerable<DisplayCategory> SearchDisplayCategory(string userKey, string searchTerm)
        {
            searchTerm = string.IsNullOrEmpty(searchTerm) ? "all" : searchTerm.ToLower();
            return context.DisplayCategorys
                .Where(x => searchTerm == "all" || x.DisplayCategoryValue.ToLower().Contains(searchTerm) || x.DisplayCategoryLabel.ToLower().Contains(searchTerm))
                .ToList()
                .OrderBy(x => x.DisplayCategoryLabel);
        }

        public Task<int> UpdateCapabilityInterface(CapabilityInterface capabilityInterface, string userKey)
        { 
            if (!isUserExist(userKey))
                 return Task.Factory.StartNew(()=>0);
            else
            {
                var entity = context.Attach(capabilityInterface);
                entity.State = EntityState.Modified;
                return context.SaveChangesAsync();
            }
        }

        public Task<int> UpdateCapabilitySupportedProperty(CapabilitySupportedProperty capabilitySupportedProperty, string userKey)
        {
            if (!isUserExist(userKey))
                return Task.Factory.StartNew(() => 0);
            else
            {
                var entity = context.Attach(capabilitySupportedProperty);
                entity.State = EntityState.Modified;
                return context.SaveChangesAsync();
            }
        }

        public Task<int> UpdateCapabilityType(CapabilityType capabilityType, string userKey)
        {
            if (!isUserExist(userKey))
                return Task.Factory.StartNew(() => 0);
            else
            {
                var entity = context.Attach(capabilityType);
                entity.State = EntityState.Modified;
                return context.SaveChangesAsync();
            }
        }

        public Task<int> UpdateCapabilityVersion(CapabilityVersion capabilityVersion, string userKey)
        {
            if (!isUserExist(userKey))
                return Task.Factory.StartNew(() => 0);
            else
            {
                var entity = context.Attach(capabilityVersion);
                entity.State = EntityState.Modified;
                return context.SaveChangesAsync();
            }
        }

        public Task<int> UpdateDisplayCategory(DisplayCategory displayCategory, string userKey)
        {
            if (!isUserExist(userKey))
                return Task.Factory.StartNew(() => 0);
            else
            {
                var entity = context.Attach(displayCategory);
                entity.State = EntityState.Modified;
                return context.SaveChangesAsync();
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
