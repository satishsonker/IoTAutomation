using IoT.BusinessLayer;
using IoT.DataLayer.Interface;
using IoT.ModelLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoT.WebAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AlexaEventSourceController : ControllerBase
    {
        private readonly ILogger _logger;
        AlexaEventSourceBL _alexaEventSourceBL;
        public AlexaEventSourceController(ILogger<ActivityLogController> logger,IAlexaEventSource alexaEventSource,IOptions<AppSettingConfig> config)
        {
            _logger = logger;
            _alexaEventSourceBL = new AlexaEventSourceBL(alexaEventSource,config);
        }

        [HttpPost]
        [Route("UpdateCode/{code}/{userKey}")]
        public void UpdateCode([FromRoute] string code, [FromRoute] string userKey)
        {
            _alexaEventSourceBL.UpdateCode(code, userKey);
        }

        [HttpPost]
        [Route("PressDoorbell/{apikey}/{endpoitnId}")]
        public void PressDoorbell([FromRoute] string endpoitnId, [FromRoute] string apiKey)
        {
            _alexaEventSourceBL.PressDoorbell(endpoitnId, apiKey);
        }
        [HttpPost]
        [Route("ChangeReport/{apikey}/{endpoitnId}")]
        public void ChangeReport([FromRoute] string apiKey)
        {
            //_alexaEventSourceBL.PressDoorbell(apiKey);
        }
    }
}
