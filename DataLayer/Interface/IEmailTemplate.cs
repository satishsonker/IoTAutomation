using IoT.ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IoT.DataLayer.Interface
{
   public interface IEmailTemplate
    {
        Task<int> AddTemplate(EmailTemplate emailTemplate);
        Task<int> UpdateTemplate(EmailTemplate emailTemplate, string userKey);
        Task<int> DeleteTemplate(int templateId, string userKey);
        Task<PagingRecord> GetTemplates(int pageNo,int pageSize, string userKey);
        Task<EmailTemplate> GetTemplate(int templateId, string userKey);
        Task<PagingRecord> SearchTemplates(string searchTerm, int pageNo, int pageSize, string userKey);
    }
}
