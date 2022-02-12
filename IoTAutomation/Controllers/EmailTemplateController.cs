using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IoT.WebAPI.Filters;
using IoT.DataLayer.Interface;
using Microsoft.Extensions.Logging;
using IoT.BusinessLayer;
using IoT.ModelLayer;

namespace IoT.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [UserKey]
    public class EmailTemplateController : ControllerBase
    {
        private EmailTemplateBL templateBL;
        private readonly ILogger _logger;
        public EmailTemplateController(IEmailTemplate emailTemplate, ILogger<EmailTemplateController> logger)
        {
            templateBL = new EmailTemplateBL(emailTemplate);
            _logger = logger;
        }

        [HttpPost]
        [Route("AddEmailTemplate")]
        public async Task<int> AddTemplate([FromBody]EmailTemplate emailTemplate,[FromHeader] string userKey)
        {
            return await templateBL.AddTemplate(emailTemplate, userKey);
        }

        [HttpPost]
        [Route("UpdateEmailTemplate")]
        public async Task<int> UpdateTemplate([FromBody] EmailTemplate emailTemplate, [FromHeader] string userKey)
        {
            return await templateBL.UpdateTemplate(emailTemplate, userKey);
        }

        [HttpPost]
        [Route("DeleteEmailTemplate/{templateId:int}")]
        public async Task<int> DeleteTemplate([FromRoute]int templateId,[FromHeader] string userKey)
        {
            return await templateBL.DeleteTemplate(templateId, userKey);
        }

        [HttpGet]
        [Route("GetEmailTemplate/{pageNo:int}/{pageSize:int}")]
        public async Task<PagingRecord> GetTemplates([FromRoute]int pageNo, [FromRoute] int pageSize, [FromHeader] string userKey)
        {
            return await templateBL.GetTemplates(pageNo, pageSize, userKey);                                                                                                                                                       
        }

        [HttpGet]
        [Route("SearchEmailTemplate/{searchTerm}/{pageNo:int}/{pageSize:int}")]
        public async Task<PagingRecord> SearchTemplates([FromRoute] string searchTerm, [FromRoute] int pageNo, [FromRoute] int pageSize, [FromHeader] string userKey)
        {
            return await templateBL.SearchTemplates(searchTerm, pageNo, pageSize, userKey);
        }
    }
}
