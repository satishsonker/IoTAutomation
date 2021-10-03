using IoT.BusinessLayer;
using IoT.DataLayer.Interface;
using IoT.DataLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoT.WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MasterDataController : ControllerBase
    {
        private MasterDataBL _masterDataBL;
        private readonly ILogger _logger;
        public MasterDataController(IMasterData masterData, ILogger<MasterDataController> logger)
        {
            _masterDataBL = new MasterDataBL(masterData);
            _logger = logger;
        }
        [HttpPost]

        [Route("AddCapabilityType")]
        public Task<int> AddCapabilityType([FromBody] CapabilityType capabilityType,  [FromHeader] string userKey)
        {
            return _masterDataBL.AddCapabilityType(capabilityType, userKey);
        }

        [HttpPost]
        [Route("UpdateCapabilityType")]
        public Task<int> UpdateCapabilityType([FromBody] CapabilityType capabilityType,  [FromHeader] string userKey)
        {
            return _masterDataBL.UpdateCapabilityType(capabilityType, userKey);
        }
        
        [HttpDelete]
        [Route("DeleteCapabilityType")]
        public Task<int> DeleteCapabilityType([FromQuery] int capabilityTypeId,  [FromHeader] string userKey)
        {
            return _masterDataBL.DeleteCapabilityType(capabilityTypeId, userKey);
        }
        
        [HttpGet]
        [Route("GetCapabilityTypeDropdownData")]
        public IEnumerable<DropdownDataModel> GetCapabilityTypeDropdownData( [FromHeader] string userKey,[FromQuery] int id)
        {
            return _masterDataBL.GetCapabilityTypeDropdownData(userKey,id);
        }
        [HttpGet]
        [Route("GetCapabilityType")]
        public IEnumerable<CapabilityType> GetCapabilityType([FromHeader] string userKey, [FromQuery] int id)
        {
            return _masterDataBL.GetCapabilityType(userKey, id);
        }

        [HttpPost]
        [Route("AddCapabilityVersion")]
        public Task<int> AddCapabilityVersion([FromBody] CapabilityVersion capabilityVersion,  [FromHeader] string userKey)
        {
            return _masterDataBL.AddCapabilityVersion(capabilityVersion, userKey);
        }
        
        [HttpPost]
        [Route("UpdateCapabilityVersion")]
        public Task<int> UpdateCapabilityVersion([FromBody] CapabilityVersion capabilityVersion,  [FromHeader] string userKey)
        {
            return _masterDataBL.UpdateCapabilityVersion(capabilityVersion, userKey);
        }
        
        [HttpDelete]
        [Route("DeleteCapabilityVersion")]
        public Task<int> DeleteCapabilityVersion([FromQuery] int capabilityVersionId,  [FromHeader] string userKey)
        {
            return _masterDataBL.DeleteCapabilityVersion(capabilityVersionId, userKey);
        }
        
        [HttpGet]
        [Route("GetCapabilityVersionDropdownData")]
        public IEnumerable<DropdownDataModel> GetCapabilityVersionDropdownData( [FromHeader] string userKey, [FromQuery] int id)
        {
            return _masterDataBL.GetCapabilityVersionDropdownData(userKey,id);
        }
        [HttpGet]
        [Route("GetCapabilityVersion")]
        public IEnumerable<CapabilityVersion> GetCapabilityVersion([FromHeader] string userKey, [FromQuery] int id)
        {
            return _masterDataBL.GetCapabilityVersion(userKey, id);
        }

        [HttpPost]
        [Route("AddDisplayCategory")]
        public Task<int> AddDisplayCategory([FromBody] DisplayCategory displayCategory,  [FromHeader] string userKey)
        {
            return _masterDataBL.AddDisplayCategory(displayCategory, userKey);
        }
        
        [HttpPost]
        [Route("UpdateDisplayCategory")]
        public Task<int> UpdateDisplayCategory([FromBody] DisplayCategory displayCategory,  [FromHeader] string userKey)
        {
            return _masterDataBL.UpdateDisplayCategory(displayCategory, userKey);
        }
        
        [HttpDelete]
        [Route("DeleteDisplayCategory")]
        public Task<int> DeleteDisplayCategory([FromQuery] int displayCategoryId,  [FromHeader] string userKey)
        {
            return _masterDataBL.DeleteDisplayCategory(displayCategoryId, userKey);
        }
        
        [HttpGet]
        [Route("GetDisplayCategoryDropdownData")]
        public IEnumerable<DropdownDataModel> GetDisplayCategoryDropdownData( [FromHeader] string userKey, [FromQuery] int id)
        {
            return _masterDataBL.GetDisplayCategoryDropdownData(userKey,id);
        }

        [HttpGet]
        [Route("GetDisplayCategory")]
        public IEnumerable<DisplayCategory> GetDisplayCategory([FromHeader] string userKey, [FromQuery] int id)
        {
            return _masterDataBL.GetDisplayCategory(userKey, id);
        }

        [HttpPost]
        [Route("AddCapabilityInterface")]
        public Task<int> AddCapabilityInterface([FromBody] CapabilityInterface capabilityInterface,  [FromHeader] string userKey)
        {
            return _masterDataBL.AddCapabilityInterface(capabilityInterface, userKey);
        }
        
        [HttpPost]
        [Route("UpdateCapabilityInterface")]
        public Task<int> UpdateCapabilityInterface([FromBody] CapabilityInterface capabilityInterface,  [FromHeader] string userKey)
        {
            return _masterDataBL.UpdateCapabilityInterface(capabilityInterface, userKey);
        }
        
        [HttpDelete]
        [Route("DeleteCapabilityInterface")]        
        public Task<int> DeleteCapabilityInterface([FromQuery] int capabilityInterfaceId,  [FromHeader] string userKey)
        {
            return _masterDataBL.DeleteCapabilityInterface(capabilityInterfaceId, userKey);
        }
        
        [HttpGet]
        [Route("GetCapabilityInterfaceDropdownData")]
        public IEnumerable<DropdownDataModel> GetCapabilityInterfaceDropdownData( [FromHeader] string userKey, [FromQuery] int id)
        {
            return _masterDataBL.GetCapabilityInterfaceDropdownData(userKey,id);
        }
        [HttpGet]
        [Route("GetCapabilityInterface")]
        public IEnumerable<CapabilityInterface> GetCapabilityInterface([FromHeader] string userKey, [FromQuery] int id)
        {
            return _masterDataBL.GetCapabilityInterface(userKey, id);
        }

        [HttpPost]
        [Route("AddCapabilitySupportedProperty")]
        public Task<int> AddCapabilitySupportedProperty([FromBody] CapabilitySupportedProperty capabilitySupportedProperty,  [FromHeader] string userKey)
        {
            return _masterDataBL.AddCapabilitySupportedProperty(capabilitySupportedProperty, userKey);
        }
        
        [HttpPost]
        [Route("UpdateCapabilitySupportedProperty")]
        public Task<int> UpdateCapabilitySupportedProperty([FromBody] CapabilitySupportedProperty capabilitySupportedProperty,  [FromHeader] string userKey)
        {
            return _masterDataBL.UpdateCapabilitySupportedProperty(capabilitySupportedProperty, userKey);
        }
        
        [HttpDelete]
        [Route("DeleteCapabilitySupportedProperty")]
        public Task<int> DeleteCapabilitySupportedProperty([FromQuery] int capabilitySupportedPropertyId,  [FromHeader] string userKey)
        {
            return _masterDataBL.DeleteCapabilitySupportedProperty(capabilitySupportedPropertyId, userKey);
        }
        
        [HttpGet]
        [Route("GetCapabilitySupportedPropertyDropdownData")]
        public IEnumerable<DropdownDataModel> GetCapabilitySupportedPropertyDropdownData( [FromHeader] string userKey, [FromQuery] int id)
        {
            return _masterDataBL.GetCapabilitySupportedPropertyDropdownData(userKey,id);
        }
        [HttpGet]
        [Route("GetCapabilitySupportedProperty")]
        public IEnumerable<CapabilitySupportedProperty> GetCapabilitySupportedProperty([FromHeader] string userKey, [FromQuery] int id)
        {
            return _masterDataBL.GetCapabilitySupportedProperty(userKey, id);
        }

        [HttpGet]
        [Route("GetAllCapabilityDropdownData")]
        public AllCapabilityMasterModel GetAllCapabilityDropdownData( [FromHeader] string userKey, [FromQuery] string searchTerm)
        {
            return _masterDataBL.GetAllCapabilityDropdownData(userKey,searchTerm);
        }

        [HttpGet]
        [Route("SearchCapabilitySupportedProperty")]
        public IEnumerable<CapabilitySupportedProperty> SearchCapabilitySupportedProperty([FromHeader] string userKey, [FromQuery] string searchTerm)
        {
            return _masterDataBL.SearchCapabilitySupportedProperty(userKey, searchTerm);
        }
        [HttpGet]
        [Route("SearchCapabilityInterface")]
        public IEnumerable<CapabilityInterface> SearchCapabilityInterface([FromHeader] string userKey, [FromQuery] string searchTerm)
        {
            return _masterDataBL.SearchCapabilityInterface(userKey, searchTerm);
        }
        [HttpGet]
        [Route("SearchDisplayCategory")]
        public IEnumerable<DisplayCategory> SearchDisplayCategory([FromHeader] string userKey, [FromQuery] string searchTerm)
        {
            return _masterDataBL.SearchDisplayCategory(userKey, searchTerm);
        }
        [HttpGet]
        [Route("SearchCapabilityVersion")]
        public IEnumerable<CapabilityVersion> SearchCapabilityVersion([FromHeader] string userKey, [FromQuery] string searchTerm)
        {
            return _masterDataBL.SearchCapabilityVersion(userKey, searchTerm);
        }
        [HttpGet]
        [Route("SearchCapabilityType")]
        public IEnumerable<CapabilityType> SearchCapabilityType([FromHeader] string userKey, [FromQuery] string searchTerm)
        {
          return  _masterDataBL.SearchCapabilityType(userKey, searchTerm);
        }
    }
}
