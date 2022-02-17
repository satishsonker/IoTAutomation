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
    public class EmailSettingRepository : IEmailSetting
    {
        private readonly AppDbContext context;
        public EmailSettingRepository(AppDbContext appDbContext)
        {
            this.context = appDbContext;
        }
        public async Task<int> DeleteEmailSetting(int settingId, string userKey)
        {
            if (await isUserExist(userKey))
            {
                var oldSetting = await context.EmailSettings.Where(x => x.SettingId == settingId).FirstOrDefaultAsync();
                var setting = context.EmailSettings.Attach(oldSetting);
                setting.State = EntityState.Deleted;
                return await context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<EmailSetting> GetEmailSetting(string userKey)
        {
            return await context.EmailSettings.OrderByDescending(x => x.CreatedDate).FirstOrDefaultAsync();
        }

        public async Task<int> UpdateEmailSetting(EmailSetting emailSetting, string userKey)
        {
            if (await isUserExist(userKey) && emailSetting != null)
            {
                emailSetting.ModifiedDate = DateTime.Now;
                if (await context.EmailSettings.Where(x => x.SettingId == emailSetting.SettingId).CountAsync() > 0)
                {
                    var eTemplates = context.EmailSettings.Attach(emailSetting);
                    eTemplates.State = EntityState.Modified;
                }
                else
                {
                    context.EmailSettings.Add(emailSetting);
                }
                return await context.SaveChangesAsync();
            }
            return 0;
        }
        private async Task<bool> isUserExist(string userKey)
        {
            var result = await context.Users.Where(x => x.UserKey.Equals(userKey)).ToListAsync();
            return result.Count > 0;
        }
    }
}
