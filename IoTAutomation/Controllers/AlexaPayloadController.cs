using IoT.BusinessLayer;
using IoT.DataLayer.Interface;
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
                return Ok(_alexaPayloadBL.GetAlexaDiscoveryPayload(userKey));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured while getting all Devices");
                return BadRequest(ex.Message);
            }
        }
    }
}
