using IoT.DataLayer.Interface;
using IoT.ModelLayer;
using System;
using System.Collections.Generic;
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
        public Task<int> AddCapabilityInterface(CapabilityInterface capabilityInterface, string userKey)
        {
            if(capabilityInterface!=null)
            {
                capabilityInterface.CreatedDate = DateTime.Now;
            }
            return _masterData.AddCapabilityInterface(capabilityInterface, userKey);
        }

        public Task<int> AddCapabilitySupportedProperty(CapabilitySupportedProperty capabilitySupportedProperty, string userKey)
        {
            if (capabilitySupportedProperty != null)
            {
                capabilitySupportedProperty.CreatedDate = DateTime.Now;
            }
            return _masterData.AddCapabilitySupportedProperty(capabilitySupportedProperty, userKey);
        }

        public Task<int> AddCapabilityType(CapabilityType capabilityType, string userKey)
        {
            if (capabilityType != null)
            {
                capabilityType.CreatedDate = DateTime.Now;
            }
            return _masterData.AddCapabilityType(capabilityType, userKey);
        }

        public Task<int> AddCapabilityVersion(CapabilityVersion capabilityVersion, string userKey)
        {
            if (capabilityVersion != null)
            {
                capabilityVersion.CreatedDate = DateTime.Now;
            }
            return _masterData.AddCapabilityVersion(capabilityVersion, userKey);
        }

        public Task<int> AddDisplayCategory(DisplayCategory displayCategory, string userKey)
        {
            if (displayCategory != null)
            {
                displayCategory.CreatedDate = DateTime.Now;
            }
            return _masterData.AddDisplayCategory(displayCategory, userKey);
        }

        public Task<int> DeleteCapabilityInterface(int capabilityInterfaceId, string userKey)
        {
            return _masterData.DeleteCapabilityInterface(capabilityInterfaceId, userKey);
        }

        public Task<int> DeleteCapabilitySupportedProperty(int capabilitySupportedPropertyId, string userKey)
        {
            return _masterData.DeleteCapabilitySupportedProperty(capabilitySupportedPropertyId, userKey);
        }

        public Task<int> DeleteCapabilityType(int capabilityTypeId, string userKey)
        {
            return _masterData.DeleteCapabilityType(capabilityTypeId, userKey);
        }

        public Task<int> DeleteCapabilityVersion(int capabilityVersionId, string userKey)
        {
            return _masterData.DeleteCapabilityVersion(capabilityVersionId, userKey);
        }

        public Task<int> DeleteDisplayCategory(int displayCategoryId, string userKey)
        {
            return _masterData.DeleteDisplayCategory(displayCategoryId, userKey);
        }

        public AllCapabilityMasterModel GetAllCapabilityDropdownData(string userKey,string searchTerm)
        {
          return  _masterData.GetAllCapabilityDropdownData(userKey,searchTerm);
        }

        public dynamic GetCapabilityInterface(string userKey, int id, int pageNo, int pageSize)
        {
            return _masterData.GetCapabilityInterface(userKey, id,pageNo,pageSize);
        }

        public IEnumerable<DropdownDataModel> GetCapabilityInterfaceDropdownData(string userKey,int id)
        {
            return _masterData.GetCapabilityInterfaceDropdownData(userKey, id);
        }

        public IEnumerable<CapabilitySupportedProperty> GetCapabilitySupportedProperty(string userKey, int id)
        {
            return _masterData.GetCapabilitySupportedProperty(userKey, id);
        }

        public IEnumerable<DropdownDataModel> GetCapabilitySupportedPropertyDropdownData(string userKey, int id)
        {
            return _masterData.GetCapabilitySupportedPropertyDropdownData(userKey, id);
        }

        public IEnumerable<CapabilityType> GetCapabilityType(string userKey, int id,int pageNo,int pageSize)
        {
            return _masterData.GetCapabilityType(userKey, id,pageNo,pageSize);
        }

        public IEnumerable<DropdownDataModel> GetCapabilityTypeDropdownData(string userKey, int id)
        {
            return _masterData.GetCapabilityTypeDropdownData(userKey, id);
        }

        public IEnumerable<CapabilityVersion> GetCapabilityVersion(string userKey, int id)
        {
            return _masterData.GetCapabilityVersion(userKey, id);
        }

        public IEnumerable<DropdownDataModel> GetCapabilityVersionDropdownData(string userKey, int id)
        {
            return _masterData.GetCapabilityVersionDropdownData(userKey, id);
        }

        public IEnumerable<DisplayCategory> GetDisplayCategory(string userKey, int id)
        {
            return _masterData.GetDisplayCategory(userKey, id);
        }

        public IEnumerable<DropdownDataModel> GetDisplayCategoryDropdownData(string userKey, int id)
        {
            return _masterData.GetDisplayCategoryDropdownData(userKey,id);
        }

        public IEnumerable<CapabilityInterface> SearchCapabilityInterface(string userKey, string searchTerm)
        {
            return _masterData.SearchCapabilityInterface(userKey, searchTerm);
        }

        public IEnumerable<CapabilitySupportedProperty> SearchCapabilitySupportedProperty(string userKey, string searchTerm)
        {
            return _masterData.SearchCapabilitySupportedProperty(userKey, searchTerm);
        }

        public IEnumerable<CapabilityType> SearchCapabilityType(string userKey, string searchTerm)
        {
            return _masterData.SearchCapabilityType(userKey, searchTerm);
        }

        public IEnumerable<CapabilityVersion> SearchCapabilityVersion(string userKey, string searchTerm)
        {
            return _masterData.SearchCapabilityVersion(userKey, searchTerm);
        }

        public IEnumerable<DisplayCategory> SearchDisplayCategory(string userKey, string searchTerm)
        {
            return _masterData.SearchDisplayCategory(userKey, searchTerm);
        }

        public Task<int> UpdateCapabilityInterface(CapabilityInterface capabilityInterface, string userKey)
        {
            if (capabilityInterface != null)
                capabilityInterface.ModifiedDate = DateTime.Now;
            return _masterData.UpdateCapabilityInterface(capabilityInterface, userKey);
        }

        public Task<int> UpdateCapabilitySupportedProperty(CapabilitySupportedProperty capabilitySupportedProperty, string userKey)
        {
            if (capabilitySupportedProperty != null)
                capabilitySupportedProperty.ModifiedDate = DateTime.Now;
            return _masterData.UpdateCapabilitySupportedProperty(capabilitySupportedProperty, userKey);
        }

        public Task<int> UpdateCapabilityType(CapabilityType capabilityType, string userKey)
        {
            if (capabilityType != null)
                capabilityType.ModifiedDate = DateTime.Now;
            return _masterData.UpdateCapabilityType(capabilityType, userKey);

        }

        public Task<int> UpdateCapabilityVersion(CapabilityVersion capabilityVersion, string userKey)
        {
            if (capabilityVersion != null)
                capabilityVersion.ModifiedDate = DateTime.Now;
            return _masterData.UpdateCapabilityVersion(capabilityVersion, userKey);
        }

        public Task<int> UpdateDisplayCategory(DisplayCategory displayCategory, string userKey)
        {
            if (displayCategory != null)
                displayCategory.ModifiedDate = DateTime.Now;
            return _masterData.UpdateDisplayCategory(displayCategory, userKey);
        }
    }
}
