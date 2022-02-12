using IoT.DataLayer.Interface;
using IoT.ModelLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.DataLayer.Repository
{
    public class EmailTemplateRepository : IEmailTemplate
    {
        private readonly AppDbContext context;
        public EmailTemplateRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<int> AddTemplate(EmailTemplate emailTemplate)
        {
            context.EmailTemplates.Add(emailTemplate);
            return await context.SaveChangesAsync();
        }

        public async Task<int> DeleteTemplate(int templateId, string userKey)
        {
            if (await isUserExist(userKey) && templateId > 0)
            {
                var oldTemplateType = await context.EmailTemplates.Where(x => x.TemplateId == templateId).FirstOrDefaultAsync();
                if (oldTemplateType != null)
                {
                    var deviceType = context.EmailTemplates.Attach(oldTemplateType);
                    deviceType.State = EntityState.Deleted;
                    return await context.SaveChangesAsync();
                }
            }
            return 0;
        }

        private async Task<bool> isUserExist(string userKey)
        {
            var result=await context.Users.Where(x => x.UserKey.Equals(userKey)).ToListAsync();
            return result.Count > 0;
        }

        public async Task<PagingRecord> GetTemplates(int pageNo, int pageSize, string userKey)
        {
            PagingRecord pagingRecord = new PagingRecord();
            var records = await context.EmailTemplates.Where(x => x.UserKey == userKey).ToListAsync();
            if (records.Count > 0)
            {
                pagingRecord.Data = records.Skip((pageNo - 1) * pageSize).Take(pageSize).AsEnumerable().Cast<object>().ToList();
                pagingRecord.PageNo = pageNo;
                pagingRecord.PageSize = pageSize;
                pagingRecord.TotalRecord = records.Count;
            }
            return pagingRecord;

        }

        public async Task<PagingRecord> SearchTemplates(string searchTerm, int pageNo, int pageSize, string userKey)
        {
            searchTerm = searchTerm.ToLower();
            PagingRecord pagingRecord = new PagingRecord();
            var records = await context.EmailTemplates.Where(x =>
            x.UserKey == userKey &&
                (
                    x.TemplateName.ToLower().Contains(searchTerm) ||
                    x.Subjest.ToLower().Contains(searchTerm) ||
                    x.Keywords.ToLower().Contains(searchTerm) ||
                    x.AttachmentPath.ToLower().Contains(searchTerm) ||
                    x.Body.ToLower().Contains(searchTerm))
                ).ToListAsync();
            if (records.Count > 0)
            {
                pagingRecord.Data = records.Skip((pageNo - 1) * pageSize).Take(pageSize).AsEnumerable().Cast<object>().ToList();
                pagingRecord.PageNo = pageNo;
                pagingRecord.PageSize = pageSize;
                pagingRecord.TotalRecord = records.Count;
            }
            return pagingRecord;
        }

        public Task<int> UpdateTemplate(EmailTemplate emailTemplate, string userKey)
        {
            throw new NotImplementedException();
        }
    }
}
