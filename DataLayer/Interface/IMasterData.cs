using IoT.ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IoT.DataLayer.Interface
{
    public interface IMasterData
    {
        Task<int> AddCapabilityType(CapabilityType capabilityType, string userKey);
        Task<int> UpdateCapabilityType(CapabilityType capabilityType, string userKey);
        Task<int> DeleteCapabilityType(int capabilityTypeId, string userKey);
        Task<List<DropdownDataModel>> GetCapabilityTypeDropdownData(string userKey,int id=0);

        Task<int> AddCapabilityVersion(CapabilityVersion capabilityVersion, string userKey);
        Task<int> UpdateCapabilityVersion(CapabilityVersion capabilityVersion, string userKey);
        Task<int> DeleteCapabilityVersion(int capabilityVersionId, string userKey);
        Task<List<DropdownDataModel>> GetCapabilityVersionDropdownData( string userKey, int id = 0);

        Task<int> AddDisplayCategory(List<DisplayCategory> displayCategory, string userKey);
        Task<int> UpdateDisplayCategory(DisplayCategory displayCategory, string userKey);
        Task<int> DeleteDisplayCategory(int displayCategoryId, string userKey);
        Task<List<DropdownDataModel>> GetDisplayCategoryDropdownData(string userKey, int id = 0);

        Task<int> AddCapabilityInterface(CapabilityInterface capabilityInterface, string userKey);
        Task<int> UpdateCapabilityInterface(CapabilityInterface capabilityInterface, string userKey);
        Task<int> DeleteCapabilityInterface(int capabilityInterfaceId, string userKey);
        Task<List<DropdownDataModel>> GetCapabilityInterfaceDropdownData(string userKey, int id = 0);

        Task<int> AddCapabilitySupportedProperty(CapabilitySupportedProperty capabilitySupportedProperty, string userKey);
        Task<int> UpdateCapabilitySupportedProperty(CapabilitySupportedProperty capabilitySupportedProperty, string userKey);
        Task<int> DeleteCapabilitySupportedProperty(int capabilitySupportedPropertyId, string userKey);
        Task<List<DropdownDataModel>> GetCapabilitySupportedPropertyDropdownData(string userKey, int id = 0);
        Task<AllCapabilityMasterModel> GetAllCapabilityDropdownData(string userKey,string searchTerm="All");
        Task<List<CapabilitySupportedProperty>> GetCapabilitySupportedProperty(string userKey, int id);
        Task<dynamic> GetCapabilityInterface(string userKey, int id, int pageNo, int pageSize);
        Task<PagingRecord> GetDisplayCategory(string userKey, int id, int pageNo, int pageSize);
        Task<List<CapabilityVersion>> GetCapabilityVersion(string userKey, int id);
        Task<PagingRecord> GetCapabilityType(string userKey, int id,int pageNo,int pageSize);
        Task<List<CapabilitySupportedProperty>> SearchCapabilitySupportedProperty(string userKey, string searchTerm);
        Task<List<CapabilityInterface>> SearchCapabilityInterface(string userKey, string searchTerm);
        Task<List<DisplayCategory>> SearchDisplayCategory(string userKey, string searchTerm);
        Task<List<CapabilityVersion>> SearchCapabilityVersion(string userKey, string searchTerm);
        Task<List<CapabilityType>> SearchCapabilityType(string userKey, string searchTerm);
    }
}
