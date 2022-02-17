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
    public class EmailSettingController : ControllerBase
    {
        private EmailSettingBL settingBL;
        private readonly ILogger _logger;
        public EmailSettingController(IEmailSetting emailSetting, ILogger<EmailSettingController> logger)
        {
            settingBL = new EmailSettingBL(emailSetting);
            _logger = logger;
        }

        [HttpPost]
        [Route("DeleteEmailSetting/{settingId:int}")]
        public async Task<int> DeleteEmailSetting([FromRoute] int settingId,[FromHeader] string userKey)
        {
            return await settingBL.DeleteEmailSetting(settingId, userKey);
        }

        [HttpGet]
        [Route("GetEmailSetting")]
        public async Task<EmailSetting> GetEmailSetting([FromHeader] string userKey)
        {
            return await settingBL.GetEmailSetting(userKey);
        }

        [HttpPost]
        [Route("UpdateEmailSetting")]
        public async Task<int> UpdateEmailSetting([FromBody] EmailSetting emailSetting, [FromHeader] string userKey)
        {
            return await settingBL.UpdateEmailSetting(emailSetting, userKey);
        }
    }
}
