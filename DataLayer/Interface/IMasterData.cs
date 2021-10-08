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
        IEnumerable<DropdownDataModel> GetCapabilityTypeDropdownData(string userKey,int id=0);

        Task<int> AddCapabilityVersion(CapabilityVersion capabilityVersion, string userKey);
        Task<int> UpdateCapabilityVersion(CapabilityVersion capabilityVersion, string userKey);
        Task<int> DeleteCapabilityVersion(int capabilityVersionId, string userKey);
        IEnumerable<DropdownDataModel> GetCapabilityVersionDropdownData( string userKey, int id = 0);

        Task<int> AddDisplayCategory(DisplayCategory displayCategory, string userKey);
        Task<int> UpdateDisplayCategory(DisplayCategory displayCategory, string userKey);
        Task<int> DeleteDisplayCategory(int displayCategoryId, string userKey);
        IEnumerable<DropdownDataModel> GetDisplayCategoryDropdownData(string userKey, int id = 0);

        Task<int> AddCapabilityInterface(CapabilityInterface capabilityInterface, string userKey);
        Task<int> UpdateCapabilityInterface(CapabilityInterface capabilityInterface, string userKey);
        Task<int> DeleteCapabilityInterface(int capabilityInterfaceId, string userKey);
        IEnumerable<DropdownDataModel> GetCapabilityInterfaceDropdownData(string userKey, int id = 0);

        Task<int> AddCapabilitySupportedProperty(CapabilitySupportedProperty capabilitySupportedProperty, string userKey);
        Task<int> UpdateCapabilitySupportedProperty(CapabilitySupportedProperty capabilitySupportedProperty, string userKey);
        Task<int> DeleteCapabilitySupportedProperty(int capabilitySupportedPropertyId, string userKey);
        IEnumerable<DropdownDataModel> GetCapabilitySupportedPropertyDropdownData(string userKey, int id = 0);
        AllCapabilityMasterModel GetAllCapabilityDropdownData(string userKey,string searchTerm="All");
        IEnumerable<CapabilitySupportedProperty> GetCapabilitySupportedProperty(string userKey, int id);
        IEnumerable<CapabilityInterface> GetCapabilityInterface(string userKey, int id);
        IEnumerable<DisplayCategory> GetDisplayCategory(string userKey, int id);
        IEnumerable<CapabilityVersion> GetCapabilityVersion(string userKey, int id);
        IEnumerable<CapabilityType> GetCapabilityType(string userKey, int id);
        IEnumerable<CapabilitySupportedProperty> SearchCapabilitySupportedProperty(string userKey, string searchTerm);
        IEnumerable<CapabilityInterface> SearchCapabilityInterface(string userKey, string searchTerm);
        IEnumerable<DisplayCategory> SearchDisplayCategory(string userKey, string searchTerm);
        IEnumerable<CapabilityVersion> SearchCapabilityVersion(string userKey, string searchTerm);
        IEnumerable<CapabilityType> SearchCapabilityType(string userKey, string searchTerm);
    }
}
