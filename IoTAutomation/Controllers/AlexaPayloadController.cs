using IoT.BusinessLayer;
using IoT.DataLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

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
        public IActionResult GetAllDevice([FromQuery] string userKey = "")
        {
            try
            {
                return Ok(_alexaPayloadBL.GetAlexaDiscoveryPayload(userKey).ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while getting all Devices");
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("UpdateDeviceStatus/{deviceKey}/{status}")]
        public IActionResult UpdateDeviceStatus([FromRoute] string deviceKey, [FromRoute] string status, [FromQuery] string userKey = "")
        {
            try
            {
                return Ok(_alexaPayloadBL.UpdateDeviceStatus(deviceKey,status, userKey));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while updating device status");
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("GetDeviceStatus/{deviceKey}")]
        public IActionResult GetDeviceStatus([FromRoute] string deviceKey, [FromQuery] string userKey = "")
        {
            try
            {
                return Ok(_alexaPayloadBL.GetDeviceStatus(deviceKey, userKey));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while getting device status");
                return BadRequest(ex.Message);
            }
        }
    }
}
