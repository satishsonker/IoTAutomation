using IoT.DataLayer.Interface;
using IoT.ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.BusinessLayer
{
    public class MasterDataBL
    {
        private readonly IMasterData _masterData;
        public MasterDataBL(IMasterData masterData)
        {
            _masterData = masterData;
        }
        public async Task<int> AddCapabilityInterface(CapabilityInterface capabilityInterface, string userKey)
        {
            if (capabilityInterface != null)
            {
                capabilityInterface.CreatedDate = DateTime.Now;
            }
            return await _masterData.AddCapabilityInterface(capabilityInterface, userKey);
        }

        public async Task<int> AddCapabilitySupportedProperty(CapabilitySupportedProperty capabilitySupportedProperty, string userKey)
        {
            if (capabilitySupportedProperty != null)
            {
                capabilitySupportedProperty.CreatedDate = DateTime.Now;
            }
            return await _masterData.AddCapabilitySupportedProperty(capabilitySupportedProperty, userKey);
        }

        public async Task<int> AddCapabilityType(CapabilityType capabilityType, string userKey)
        {
            if (capabilityType != null)
            {
                capabilityType.CreatedDate = DateTime.Now;
            }
            return await _masterData.AddCapabilityType(capabilityType, userKey);
        }

        public async Task<int> AddCapabilityVersion(CapabilityVersion capabilityVersion, string userKey)
        {
            if (capabilityVersion != null)
            {
                capabilityVersion.CreatedDate = DateTime.Now;
            }
            return await _masterData.AddCapabilityVersion(capabilityVersion, userKey);
        }

        public async Task<int> AddDisplayCategory(DisplayCategory displayCategory, string userKey)
        {
            List<DisplayCategory> list = new List<DisplayCategory>();
            if (displayCategory != null)
            {
                displayCategory.CreatedDate = DateTime.Now;
                var splitedValue = displayCategory.DisplayCategoryValue.Split(",").ToList();
                foreach (string displayCat in splitedValue)
                {
                    var item = (DisplayCategory)displayCategory.Clone();
                    string val = displayCat.Trim().ToUpper();
                    item.DisplayCategoryLabel = val.ToLower().Capatalize();
                    item.DisplayCategoryValue = val;
                    list.Add(item);
                }
            }

            return await _masterData.AddDisplayCategory(list, userKey);
        }

        public async Task<int> DeleteCapabilityInterface(int capabilityInterfaceId, string userKey)
        {
            return await _masterData.DeleteCapabilityInterface(capabilityInterfaceId, userKey);
        }

        public async Task<int> DeleteCapabilitySupportedProperty(int capabilitySupportedPropertyId, string userKey)
        {
            return await _masterData.DeleteCapabilitySupportedProperty(capabilitySupportedPropertyId, userKey);
        }

        public async Task<int> DeleteCapabilityType(int capabilityTypeId, string userKey)
        {
            return await _masterData.DeleteCapabilityType(capabilityTypeId, userKey);
        }

        public async Task<int> DeleteCapabilityVersion(int capabilityVersionId, string userKey)
        {
            return await _masterData.DeleteCapabilityVersion(capabilityVersionId, userKey);
        }

        public async Task<int> DeleteDisplayCategory(int displayCategoryId, string userKey)
        {
            return await _masterData.DeleteDisplayCategory(displayCategoryId, userKey);
        }

        public async Task<AllCapabilityMasterModel> GetAllCapabilityDropdownData(string userKey, string searchTerm)
        {
            return await _masterData.GetAllCapabilityDropdownData(userKey, searchTerm);
        }

        public async Task<dynamic> GetCapabilityInterface(string userKey, int id, int pageNo, int pageSize)
        {
            return await _masterData.GetCapabilityInterface(userKey, id, pageNo, pageSize);
        }

        public async Task<List<DropdownDataModel>> GetCapabilityInterfaceDropdownData(string userKey, int id)
        {
            return await _masterData.GetCapabilityInterfaceDropdownData(userKey, id);
        }

        public async Task<List<CapabilitySupportedProperty>> GetCapabilitySupportedProperty(string userKey, int id)
        {
            return await _masterData.GetCapabilitySupportedProperty(userKey, id);
        }

        public async Task<List<DropdownDataModel>> GetCapabilitySupportedPropertyDropdownData(string userKey, int id)
        {
            return await _masterData.GetCapabilitySupportedPropertyDropdownData(userKey, id);
        }

        public async Task<PagingRecord> GetCapabilityType(string userKey, int id, int pageNo, int pageSize)
        {
            pageNo = pageNo < 1 ? 1 : pageNo;
            pageSize = pageSize < 1 ? 10 : pageSize;
            return await _masterData.GetCapabilityType(userKey, id, pageNo, pageSize);
        }

        public async Task<List<DropdownDataModel>> GetCapabilityTypeDropdownData(string userKey, int id)
        {
            return await _masterData.GetCapabilityTypeDropdownData(userKey, id);
        }

        public async Task<List<CapabilityVersion>> GetCapabilityVersion(string userKey, int id)
        {
            return await _masterData.GetCapabilityVersion(userKey, id);
        }

        public async Task<List<DropdownDataModel>> GetCapabilityVersionDropdownData(string userKey, int id)
        {
            return await _masterData.GetCapabilityVersionDropdownData(userKey, id);
        }

        public async Task<PagingRecord> GetDisplayCategory(string userKey, int id,int pageNo,int PageSize)
        {
            return await _masterData.GetDisplayCategory(userKey, id,pageNo,PageSize);
        }

        public async Task<List<DropdownDataModel>> GetDisplayCategoryDropdownData(string userKey, int id)
        {
            return await _masterData.GetDisplayCategoryDropdownData(userKey, id);
        }

        public async Task<List<CapabilityInterface>> SearchCapabilityInterface(string userKey, string searchTerm)
        {
            return await _masterData.SearchCapabilityInterface(userKey, searchTerm);
        }

        public async Task<List<CapabilitySupportedProperty>> SearchCapabilitySupportedProperty(string userKey, string searchTerm)
        {
            return await _masterData.SearchCapabilitySupportedProperty(userKey, searchTerm);
        }

        public async Task<List<CapabilityType>> SearchCapabilityType(string userKey, string searchTerm)
        {
            return await _masterData.SearchCapabilityType(userKey, searchTerm);
        }

        public async Task<List<CapabilityVersion>> SearchCapabilityVersion(string userKey, string searchTerm)
        {
            return await _masterData.SearchCapabilityVersion(userKey, searchTerm);
        }

        public async Task<List<DisplayCategory>> SearchDisplayCategory(string userKey, string searchTerm)
        {
            return await _masterData.SearchDisplayCategory(userKey, searchTerm);
        }

        public async Task<int> UpdateCapabilityInterface(CapabilityInterface capabilityInterface, string userKey)
        {
            if (capabilityInterface != null)
                capabilityInterface.ModifiedDate = DateTime.Now;
            return await _masterData.UpdateCapabilityInterface(capabilityInterface, userKey);
        }

        public async Task<int> UpdateCapabilitySupportedProperty(CapabilitySupportedProperty capabilitySupportedProperty, string userKey)
        {
            if (capabilitySupportedProperty != null)
                capabilitySupportedProperty.ModifiedDate = DateTime.Now;
            return await _masterData.UpdateCapabilitySupportedProperty(capabilitySupportedProperty, userKey);
        }

        public async Task<int> UpdateCapabilityType(CapabilityType capabilityType, string userKey)
        {
            if (capabilityType != null)
                capabilityType.ModifiedDate = DateTime.Now;
            return await _masterData.UpdateCapabilityType(capabilityType, userKey);

        }

        public async Task<int> UpdateCapabilityVersion(CapabilityVersion capabilityVersion, string userKey)
        {
            if (capabilityVersion != null)
                capabilityVersion.ModifiedDate = DateTime.Now;
            return await _masterData.UpdateCapabilityVersion(capabilityVersion, userKey);
        }

        public async Task<int> UpdateDisplayCategory(DisplayCategory displayCategory, string userKey)
        {
            if (displayCategory != null)
                displayCategory.ModifiedDate = DateTime.Now;
            return await _masterData.UpdateDisplayCategory(displayCategory, userKey);
        }
    }
}
