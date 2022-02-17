using IoT.DataLayer.Interface;
using IoT.ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IoT.BusinessLayer
{
  public  class EmailTemplateBL
    {
        private readonly IEmailTemplate emailTemplateRepo;
        public EmailTemplateBL(IEmailTemplate _emailTemplate)
        {
            this.emailTemplateRepo = _emailTemplate;
        }

        public async Task<int> AddTemplate(EmailTemplate emailTemplate,string userKey)
        {
            emailTemplate.CreatedDate = DateTime.Now;
            emailTemplate.ModifiedDate = DateTime.Now;
            emailTemplate.UserKey = userKey;
           return await emailTemplateRepo.AddTemplate(emailTemplate);
        }

        public async Task<int> DeleteTemplate(int templateId, string userKey)
        {
            return await emailTemplateRepo.DeleteTemplate(templateId,userKey);
        }

        public async Task<PagingRecord> GetTemplates(int pageNo, int pageSize, string userKey)
        {
            return await emailTemplateRepo.GetTemplates(pageNo,pageSize,userKey);
        }

        public async Task<EmailTemplate> GetTemplate(int templateId, string userKey)
        {
            return await emailTemplateRepo.GetTemplate(templateId, userKey);
        }

        public async Task<PagingRecord> SearchTemplates(string searchTerm, int pageNo, int pageSize, string userKey)
        {
            return await emailTemplateRepo.SearchTemplates(searchTerm,pageNo,pageSize,userKey);
        }

        public async Task<int> UpdateTemplate(EmailTemplate emailTemplate, string userKey)
        {
            emailTemplate.ModifiedDate = DateTime.Now;
            return await emailTemplateRepo.UpdateTemplate(emailTemplate,userKey);
        }
    }
}
