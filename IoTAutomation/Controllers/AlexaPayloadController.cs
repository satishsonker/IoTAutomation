using IoT.BusinessLayer;
using IoT.DataLayer.Interface;
using IoT.ModelLayer;
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
    public class AlexaPayloadController : ControllerBase
    {
        private AlexaPayloadBL _alexaPayloadBL;
        private readonly ILogger _logger;
        public AlexaPayloadController(IAlexaPayload alexaPayload, ILogger<AlexaPayloadController> logger)
        {
            _alexaPayloadBL = new AlexaPayloadBL(alexaPayload);
            _logger = logger;
        }
        [HttpGet]
        [Route("GetAlexaDiscoveryPayload")]
        public async Task<List<Device>> GetAllDevice([FromQuery] string userKey = "")
        {
            try
            {
                return await _alexaPayloadBL.GetAlexaDiscoveryPayload(userKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while getting all Devices");
                return new List<Device>();
            }
        }
        [HttpGet]
        [Route("UpdateDeviceStatus/{deviceKey}/{status}")]
        public async Task<bool> UpdateDeviceStatus([FromRoute] string deviceKey, [FromRoute] string status, [FromQuery] string userKey = "")
        {
            try
            {
                return await _alexaPayloadBL.UpdateDeviceStatus(deviceKey,status, userKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while updating device status");
                return default;
            }
        }
        [HttpGet]
        [Route("GetDeviceStatus/{deviceKey}")]
        public async Task<string> GetDeviceStatus([FromRoute] string deviceKey, [FromQuery] string userKey = "")
        {
            try
            {
                return await _alexaPayloadBL.GetDeviceStatus(deviceKey, userKey);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while getting device status");
                return string.Empty;
            }
        }
    }
}
