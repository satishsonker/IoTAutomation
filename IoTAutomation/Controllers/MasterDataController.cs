using IoT.BusinessLayer;
using IoT.DataLayer.Interface;
using IoT.ModelLayer;
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
        public async Task<int> AddCapabilityType([FromBody] CapabilityType capabilityType,  [FromHeader] string userKey)
        {
            return await _masterDataBL.AddCapabilityType(capabilityType, userKey);
        }

        [HttpPost]
        [Route("UpdateCapabilityType")]
        public async Task<int> UpdateCapabilityType([FromBody] CapabilityType capabilityType,  [FromHeader] string userKey)
        {
            return await _masterDataBL.UpdateCapabilityType(capabilityType, userKey);
        }
        
        [HttpDelete]
        [Route("DeleteCapabilityType")]
        public async Task<int> DeleteCapabilityType([FromQuery] int capabilityTypeId,  [FromHeader] string userKey)
        {
            return await _masterDataBL.DeleteCapabilityType(capabilityTypeId, userKey);
        }
        
        [HttpGet]
        [Route("GetCapabilityTypeDropdownData")]
        public async Task<List<DropdownDataModel>> GetCapabilityTypeDropdownData( [FromHeader] string userKey,[FromQuery] int id)
        {
            return await _masterDataBL.GetCapabilityTypeDropdownData(userKey,id);
        }
        [HttpGet]
        [Route("GetCapabilityType")]
        public async Task<List<CapabilityType>> GetCapabilityType([FromHeader] string userKey, [FromQuery] int id, [FromQuery] int pageNo, [FromQuery] int pageSize)
        {
            return await _masterDataBL.GetCapabilityType(userKey, id,pageNo,pageSize);
        }

        [HttpPost]
        [Route("AddCapabilityVersion")]
        public async Task<int> AddCapabilityVersion([FromBody] CapabilityVersion capabilityVersion,  [FromHeader] string userKey)
        {
            return await _masterDataBL.AddCapabilityVersion(capabilityVersion, userKey);
        }
        
        [HttpPost]
        [Route("UpdateCapabilityVersion")]
        public async Task<int> UpdateCapabilityVersion([FromBody] CapabilityVersion capabilityVersion,  [FromHeader] string userKey)
        {
            return await _masterDataBL.UpdateCapabilityVersion(capabilityVersion, userKey);
        }
        
        [HttpDelete]
        [Route("DeleteCapabilityVersion")]
        public async Task<int> DeleteCapabilityVersion([FromQuery] int capabilityVersionId,  [FromHeader] string userKey)
        {
            return await _masterDataBL.DeleteCapabilityVersion(capabilityVersionId, userKey);
        }
        
        [HttpGet]
        [Route("GetCapabilityVersionDropdownData")]
        public async Task<List<DropdownDataModel>> GetCapabilityVersionDropdownData( [FromHeader] string userKey, [FromQuery] int id)
        {
            return await _masterDataBL.GetCapabilityVersionDropdownData(userKey,id);
        }
        [HttpGet]
        [Route("GetCapabilityVersion")]
        public async Task<List<CapabilityVersion>> GetCapabilityVersion([FromHeader] string userKey, [FromQuery] int id)
        {
            return await _masterDataBL.GetCapabilityVersion(userKey, id);
        }

        [HttpPost]
        [Route("AddDisplayCategory")]
        public async Task<int> AddDisplayCategory([FromBody] DisplayCategory displayCategory,  [FromHeader] string userKey)
        {
            return await _masterDataBL.AddDisplayCategory(displayCategory, userKey);
        }
        
        [HttpPost]
        [Route("UpdateDisplayCategory")]
        public async Task<int> UpdateDisplayCategory([FromBody] DisplayCategory displayCategory,  [FromHeader] string userKey)
        {
            return await _masterDataBL.UpdateDisplayCategory(displayCategory, userKey);
        }
        
        [HttpDelete]
        [Route("DeleteDisplayCategory")]
        public async Task<int> DeleteDisplayCategory([FromQuery] int displayCategoryId,  [FromHeader] string userKey)
        {
            return await _masterDataBL.DeleteDisplayCategory(displayCategoryId, userKey);
        }
        
        [HttpGet]
        [Route("GetDisplayCategoryDropdownData")]
        public async Task<List<DropdownDataModel>> GetDisplayCategoryDropdownData( [FromHeader] string userKey, [FromQuery] int id)
        {
            return await _masterDataBL.GetDisplayCategoryDropdownData(userKey,id);
        }

        [HttpGet]
        [Route("GetDisplayCategory")]
        public async Task<List<DisplayCategory>> GetDisplayCategory([FromHeader] string userKey, [FromQuery] int id)
        {
            return await _masterDataBL.GetDisplayCategory(userKey, id);
        }

        [HttpPost]
        [Route("AddCapabilityInterface")]
        public async Task<int> AddCapabilityInterface([FromBody] CapabilityInterface capabilityInterface,  [FromHeader] string userKey)
        {
            return await _masterDataBL.AddCapabilityInterface(capabilityInterface, userKey);
        }
        
        [HttpPost]
        [Route("UpdateCapabilityInterface")]
        public async Task<int> UpdateCapabilityInterface([FromBody] CapabilityInterface capabilityInterface,  [FromHeader] string userKey)
        {
            return await _masterDataBL.UpdateCapabilityInterface(capabilityInterface, userKey);
        }
        
        [HttpDelete]
        [Route("DeleteCapabilityInterface")]        
        public async Task<int> DeleteCapabilityInterface([FromQuery] int capabilityInterfaceId,  [FromHeader] string userKey)
        {
            return await _masterDataBL.DeleteCapabilityInterface(capabilityInterfaceId, userKey);
        }
        
        [HttpGet]
        [Route("GetCapabilityInterfaceDropdownData")]
        public async Task<List<DropdownDataModel>> GetCapabilityInterfaceDropdownData( [FromHeader] string userKey, [FromQuery] int id)
        {
            return await _masterDataBL.GetCapabilityInterfaceDropdownData(userKey,id);
        }
        [HttpGet]
        [Route("GetCapabilityInterface")]
        public async Task<dynamic> GetCapabilityInterface([FromHeader] string userKey, [FromQuery] int id, [FromQuery] int pageNo, [FromQuery] int pageSize)
        {
            return await _masterDataBL.GetCapabilityInterface(userKey, id,pageNo,pageSize);
        }

        [HttpPost]
        [Route("AddCapabilitySupportedProperty")]
        public async Task<int> AddCapabilitySupportedProperty([FromBody] CapabilitySupportedProperty capabilitySupportedProperty,  [FromHeader] string userKey)
        {
            return await _masterDataBL.AddCapabilitySupportedProperty(capabilitySupportedProperty, userKey);
        }
        
        [HttpPost]
        [Route("UpdateCapabilitySupportedProperty")]
        public async Task<int> UpdateCapabilitySupportedProperty([FromBody] CapabilitySupportedProperty capabilitySupportedProperty,  [FromHeader] string userKey)
        {
            return await _masterDataBL.UpdateCapabilitySupportedProperty(capabilitySupportedProperty, userKey);
        }
        
        [HttpDelete]
        [Route("DeleteCapabilitySupportedProperty")]
        public async Task<int> DeleteCapabilitySupportedProperty([FromQuery] int capabilitySupportedPropertyId,  [FromHeader] string userKey)
        {
            return await _masterDataBL.DeleteCapabilitySupportedProperty(capabilitySupportedPropertyId, userKey);
        }
        
        [HttpGet]
        [Route("GetCapabilitySupportedPropertyDropdownData")]
        public async Task<List<DropdownDataModel>> GetCapabilitySupportedPropertyDropdownData( [FromHeader] string userKey, [FromQuery] int id)
        {
            return await _masterDataBL.GetCapabilitySupportedPropertyDropdownData(userKey,id);
        }
        [HttpGet]
        [Route("GetCapabilitySupportedProperty")]
        public async Task<List<CapabilitySupportedProperty>> GetCapabilitySupportedProperty([FromHeader] string userKey, [FromQuery] int id)
        {
            return await _masterDataBL.GetCapabilitySupportedProperty(userKey, id);
        }

        [HttpGet]
        [Route("GetAllCapabilityDropdownData")]
        public async Task<AllCapabilityMasterModel> GetAllCapabilityDropdownData( [FromHeader] string userKey, [FromQuery] string searchTerm)
        {
            return await _masterDataBL.GetAllCapabilityDropdownData(userKey,searchTerm);
        }

        [HttpGet]
        [Route("SearchCapabilitySupportedProperty")]
        public async Task<List<CapabilitySupportedProperty>> SearchCapabilitySupportedProperty([FromHeader] string userKey, [FromQuery] string searchTerm)
        {
            return await _masterDataBL.SearchCapabilitySupportedProperty(userKey, searchTerm);
        }
        [HttpGet]
        [Route("SearchCapabilityInterface")]
        public async Task<List<CapabilityInterface>> SearchCapabilityInterface([FromHeader] string userKey, [FromQuery] string searchTerm)
        {
            return await _masterDataBL.SearchCapabilityInterface(userKey, searchTerm);
        }
        [HttpGet]
        [Route("SearchDisplayCategory")]
        public async Task<List<DisplayCategory>> SearchDisplayCategory([FromHeader] string userKey, [FromQuery] string searchTerm)
        {
            return await _masterDataBL.SearchDisplayCategory(userKey, searchTerm);
        }
        [HttpGet]
        [Route("SearchCapabilityVersion")]
        public async Task<List<CapabilityVersion>> SearchCapabilityVersion([FromHeader] string userKey, [FromQuery] string searchTerm)
        {
            return await _masterDataBL.SearchCapabilityVersion(userKey, searchTerm);
        }
        [HttpGet]
        [Route("SearchCapabilityType")]
        public async Task<List<CapabilityType>> SearchCapabilityType([FromHeader] string userKey, [FromQuery] string searchTerm)
        {
          return await  _masterDataBL.SearchCapabilityType(userKey, searchTerm);
        }
    }
}
