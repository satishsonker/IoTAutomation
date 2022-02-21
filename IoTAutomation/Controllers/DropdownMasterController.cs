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
    public class DropdownMasterController : ControllerBase
    {
        private DropdownMasterBL masterBL;
        private readonly ILogger _logger;
        public DropdownMasterController(IDropdownMaster dropdownMaster, ILogger<DropdownMasterController> logger)
        {
            masterBL = new DropdownMasterBL(dropdownMaster);
            _logger = logger;
        }

        [HttpPost]
        [Route("AddDropdownMaster")]
        public async Task<int> AddData([FromBody] DropdownMaster dropdownMaster, [FromHeader] string userKey)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int result = await masterBL.AddData(dropdownMaster, userKey);
                    if (result < 1)
                    {
                        _logger.LogWarning("Unable to Add Dropdown master: User Key {0}", userKey);
                        return 0;
                    }
                    return result;
                }
                _logger.LogWarning("Get invalid Model while Adding the Dropdown master: User Key {0}", userKey);
                return 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occured while Adding the Dropdown master: User Key {0}", userKey);
                return 0;
            }
        }

        [HttpPost]
        [Route("UpdateDropdownMaster")]
        public async Task<int> UpdateData([FromBody] DropdownMaster dropdownMaster, [FromHeader] string userKey)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int result = await masterBL.UpdateData(dropdownMaster, userKey);
                    if (result < 1)
                    {
                        _logger.LogWarning("Unable to Update Dropdown master: User Key {0}", userKey);
                        return 0;
                    }
                    return result;
                }
                _logger.LogWarning("Get invalid Model while updating the Dropdown master: User Key {0}", userKey);
                return 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occured while Updating the Dropdown master: User Key {0}", userKey);
                return 0;
            }
        }

        [HttpDelete]
        [Route("DeleteDropdownMaster/{dropdownDataId:int}")]
        public async Task<int> DeleteData([FromRoute] int dropdownDataId, [FromHeader] string userKey)
        {
            try
            {
                int result = await masterBL.DeleteData(dropdownDataId, userKey);
                if (result < 1)
                {
                    _logger.LogWarning("Unable to Delete Dropdown master: User Key {0}", userKey);
                    return 0;
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occured while Deleting the Dropdown master: User Key {0}", userKey);
                return 0;
            }
        }

        [HttpGet]
        [Route("SearchDropdownMaster/{searchTerm}/{pageNo:int}/{pageSize:int}")]
        public async Task<PagingRecord> SearchData([FromRoute] string searchTerm, [FromRoute] int pageNo, [FromRoute] int pageSize, [FromHeader] string userKey)
        {
            try
            {
                var result = await masterBL.SearchData(searchTerm, pageNo, pageSize, userKey);
                if (result == null)
                {
                    _logger.LogWarning("Unable to search Dropdown master: User Key {0},searchTerm:{1}", userKey, searchTerm);
                    return new PagingRecord(); ;
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occured while Deleting the Dropdown master: User Key {0}", userKey);
                return new PagingRecord();
            }
        }

        [HttpGet]
        [Route("GetDropdownMaster/{id:int}")]
        public async Task<DropdownMaster> GetData([FromRoute] int id, [FromHeader] string userKey)
        {
            try
            {
                var result = await masterBL.GetData(id, userKey);
                if (result == null)
                {
                    _logger.LogWarning("Unable to Get Dropdown master: User Key {0}", userKey);
                    return new DropdownMaster(); ;
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occured while Deleting the Dropdown master: User Key {0}", userKey);
                return new DropdownMaster();
            }
        }

        [HttpGet]
        [Route("GetAllDropdownMaster")]
        public async Task<PagingRecord> GetAllData([FromRoute] int pageNo, [FromRoute] int pageSize, [FromHeader] string userKey)
        {
            try
            {
                var result = await masterBL.GetAllData(pageNo, pageSize, userKey);
                if (result == null)
                {
                    _logger.LogWarning("Unable to Get all Dropdown master: User Key {0}", userKey);
                    return new PagingRecord(); ;
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Occured while getting allgetting the Dropdown master: User Key {0}", userKey);
                return new PagingRecord();
            }
        }
    }
}
